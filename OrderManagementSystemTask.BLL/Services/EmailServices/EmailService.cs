using OrderManagementSystemTask.BLL.Dtos.EmailDto;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace OrderManagementSystemTask.BLL.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Email email)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var client = new SmtpClient(emailSettings["Host"], int.Parse(emailSettings["Port"]));
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(emailSettings["Email"], emailSettings["Password"]);

            await client.SendMailAsync(emailSettings["Email"], email.To, email.Subject, email.Body);
        }
    }
}
