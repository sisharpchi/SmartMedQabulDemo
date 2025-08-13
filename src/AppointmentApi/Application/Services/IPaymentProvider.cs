namespace AppointmentApi.Application.Services;

public interface IPaymentProvider
{
    Task<bool> ChargeAsync(decimal amount, string currency);
    Task<bool> RefundAsync(long paymentId);
}

public class FakePaymentProvider : IPaymentProvider
{
    public Task<bool> ChargeAsync(decimal amount, string currency) => Task.FromResult(true);
    public Task<bool> RefundAsync(long paymentId) => Task.FromResult(true);
}
