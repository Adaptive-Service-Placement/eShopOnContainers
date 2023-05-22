namespace Microsoft.eShopOnContainers.Payment.API.IntegrationEvents.EventHandling;

// Random Event Handler do nothing but print all class attributes, they do not serve any specific purpose
public class RandomWebhookPaymentEventHandler : IIntegrationEventHandler<RandomWebhookPaymentEvent>
{
    private readonly ILogger<RandomWebhookPaymentEventHandler> _logger;

    public RandomWebhookPaymentEventHandler(ILogger<RandomWebhookPaymentEventHandler> logger) 
    {
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(RandomWebhookPaymentEvent @event) 
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
    }
}
