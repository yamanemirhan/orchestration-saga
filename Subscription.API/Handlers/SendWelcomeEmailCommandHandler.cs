using MassTransit;
using Subscription.API.Data;
using Subscription.API.Messages;
using Subscription.API.Services.Email;

namespace Subscription.API.Handlers
{
    public class SendWelcomeEmailCommandHandler(IEmailService emailService) : IConsumer<SendWelcomeEmailCommand>
    {
        public async Task Consume(ConsumeContext<SendWelcomeEmailCommand> context)
        {
            var message = context.Message;

            await emailService.SendWelcomeEmailAsync(message.SubscriptionId, message.Email);

            await context.Publish(new WelcomeEmailSent
            {
                SubscriptionId = message.SubscriptionId
            });
        }
    }
}
