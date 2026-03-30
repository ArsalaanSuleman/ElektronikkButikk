using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetById(int id)
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
        // Include henter Orders sammen med kunden i én spørring
        // uten Include ville Orders alltid vært en tom liste
    }

    public async Task Add(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }
}