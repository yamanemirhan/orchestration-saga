using MassTransit;
using Subscription.API.Data;
using Subscription.API.Messages;

namespace Subscription.API.Handlers
{
    public class ProcessPaymentCommandHandler(AppDbContext dbContext) : IConsumer<ProcessPaymentCommand>
    {
        public async Task Consume(ConsumeContext<ProcessPaymentCommand> context)
        {
            var message = context.Message;

            bool paymentSuccess = true;
            //bool paymentSuccess = false;

            if (paymentSuccess)
            {
                await context.Publish(new PaymentSucceeded
                {
                    SubscriptionId = message.SubscriptionId
                });
            }
            else
            {
                await context.Publish(new PaymentFailed
                {
                    SubscriptionId = message.SubscriptionId,
                    Reason = "Ödeme reddedildi"
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
