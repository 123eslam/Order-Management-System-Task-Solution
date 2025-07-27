using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.BLL.Dtos.InvoiceDtos;
using OrderManagementSystemTask.BLL.Services.InvoiceServices;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(InvoiceResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InvoiceResultDto>> GetInvoiceById(int id)
        {
            var invoice = await invoiceService.GetInvoiceByIdAsync(id);
            return Ok(invoice);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<InvoiceResultDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InvoiceResultDto>>> GetAllInvoices()
        {
            var invoices = await invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }
    }
}
