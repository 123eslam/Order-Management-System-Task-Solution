using OrderManagementSystemTask.BLL.Dtos.EmailDto;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.BLL.Services.EmailServices;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;

namespace OrderManagementSystemTask.BLL.Services.OrderServices
{
    public class OrderService(IUnitOfWork unitOfWork, IEmailService emailService) : IOrderService
    {
        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync()
        {
            var orders = await unitOfWork.OrderRepository.GetAllOrdersWithDetailsAsync();
            var orderDtos = orders.Select(order => new OrderResultDto
            {
                Id = order.Id,
                CustomerName = order.Customer.Name,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.UnitPrice,
                    Discount = item.Discount
                }).ToList()
            });
            return orderDtos;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(int orderId)
        {
            var order = await unitOfWork.OrderRepository.GetOrderWithDetailsByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }
            return new OrderResultDto
            {
                Id = order.Id,
                CustomerName = order.Customer.Name,
                CustomerEmail = order.Customer.Email,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod, 
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.UnitPrice,
                    Discount = item.Discount
                }).ToList()
            };
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest)
        {
            var customer = await unitOfWork.CustomerRepostory.GetByIdAsync(orderRequest.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {orderRequest.CustomerId} not found.");
            }
            var productIds = orderRequest.OrderItems.Select(item => item.ProductId).ToList();
            var products = await unitOfWork.ProductRepostory.GetByIdsAsync(productIds); 
            var productsDict = products.ToDictionary(p => p.Id);
            foreach (var item in orderRequest.OrderItems)
            {
                if (!productsDict.TryGetValue(item.ProductId, out var product) || product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} is either not available or has insufficient stock.");
                }
            }
            var orderItems = orderRequest.OrderItems.Select(item =>
            {
                var product = productsDict[item.ProductId];
                return new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price, 
                    Discount = 0 
                };
            }).ToList();
            decimal subTotal = orderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
            decimal discountPercentage = 0m;
            if (subTotal > 200)
            {
                discountPercentage = 0.10m; 
            }
            else if (subTotal > 100)
            {
                discountPercentage = 0.05m; 
            }
            decimal totalAmount = subTotal * (1 - discountPercentage);
            var newOrder = new Order
            {
                CustomerId = orderRequest.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                PaymentMethod = orderRequest.PaymentMethod,
                OrderItems = orderItems,
                PaymentIntentId = 0//for now, this will be replaced with actual payment intent ID
            };
            unitOfWork.OrderRepository.Add(newOrder);
            foreach (var item in orderItems)
            {
                productsDict[item.ProductId].Stock -= item.Quantity;
            }
            var newInvoice = new Invoice
            {
                Order = newOrder,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = totalAmount
            };
            unitOfWork.InvoiceRepository.Add(newInvoice);
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to create the order.");
            }
            return await GetOrderByIdAsync(newOrder.Id);
        }

        public async Task<OrderResultDto> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await unitOfWork.OrderRepository.GetOrderWithDetailsByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }
            var validStatuses = new[] { "Pending", "PaymentReceived", "Shipped", "Delivered", "Cancelled" };
            if (!validStatuses.Contains(newStatus))
            {
                throw new ArgumentException($"'{newStatus}' is not a valid order status.");
            }
            order.Status = newStatus;
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to update the order status.");
            }
            var emailSend = new Email
            {
                To = order.Customer.Email,
                Subject = "Order Status Update",
                Body = $"Your order with ID {order.Id} has been updated to '{newStatus}'."
            };
            await emailService.SendEmailAsync(emailSend);
            return await GetOrderByIdAsync(orderId);
        }
    }
}
