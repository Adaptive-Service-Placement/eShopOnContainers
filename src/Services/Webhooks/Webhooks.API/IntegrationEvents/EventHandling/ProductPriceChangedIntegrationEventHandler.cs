namespace Webhooks.API.IntegrationEvents;

public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
{
    public async Task Handle(ProductPriceChangedIntegrationEvent @event)
    {
        TimeService.logCurrentTimestamp(_logger);
        int i = 0;
    }
}
