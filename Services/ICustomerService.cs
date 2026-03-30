public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(int id);
    Task AddCustomer(string name, string email, string phoneNumber);
}