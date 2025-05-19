namespace Subscription.API.Messages
{
    public record CreateSubscriptionCommand(Guid SubscriptionId, Guid UserId, string Email);
    public record ProcessPaymentCommand(Guid SubscriptionId, decimal Amount);
    public record ActivateServiceCommand(Guid SubscriptionId);
    public record GenerateInvoiceCommand(Guid SubscriptionId);
    public record SendWelcomeEmailCommand(Guid SubscriptionId, string Email);
    public record CancelSubscriptionCommand(Guid SubscriptionId, string Reason);
}
