using OrderManagementSystemTask.BLL.Dtos.EmailDto;

namespace OrderManagementSystemTask.BLL.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
