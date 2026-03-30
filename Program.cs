using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==================
// REGISTRER TJENESTER
// ==================

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = 
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        // Forteller JSON-serialiseren at den skal ignorere
        // sirkler istedenfor å krasje
    });
// Forteller ASP.NET Core at vi bruker Controllers

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Swagger gir oss et grafisk grensesnitt for å teste API-et
// — vi åpner det i nettleseren etterpå

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=elektronikk.db"));
// Databasen vår

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// Scoped = samme instans innenfor én HTTP-request

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSingleton<IServiceBus, InMemoryServiceBus>();
// Singleton = én delt instans for hele appen

var app = builder.Build();

// ==================
// LAG DATABASEN
// ==================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    // Lager tabellene hvis de ikke finnes
}

// ==================
// MIDDLEWARE
// ==================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Swagger UI er kun tilgjengelig i utviklingsmodus
}

//app.UseHttpsRedirection();
app.MapControllers();
// Forteller appen om å bruke Controllers vi har laget

app.Run();