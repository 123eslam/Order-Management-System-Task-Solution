using Microsoft.Extensions.Logging;

namespace OrderManagementSystemTask.BLL.Services.PaymentServices
{
    public class PayPalPaymentService(ILogger<PayPalPaymentService> logger) : IPaymentService
    {
        public string PaymentMethodName => "PayPal";
        public async Task<bool> ProcessPayment(decimal amount)
        {
            logger.LogInformation("Attempting to process payment of {Amount:C} using Credit Card.", amount);
            await Task.Delay(500);
            logger.LogInformation("Payment of {Amount:C} using Credit Card was successful.", amount);
            return true;
        }
    }
}
