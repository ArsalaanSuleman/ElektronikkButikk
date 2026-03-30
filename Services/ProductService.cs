public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IServiceBus _serviceBus;

    public ProductService(IProductRepository repository, IServiceBus serviceBus)
    {
        _repository = repository;
        _serviceBus = serviceBus;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _repository.GetAll();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task AddProduct(string name, string brand, decimal price, int stockQuantity)
    {
        if (price <= 0)
            throw new ArgumentException("Pris kan ikke være negativ eller null");
        if (stockQuantity < 0)
            throw new ArgumentException("Lagerbeholdning kan ikke være negativ");
        // Forretningsregler — hører hjemme her i Service-laget

        var product = new Product
        {
            Name = name,
            Brand = brand,
            Price = price,
            StockQuantity = stockQuantity
        };

        await _repository.Add(product);
    }

    public async Task UpdateStock(int productId, int newStock)
    {
        var product = await _repository.GetById(productId);
        if (product == null)
            throw new ArgumentException($"Produkt med id {productId} finnes ikke");

        product.StockQuantity = newStock;
        await _repository.Update(product);

        if (newStock < 5)
        {
            _serviceBus.Publish(new LowStockMessage
            {
                ProductId = product.Id,
                ProductName = product.Name,
                RemainingStock = newStock
            });
            // Sender varsel når lagerbeholdningen blir lav
            // Andre systemer kan lytte på denne meldingen
            // og f.eks. automatisk bestille flere varer
        }
    }

    public async Task DeleteProduct(int productId)
    {
        var product = await _repository.GetById(productId);
        if (product == null)
            throw new ArgumentException($"Produkt med id {productId} finnes ikke");

        await _repository.Delete(product);
    }
}