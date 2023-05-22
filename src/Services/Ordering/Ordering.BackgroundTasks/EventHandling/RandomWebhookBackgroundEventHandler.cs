namespace Ordering.BackgroundTasks.EventHandling
{
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
    using Microsoft.Extensions.Logging;
    using Ordering.BackgroundTasks.Events;
    using System.Threading.Tasks;

    public class RandomWebhookBackgroundEventHandler : IIntegrationEventHandler<RandomWebhookBackgroundEvent> 
    {
        private readonly ILogger<RandomWebhookBackgroundEventHandler> _logger;

        public RandomWebhookBackgroundEventHandler(ILogger<RandomWebhookBackgroundEventHandler> logger) 
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(RandomWebhookBackgroundEvent @event) 
        {
            TimeService.logCurrentTimestamp(_logger);
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at Webhook - ({@IntegrationEvent})", @event.Id, @event);

            _logger.LogInformation("----- Random Event ID: {id}", @event.EventId);

            foreach (var randomString in @event.ListOfRandomStrings)
            {
                _logger.LogInformation("----- Random String: {string} -----", randomString);
            }

            foreach (var randomNumber in @event.ListOfRandomNumbers)
            {
                _logger.LogInformation("----- Random Number: {number} -----", randomNumber);
            }
        }

    }
}