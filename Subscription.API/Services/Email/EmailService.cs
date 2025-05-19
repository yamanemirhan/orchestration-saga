namespace Subscription.API.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task SendWelcomeEmailAsync(Guid subscriptionId, string email)
        {
            Console.WriteLine($"Welcome email sent to {email} for subscription {subscriptionId}");
            return Task.CompletedTask;
        }
    }
}
