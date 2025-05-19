using MassTransit;
using Subscription.API.Data;
using Subscription.API.Messages;

namespace Subscription.API.Handlers
{
    public class GenerateInvoiceCommandHandler(AppDbContext dbContext) : IConsumer<GenerateInvoiceCommand>
    {
        public async Task Consume(ConsumeContext<GenerateInvoiceCommand> context)
        {
            var message = context.Message;

            // TODO

            await context.Publish(new InvoiceGenerated
            {
                SubscriptionId = message.SubscriptionId
            });
        }
    }
}
