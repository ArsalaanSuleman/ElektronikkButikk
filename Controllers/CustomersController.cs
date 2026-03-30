using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomers();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerById(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCustomerRequest request)
    {
        await _customerService.AddCustomer(
            request.Name,
            request.Email,
            request.PhoneNumber);

        return Created("", null);
    }
}

public record AddCustomerRequest(string Name, string Email, string PhoneNumber);