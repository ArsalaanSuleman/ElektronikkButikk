public interface ICustomerRepository
{
    Task<List<Customer>> GetAll();
    Task<Customer?> GetById(int id);
    Task Add(Customer customer);
}