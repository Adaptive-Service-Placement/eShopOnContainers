namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.EventHandling;

public class RandomCatalogOrderingEventHandler : IIntegrationEventHandler<RandomCatalogOrderingEvent> 
{
    private readonly ILogger<RandomCatalogOrderingEventHandler> _logger;

    public RandomCatalogOrderingEventHandler(ILogger<RandomCatalogOrderingEventHandler> logger) 
    {
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(RandomCatalogOrderingEvent @event) 
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