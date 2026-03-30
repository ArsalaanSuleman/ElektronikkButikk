public interface IOrderRepository
{
    Task<List<Order>> GetAll();
    Task<Order?> GetById(int id);
    Task Add(Order order);
}