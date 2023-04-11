namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;

public class TimeService {
    public static void logCurrentTimestamp(ILogger _logger) {
        String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        _logger.LogInformation("Received event at {time}", timeStamp);
    }
}