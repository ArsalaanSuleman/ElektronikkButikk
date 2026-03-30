public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task AddProduct(string name, string brand, decimal price, int stockQuantity);
    Task UpdateStock(int productId, int newStock);
    Task DeleteProduct(int productId);
}