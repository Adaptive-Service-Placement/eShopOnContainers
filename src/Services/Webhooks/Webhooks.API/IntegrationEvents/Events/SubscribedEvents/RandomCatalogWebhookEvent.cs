namespace Webhooks.API.IntegrationEvents;

// Random Events notes:
// A random Event is implemented for evaluation purposes to simulate communication between all services
public record RandomCatalogWebhookEvent : IntegrationEvent 
{
    public String EventId {get; private init;}

    public List<int> ListOfRandomNumbers {get; private init;}

    public List<String> ListOfRandomStrings {get; private init;}

    public RandomCatalogWebhookEvent(String eventId, List<int> listOfRandomNumbers, List<String> listOfRandomStrings) 
    {
        EventId = eventId;
        ListOfRandomNumbers = listOfRandomNumbers;
        ListOfRandomStrings = listOfRandomStrings;
    }
}