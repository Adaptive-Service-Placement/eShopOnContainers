namespace Webhooks.API.IntegrationEvents;

public class RandomCatalogWebhookEventHandler : IIntegrationEventHandler<RandomCatalogWebhookEvent> 
{
    private readonly ILogger<RandomCatalogWebhookEventHandler> _logger;

    public RandomCatalogWebhookEventHandler(ILogger<RandomCatalogWebhookEventHandler> logger) 
    {
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(RandomCatalogWebhookEvent @event) 
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

           using (LogContext.PushProperty("Latency", $"{@event.Id}-{Program.AppName}"))
            {
                TimeSpan latency = DateTime.Now - @event.CreationDate;
                _logger.LogInformation("{latency}", (int)latency.TotalMilliseconds);
            }
    }

}