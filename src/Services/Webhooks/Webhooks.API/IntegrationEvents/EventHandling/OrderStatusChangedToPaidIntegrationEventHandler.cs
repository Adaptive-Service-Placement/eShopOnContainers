﻿namespace Webhooks.API.IntegrationEvents;

public class OrderStatusChangedToPaidIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
{
    private readonly IWebhooksRetriever _retriever;
    private readonly IWebhooksSender _sender;
    private readonly ILogger<OrderStatusChangedToShippedIntegrationEventHandler> _logger;
    public OrderStatusChangedToPaidIntegrationEventHandler(IWebhooksRetriever retriever, IWebhooksSender sender, ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger)
    {
        _retriever = retriever;
        _sender = sender;
        _logger = logger;
    }

    public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
    {
        TimeService.logCurrentTimestamp(_logger);
        var subscriptions = await _retriever.GetSubscriptionsOfType(WebhookType.OrderPaid);
        _logger.LogInformation("Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionsCount} subscriptions to process", subscriptions.Count());
        var whook = new WebhookData(WebhookType.OrderPaid, @event);
        await _sender.SendAll(subscriptions, whook);
    }
}
