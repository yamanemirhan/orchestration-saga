using MassTransit;

namespace Subscription.API.Sagas
{
    public class SubscriptionState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public Guid SubscriptionId { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }

        // what state the saga is in
        // having individual properties for each state could be helpful for complex conditions
        // such as when PaymentSucceeded true, WelcomeEmailSent true but ServiceActivated is false etc.
        public bool PaymentSucceeded { get; set; }

        public bool ServiceActivated { get; set; }

        public bool InvoiceGenerated { get; set; }
        public bool WelcomeEmailSent { get; set; }
    }
}