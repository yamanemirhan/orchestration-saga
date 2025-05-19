using MassTransit;
using Subscription.API.Data;
using Subscription.API.Entities;
using Subscription.API.Messages;
using Microsoft.EntityFrameworkCore;

namespace Subscription.API.Handlers
{
    public class CreateSubscriptionCommandHandler(AppDbContext dbContext) : IConsumer<CreateSubscriptionCommand>
    {
        public async Task Consume(ConsumeContext<CreateSubscriptionCommand> context)
        {
            var message = context.Message;

            // TODO: Check for duplicate subscription with same SubscriptionId and UserId, isActive
            var existingSubscription = await dbContext.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Email == message.Email);

            if (existingSubscription != null)
            {
                Console.WriteLine("Aktif abonelik mevcut, yeni abonelik oluşturulmadı.");
                return;
            }

            var subscription = new SubscriptionEntity
            {
                Id = message.SubscriptionId,
                UserId = message.UserId,
                Email = message.Email,
                IsActive = false,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Subscriptions.Add(subscription);
            await dbContext.SaveChangesAsync();

            await context.Publish(new SubscriptionCreated
            {
                SubscriptionId = message.SubscriptionId,
                UserId = message.UserId,
                Email = message.Email
            });
        }
    }
}
