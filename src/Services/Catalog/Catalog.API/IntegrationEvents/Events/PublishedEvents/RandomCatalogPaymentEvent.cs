namespace Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events;

// Random Events notes:
// A random Event is implemented for evaluation purposes to simulate communication between all services
public record RandomCatalogPaymentEvent : IntegrationEvent 
{
    public String EventId {get; private init;}

    public List<int> ListOfRandomNumbers {get; private init;}

    public List<String> ListOfRandomStrings {get; private init;}

    public DateTime timeOfPublishment {get; private init;}

    public RandomCatalogPaymentEvent(String eventId, List<int> listOfRandomNumbers, List<String> listOfRandomStrings) 
    {
        EventId = eventId;
        ListOfRandomNumbers = listOfRandomNumbers;
        ListOfRandomStrings = listOfRandomStrings;
        timeOfPublishment = DateTime.Now;
    }
}