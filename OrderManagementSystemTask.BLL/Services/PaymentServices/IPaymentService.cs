namespace OrderManagementSystemTask.BLL.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(decimal amount);
        string PaymentMethodName { get; }
    }
}
