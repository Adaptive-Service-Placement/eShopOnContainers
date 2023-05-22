namespace Webhooks.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WebhooksController : ControllerBase
{
    private readonly WebhooksContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IGrantUrlTesterService _grantUrlTester;
    private readonly IEventBus _eventBus;

    public WebhooksController(WebhooksContext dbContext, IIdentityService identityService, IGrantUrlTesterService grantUrlTester, IEventBus eventBus)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _grantUrlTester = grantUrlTester;
        _eventBus = eventBus;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WebhookSubscription>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ListByUser()
    {
        var userId = _identityService.GetUserIdentity();
        var data = await _dbContext.Subscriptions.Where(s => s.UserId == userId).ToListAsync();
        return Ok(data);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(WebhookSubscription), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByUserAndId(int id)
    {
        var userId = _identityService.GetUserIdentity();
        var subscription = await _dbContext.Subscriptions.SingleOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        if (subscription != null)
        {
            return Ok(subscription);
        }
        return NotFound($"Subscriptions {id} not found");
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(418)]
    public async Task<IActionResult> SubscribeWebhook(WebhookSubscriptionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var grantOk = await _grantUrlTester.TestGrantUrl(request.Url, request.GrantUrl, request.Token ?? string.Empty);

        if (grantOk)
        {
            var subscription = new WebhookSubscription()
            {
                Date = DateTime.UtcNow,
                DestUrl = request.Url,
                Token = request.Token,
                Type = Enum.Parse<WebhookType>(request.Event, ignoreCase: true),
                UserId = _identityService.GetUserIdentity()
            };

            _dbContext.Add(subscription);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetByUserAndId", new { id = subscription.Id }, subscription);
        }
        else
        {
            return StatusCode(418, "Grant url can't be validated");
        }
    }

    // send random message here, figure out later how to trigger this task
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UnsubscribeWebhook(int id)
    {
        var userId = _identityService.GetUserIdentity();
        var subscription = await _dbContext.Subscriptions.SingleOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (subscription != null)
        {
            _dbContext.Remove(subscription);
            await _dbContext.SaveChangesAsync();
            return Accepted();
        }

        return NotFound($"Subscriptions {id} not found");
    }

    [HttpGet]
    [Route("ordering")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendOrderingMessage()
    {
        // sends random message to Ordering service
        var randomWebhookOrderingEvent = new RandomWebhookOrderingEvent("Hello Ordering from Webhook", createListOfRandomNumbers(), createListOfRandomStrings());

        _eventBus.Publish(randomWebhookOrderingEvent);

        return Ok();
    }
    
    [HttpGet]
    [Route("payment")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendPaymentMessage()
    {
        // sends random message to Payment service
        var randomWebhookPaymentEvent = new RandomWebhookPaymentEvent("Hello Payment from Webhook", createListOfRandomNumbers(), createListOfRandomStrings());

        _eventBus.Publish(randomWebhookPaymentEvent);

        return Ok();
    }

    [HttpGet]
    [Route("catalog")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendCatalogMessage()
    {
        // sends random message to Catalog service
        var randomWebhookCatalogEvent = new RandomWebhookCatalogEvent("Hello Catalog from Webhook", createListOfRandomNumbers(), createListOfRandomStrings());

        _eventBus.Publish(randomWebhookCatalogEvent);
        
        return Ok();
    }

    [HttpGet]
    [Route("signal")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendSignalMessage()
    {
        // sends random message to Webhook service
        var randomWebhookSignalEvent = new RandomWebhookSignalEvent("Hello Signal from Webhook", createListOfRandomNumbers(), createListOfRandomStrings());

        _eventBus.Publish(randomWebhookSignalEvent);

        return Ok();
    }

    [HttpGet]
    [Route("background")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendBackgroundMessage()
    {
        // sends random message to Webhook service
        var randomWebhookBackgroundEvent = new RandomWebhookBackgroundEvent("Hello Background from Webhook", createListOfRandomNumbers(), createListOfRandomStrings());

        _eventBus.Publish(randomWebhookBackgroundEvent);

        return Ok();
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
