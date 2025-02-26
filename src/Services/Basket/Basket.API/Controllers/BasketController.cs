﻿namespace Microsoft.eShopOnContainers.Services.Basket.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly IIdentityService _identityService;
    private readonly IEventBus _eventBus;
    private readonly ILogger<BasketController> _logger;

    public BasketController(
        ILogger<BasketController> logger,
        IBasketRepository repository,
        IIdentityService identityService,
        IEventBus eventBus)
    {
        _logger = logger;
        _repository = repository;
        _identityService = identityService;
        _eventBus = eventBus;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
    {
        var basket = await _repository.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket value)
    {   
        var randomBasketPaymentEvent = new RandomBasketPaymentEvent(value.BuyerId, createListOfRandomNumbers(), createListOfRandomStrings());

        try 
        {
            _eventBus.Publish(randomBasketPaymentEvent);
        } 
        catch(Exception ex) 
        {
            _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", randomBasketPaymentEvent.Id, Program.AppName);
            
            throw;
        }

        return Ok(await _repository.UpdateBasketAsync(value));
    }

    [Route("checkout")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
    {
        var userId = _identityService.GetUserIdentity();

        basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
            guid : basketCheckout.RequestId;

        var basket = await _repository.GetBasketAsync(userId);

        if (basket == null)
        {
            return BadRequest();
        }

        var userName = this.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

        var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
            basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
            basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

        // Once basket is checkout, sends an integration event to
        // ordering.api to convert basket to order and proceeds with
        // order creation process
        try
        {
            _eventBus.Publish(eventMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, Program.AppName);

            throw;
        }

        // sends a Random Event to the catalog.api
        var randomBasketCatalogEventMessage = new RandomBasketCatalogEvent(userId, createListOfRandomNumbers(), createListOfRandomStrings());

        try 
        {
            _eventBus.Publish(randomBasketCatalogEventMessage);
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", randomBasketCatalogEventMessage.Id, Program.AppName);

            throw;
        }

        return Accepted();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteBasketByIdAsync(string id)
    {
        var randomBasketWebhookEvent = new RandomBasketWebhookEvent(id, createListOfRandomNumbers(), createListOfRandomStrings());

        try 
        {
            _eventBus.Publish(randomBasketWebhookEvent);
        } 
        catch(Exception ex) 
        {
            _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", randomBasketWebhookEvent.Id, Program.AppName);

            throw;
        }
        await _repository.DeleteBasketAsync(id);
    }

    private List<int> createListOfRandomNumbers() 
    {
        Random rand = new Random();
        List<int> listOfRandomNumbers = new List<int>();

        // minimum size of 10 entries, maximum size of 20 entries
        int sizeOfList = rand.Next(20, 30+1);

        for (int i = 0; i < sizeOfList; i++)
        {
            listOfRandomNumbers.Add(rand.Next());
        }

        return listOfRandomNumbers;
    }

    private List<String> createListOfRandomStrings() 
    {
        Random rand = new Random();
        List<String> listOfRandomStrings = new List<String>();

        // minimum size of 10 entries, maximum size of 20 entries
        int sizeOfList = rand.Next(10, 20+1);

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (int i = 0; i < sizeOfList; i++)
        {
            // creates a random String with a maximum size of 30
            listOfRandomStrings.Add(new string(Enumerable.Repeat(chars, 20).Select(s => s[rand.Next(s.Length)]).ToArray()));
        }

        return listOfRandomStrings;
    }
}
