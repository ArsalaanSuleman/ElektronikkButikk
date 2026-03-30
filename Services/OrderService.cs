public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IServiceBus _serviceBus;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IServiceBus serviceBus)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _serviceBus = serviceBus;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAll();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _orderRepository.GetById(id);
    }

    public async Task CreateOrder(int customerId, List<(int ProductId, int Quantity)> items)
    {
        var customer = await _customerRepository.GetById(customerId);
        if (customer == null)
            throw new ArgumentException($"Kunde med id {customerId} finnes ikke");

        var order = new Order { CustomerId = customerId };
        decimal totalAmount = 0;

        foreach (var (productId, quantity) in items)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
                throw new ArgumentException($"Produkt med id {productId} finnes ikke");

            if (product.StockQuantity < quantity)
                throw new ArgumentException($"{product.Name} har ikke nok på lager");
            // Sjekker at vi har nok på lager før vi oppretter bestillingen

            order.Items.Add(new OrderItem
            {
                ProductId = productId,
                Quantity = quantity,
                PriceAtPurchase = product.Price
                // Vi lagrer prisen slik den er nå
            });

            product.StockQuantity -= quantity;
            await _productRepository.Update(product);
            // Oppdaterer lagerbeholdningen

            totalAmount += product.Price * quantity;
        }

        await _orderRepository.Add(order);

        _serviceBus.Publish(new OrderCreatedMessage
        {
            OrderId = order.Id,
            CustomerId = customerId,
            TotalAmount = totalAmount
        });
        // Sender melding om at en bestilling er opprettet
    }
}