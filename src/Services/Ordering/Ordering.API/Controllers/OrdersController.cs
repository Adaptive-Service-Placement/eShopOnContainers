namespace Microsoft.eShopOnContainers.Services.Ordering.API.Controllers;

using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;
using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries;
using Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.Services;

[Route("api/v1/[controller]")]
[Authorize]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderQueries _orderQueries;
    private readonly IIdentityService _identityService;
    private readonly ILogger<OrdersController> _logger;
    private readonly IEventBus _eventBus;

    public OrdersController(
        IMediator mediator,
        IOrderQueries orderQueries,
        IIdentityService identityService,
        ILogger<OrdersController> logger,
        IEventBus eventBus)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _eventBus = eventBus;
    }

    // send random message here, figure out later how to trigger this task
    [Route("cancel")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
    {
        bool commandResult = false;

        if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
        {
            var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);

            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestCancelOrder.GetGenericTypeName(),
                nameof(requestCancelOrder.Command.OrderNumber),
                requestCancelOrder.Command.OrderNumber,
                requestCancelOrder);

            commandResult = await _mediator.Send(requestCancelOrder);
        }

        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Route("ship")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ShipOrderAsync([FromBody] ShipOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
    {
        bool commandResult = false;

        if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
        {
            var requestShipOrder = new IdentifiedCommand<ShipOrderCommand, bool>(command, guid);

            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestShipOrder.GetGenericTypeName(),
                nameof(requestShipOrder.Command.OrderNumber),
                requestShipOrder.Command.OrderNumber,
                requestShipOrder);

            commandResult = await _mediator.Send(requestShipOrder);
        }

        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Route("{orderId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetOrderAsync(int orderId)
    {
        try
        {
            //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
            //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
            var order = await _orderQueries.GetOrderAsync(orderId);

            return Ok(order);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderSummary>>> GetOrdersAsync()
    {
        var userid = _identityService.GetUserIdentity();
        var orders = await _orderQueries.GetOrdersFromUserAsync(Guid.Parse(userid));

        return Ok(orders);
    }

    [Route("cardtypes")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CardType>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<CardType>>> GetCardTypesAsync()
    {
        var cardTypes = await _orderQueries.GetCardTypesAsync();

        return Ok(cardTypes);
    }

    [Route("draft")]
    [HttpPost]
    public async Task<ActionResult<OrderDraftDTO>> CreateOrderDraftFromBasketDataAsync([FromBody] CreateOrderDraftCommand createOrderDraftCommand)
    {
        _logger.LogInformation(
            "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            createOrderDraftCommand.GetGenericTypeName(),
            nameof(createOrderDraftCommand.BuyerId),
            createOrderDraftCommand.BuyerId,
            createOrderDraftCommand);

        return await _mediator.Send(createOrderDraftCommand);
    }

    [HttpGet]
    [Route("payment")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendPaymentMessage()
    {
        // sends random message to Payment service
        var randomOrderingPaymentEvent = new RandomOrderingPaymentEvent("Hello Payment from Ordering", createListOfRandomNumbers(), createListOfRandomStrings());
        
        _eventBus.Publish(randomOrderingPaymentEvent);

        return Ok();
    }

    [HttpGet]
    [Route("catalog")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendCatalogMessage()
    {
        // sends random message to Payment service
        var randomOrderingCatalogEvent = new RandomOrderingCatalogEvent("Hello Catalog from Ordering", createListOfRandomNumbers(), createListOfRandomStrings());
        
        _eventBus.Publish(randomOrderingCatalogEvent);

        return Ok();
    }

    [HttpGet]
    [Route("webhook")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> sendWebhookMessage()
    {
        // sends random message to Payment service
        var randomOrderingWebhookEvent = new RandomOrderingWebhookEvent("Hello Webhook from Ordering", createListOfRandomNumbers(), createListOfRandomStrings());
        
        _eventBus.Publish(randomOrderingWebhookEvent);

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
