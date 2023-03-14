namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
public class MessagingInformation {

    [JsonInclude]
    public String serviceIp{get; set;}

    [JsonInclude]
    public String exchange{get; set;}

    [JsonInclude]
    public String routingKey{get; set;}

    [JsonInclude]
    public decimal messageSize{get; set;}
}