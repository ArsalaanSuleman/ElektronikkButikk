using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
        // ToListAsync er den asynkrone versjonen av ToList()
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FindAsync(id);
        // Returnerer null hvis produktet ikke finnes
        // det er derfor returtypen er Product? (nullable)
    }

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}