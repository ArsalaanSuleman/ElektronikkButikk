public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _repository.GetAll();
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task AddCustomer(string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-post kan ikke være tom");
        // Enkel validering — i virkeligheten ville du også
        // sjekket at e-posten er gyldig format og ikke
        // allerede er registrert i systemet

        var customer = new Customer
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber
        };

        await _repository.Add(customer);
    }
}