using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
// [controller] byttes automatisk ut med "products"
// så alle endepunkter starter med /api/products
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
        // DI igjen! Controlleren ber om IProductService
        // — den vet ikke hvilken implementasjon den får
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
        // Ok() returnerer HTTP 200 med produktlisten som JSON
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
            return NotFound();
            // NotFound() returnerer HTTP 404

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProductRequest request)
    {
        await _productService.AddProduct(
            request.Name,
            request.Brand,
            request.Price,
            request.StockQuantity);

        return Created("", null);
        // Created() returnerer HTTP 201 — standard for POST
    }

    [HttpPut("{id}/stock")]
    public async Task<IActionResult> UpdateStock(int id, UpdateStockRequest request)
    {
        await _productService.UpdateStock(id, request.NewStock);
        return NoContent();
        // NoContent() returnerer HTTP 204 — vellykket men ingen data å returnere
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }
}

// Request-klasser — beskriver hva API-et forventer å få inn
public record AddProductRequest(string Name, string Brand, decimal Price, int StockQuantity);
public record UpdateStockRequest(int NewStock);