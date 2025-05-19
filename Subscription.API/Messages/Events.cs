namespace Subscription.API.Messages
{
    public class SubscriptionCreated
    {
        public Guid SubscriptionId { get; init; }
        public Guid UserId { get; init; }
        public string Email { get; init; }
    }
    public class PaymentSucceeded
    {
        public Guid SubscriptionId { get; init; }
    }
    public class PaymentFailed
    {
        public Guid SubscriptionId { get; init; }
        public string Reason { get; init; }
    }

    public class ServiceActivated
    {
        public Guid SubscriptionId { get; init; }
    }

    public class InvoiceGenerated
    {
        public Guid SubscriptionId { get; init; }
    }

    public class WelcomeEmailSent
    {
        public Guid SubscriptionId { get; init; }
    }
}
