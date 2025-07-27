using OrderManagementSystemTask.BLL.Dtos.CustomerDto;
using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;

namespace OrderManagementSystemTask.BLL.Services.CustomerServices
{
    public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
    {
        public async Task<CustomerResultDto> CreateCustomerAsync(CreateOrUpdateCustomerDto customerDto)
        {
            var customere = new Customer
            {
                Email = customerDto.Email,
                Name = customerDto.Name
            };
            unitOfWork.CustomerRepostory.Add(customere);
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to create customer.");
            }
            return new CustomerResultDto
            {
                Id = customere.Id,
                Name = customere.Name,
                Email = customere.Email
            };
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllCustomerOrders(int customerId)
        {
            var orders = await unitOfWork.OrderRepository.GetAllCustomerOrderAsync(customerId);
            if (orders == null || !orders.Any())
            {
                throw new NotFoundException($"No orders found for customer with ID {customerId}.");
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
