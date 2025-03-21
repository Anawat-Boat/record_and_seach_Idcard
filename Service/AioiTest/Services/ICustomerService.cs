using AioiTest.Model;

namespace AioiTest.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerById(int id);
        Task<List<CustomerModel>> GetCustomerList();
        Task<bool> AddCustomer(CustomerModel customer);
        Task<bool> UpdateCustomer(CustomerModel customer);
        Task<bool> DeleteCustomerById(int id);
    }
}
