namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.EventHandling;

public class RandomSignalOrderingEventHandler : IIntegrationEventHandler<RandomSignalOrderingEvent> 
{
    private readonly ILogger<RandomSignalOrderingEventHandler> _logger;

    public RandomSignalOrderingEventHandler(ILogger<RandomSignalOrderingEventHandler> logger) 
    {
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(RandomSignalOrderingEvent @event) 
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