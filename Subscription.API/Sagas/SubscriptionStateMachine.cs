using MassTransit;
using Subscription.API.Messages;

namespace Subscription.API.Sagas
{
    public class SubscriptionStateMachine : MassTransitStateMachine<SubscriptionState>
    {
        public State SubscriptionCreated { get; private set; }
        public State PaymentProcessed { get; private set; }
        public State Activated { get; private set; }
        public State Completed { get; private set; }
        public State Cancelled { get; private set; }

        public Event<SubscriptionCreated> SubscriptionCreatedEvent { get; private set; }
        public Event<PaymentSucceeded> PaymentSucceeded { get; private set; }
        public Event<PaymentFailed> PaymentFailed { get; private set; }
        public Event<ServiceActivated> ServiceActivated { get; private set; }
        public Event<InvoiceGenerated> InvoiceGenerated { get; private set; }
        public Event<WelcomeEmailSent> WelcomeEmailSent { get; private set; }

        public SubscriptionStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => SubscriptionCreatedEvent, x => x.CorrelateById(m => m.Message.SubscriptionId));
            Event(() => PaymentSucceeded, x => x.CorrelateById(context => context.Message.SubscriptionId));
            Event(() => PaymentFailed, x => x.CorrelateById(context => context.Message.SubscriptionId));
            Event(() => ServiceActivated, x => x.CorrelateById(context => context.Message.SubscriptionId));
            Event(() => InvoiceGenerated, x => x.CorrelateById(context => context.Message.SubscriptionId));
            Event(() => WelcomeEmailSent, x => x.CorrelateById(context => context.Message.SubscriptionId));

            Initially(
                 When(SubscriptionCreatedEvent)
                     .Then(context =>
                     {
                         context.Saga.SubscriptionId = context.Message.SubscriptionId;
                         context.Saga.UserId = context.Message.UserId;
                         context.Saga.Email = context.Message.Email;
                         context.Saga.CreatedAt = DateTime.UtcNow;
                     })
                     .ThenAsync(context =>
                         context.Publish(new ProcessPaymentCommand(context.Saga.SubscriptionId, 9.99m)))
                     .TransitionTo(SubscriptionCreated)
             );

            During(SubscriptionCreated,
                When(PaymentSucceeded)
                    .Then(context => context.Saga.PaymentSucceeded = true)
                    .ThenAsync(context => context.Publish(new ActivateServiceCommand(context.Message.SubscriptionId)))
                    .TransitionTo(PaymentProcessed),

                When(PaymentFailed)
                    .Then(context =>
                    {
                        context.Saga.PaymentSucceeded = false;
                    })
                    .TransitionTo(Cancelled)
            );

            During(PaymentProcessed,
                When(ServiceActivated)
                    .Then(context => context.Saga.ServiceActivated = true)
                    .ThenAsync(context => context.Publish(new GenerateInvoiceCommand(context.Message.SubscriptionId)))
                    .TransitionTo(Activated)
            );

            During(Activated,
                When(InvoiceGenerated)
                    .Then(context =>
                    {
                        context.Saga.InvoiceGenerated = true;
                    })
                    .ThenAsync(context => context.Publish(new SendWelcomeEmailCommand(context.Saga.SubscriptionId, context.Saga.Email)))
                    .TransitionTo(Activated),

                When(WelcomeEmailSent)
                    .Then(context => context.Saga.WelcomeEmailSent = true)
                    .TransitionTo(Completed)
                    .Then(context => Console.WriteLine($"Subscription {context.Saga.SubscriptionId} completed successfully."))
                    .Finalize()
            );
        }
    }
}
