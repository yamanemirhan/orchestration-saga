using MassTransit;
using Microsoft.EntityFrameworkCore;
using Subscription.API.Data;
using Subscription.API.Messages;

namespace Subscription.API.Handlers
{
    public class ActivateServiceCommandHandler(AppDbContext dbContext) : IConsumer<ActivateServiceCommand>
    {
        public async Task Consume(ConsumeContext<ActivateServiceCommand> context)
        {
            var message = context.Message;

            var subscription = await dbContext.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == message.SubscriptionId);

            if (subscription != null)
            {
                subscription.IsActive = true;
                await dbContext.SaveChangesAsync();

                await context.Publish(new ServiceActivated
                {
                    SubscriptionId = message.SubscriptionId
                });
            }
            else
            {
                Console.WriteLine($"Abonelik bulunamadı: {message.SubscriptionId}");
            }
        }
    }
}
