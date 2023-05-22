namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents.Events;

// Random Events notes:
// A random Event is implemented for evaluation purposes to simulate communication between all services
public record RandomOrderingSignalEvent : IntegrationEvent 
{
    public String EventId {get; private init;}

    public List<int> ListOfRandomNumbers {get; private init;}

    public List<String> ListOfRandomStrings {get; private init;}

    public RandomOrderingSignalEvent(String eventId, List<int> listOfRandomNumbers, List<String> listOfRandomStrings) 
    {
        EventId = eventId;
        ListOfRandomNumbers = listOfRandomNumbers;
        ListOfRandomStrings = listOfRandomStrings;
    }
}