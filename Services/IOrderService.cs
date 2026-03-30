public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task<Order?> GetOrderById(int id);
    Task CreateOrder(int customerId, List<(int ProductId, int Quantity)> items);
    // List<(int ProductId, int Quantity)> er en liste av tupler
    // — en enkel måte å sende inn flere produkt/antall par
}