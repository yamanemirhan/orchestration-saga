using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Subscription.API.DTOs.Requests;
using Subscription.API.Messages;

namespace Subscription.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController(IPublishEndpoint _publishEndpoint) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            var command = new CreateSubscriptionCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request.Email
            );

            await _publishEndpoint.Publish(command);

            return Ok($"Subscription command sent for {request.Email}");
        }
    }
}
