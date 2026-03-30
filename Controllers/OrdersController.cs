using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        await _orderService.CreateOrder(
            request.CustomerId,
            request.Items.Select(i => (i.ProductId, i.Quantity)).ToList());
        // Vi konverterer request-items til tupler som OrderService forventer

        return Created("", null);
    }
}

// OrderItemRequest beskriver ett produkt i en bestilling
public record OrderItemRequest(int ProductId, int Quantity);
public record CreateOrderRequest(int CustomerId, List<OrderItemRequest> Items);