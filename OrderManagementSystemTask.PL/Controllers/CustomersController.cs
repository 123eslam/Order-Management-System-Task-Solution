using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.CustomerDto;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.BLL.Services.CustomerServices;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerResultDto>> CreateCustomer(CustomerResultDto customerDto)
        {
            var createdCustomer = await customerService.CreateCustomerAsync(customerDto);
            return Ok(createdCustomer);
        }
        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(typeof(IEnumerable<OrderResultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllCustomerOrders(int customerId)
        {
            try
            {
                var orders = await customerService.GetAllCustomerOrders(customerId);
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
