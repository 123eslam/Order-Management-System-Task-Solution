using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.CustomerDto;
using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.BLL.Services.CustomerServices;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerResultDto>> CreateCustomer(CreateOrUpdateCustomerDto customerDto)
        {
            var createdCustomer = await customerService.CreateCustomerAsync(customerDto);
            return Ok(createdCustomer);
        }
        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(typeof(IEnumerable<OrderResultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllCustomerOrders(int customerId)
        {
            var orders = await customerService.GetAllCustomerOrders(customerId);
            return Ok(orders);
        }
    }
}
