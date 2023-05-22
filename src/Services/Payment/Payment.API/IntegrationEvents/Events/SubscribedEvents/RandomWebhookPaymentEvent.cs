using System.Collections.Generic;

namespace Microsoft.eShopOnContainers.Payment.API.IntegrationEvents.Events;

// Random Events notes:
// A random Event is implemented for evaluation purposes to simulate communication between all services
public record RandomWebhookPaymentEvent : IntegrationEvent 
{
    public String EventId {get; private init;}

    public List<int> ListOfRandomNumbers {get; private init;}

    public List<String> ListOfRandomStrings {get; private init;}

    public RandomWebhookPaymentEvent(String eventId, List<int> listOfRandomNumbers, List<String> listOfRandomStrings) 
    {
        EventId = eventId;
        ListOfRandomNumbers = listOfRandomNumbers;
        ListOfRandomStrings = listOfRandomStrings;
    }
}