namespace Subscription.API.Services.Email
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(Guid subscriptionId, string email);
    }
}
