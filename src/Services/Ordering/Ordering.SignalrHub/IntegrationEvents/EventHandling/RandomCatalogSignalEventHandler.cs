namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents.EventHandling;

// Random Event Handler do nothing but print all class attributes, they do not serve any specific purpose
public class RandomCatalogSignalEventHandler : IIntegrationEventHandler<RandomCatalogSignalEvent> 
{
    private readonly ILogger<RandomCatalogSignalEventHandler> _logger;

    public RandomCatalogSignalEventHandler(ILogger<RandomCatalogSignalEventHandler> logger) 
    {
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(RandomCatalogSignalEvent @event) 
    {
        TimeService.logCurrentTimestamp(_logger);
         using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

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

        using (LogContext.PushProperty("Latency", $"{@event.Id}-{Program.AppName}"))
        {
            TimeSpan latency = DateTime.Now - @event.CreationDate;
            _logger.LogInformation("{latency}", (int)latency.TotalMilliseconds);
        }
    }
}