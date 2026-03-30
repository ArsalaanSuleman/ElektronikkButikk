using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Customer> Customers {get; set;} = null!;
    public DbSet<Order> Orders {get; set;} = null!;
    public DbSet<OrderItem> OrderItems {get; set;} = null!;
}

/*
    I det forrige prosjektet hadde vi alt i Program.cs — det fungerer for læring, 
    men i virkeligheten har hver klasse sin egen fil. 
    Da er det enkelt å finne frem og jobbe i team.
*/