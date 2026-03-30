using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAll()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .ToListAsync();
        // Include henter Customer sammen med bestillingen
        // ThenInclude går ett nivå dypere — henter Product
        // for hvert OrderItem
    }

    public async Task<Order?> GetById(int id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task Add(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}