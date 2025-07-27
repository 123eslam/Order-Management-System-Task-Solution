using OrderManagementSystemTask.BLL.Dtos.CustomerDto;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;

namespace OrderManagementSystemTask.BLL.Services.CustomerServices
{
    public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
    {
        public Task<CustomerResultDto> CreateCustomerAsync(CustomerResultDto customerDto)
        {
            var customere = new Customer
            {
                Email = customerDto.Email,
                Name = customerDto.Name
            };
            unitOfWork.CustomerRepostory.Add(customere);
            return unitOfWork.CompleteAsync().ContinueWith(c =>
            {
                if (c.IsCompletedSuccessfully)
                {
                    return customerDto;
                }
                else
                {
                    throw new Exception("Failed to create customer.");
                }
            });
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllCustomerOrders(int customerId)
        {
            var orders = await unitOfWork.OrderRepository.GetAllCustomerOrderAsync(customerId);
            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException($"No orders found for customer with ID {customerId}.");
            }
            return orders.Select(o => new OrderResultDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                CustomerEmail = o.Customer.Email,
                CustomerName = o.Customer.Name,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.UnitPrice,
                    Discount = oi.Discount
                }).ToList(),
                PaymentMethod = o.PaymentMethod,
                Status = o.PaymentIntentId.ToString() //Will Change later
            });
        }
    }
}
