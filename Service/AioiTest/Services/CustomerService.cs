using AioiTest.Entity;
using AioiTest.Model;
using AioiTest.Repository;

namespace AioiTest.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> AddCustomer(CustomerModel customer)
        {
            TbCustomer tbCustomer = MapDataModel.MapToTbCustomer(customer);
            await _customerRepository.Add(tbCustomer);
            return true;
        }

        public async Task<bool> DeleteCustomerById(int id)
        {
            await _customerRepository.Delete(id);
            return true;
        }

        public async Task<CustomerModel> GetCustomerById(int id)
        {
            TbCustomer result = await _customerRepository.GetById(id);
            if (result == null)
            {
                throw new Exception("Customer not found");
            }
            return new CustomerModel(result);
        }

        public async Task<List<CustomerModel>> GetCustomerList()
        {
            List<TbCustomer> result = await _customerRepository.GetList();
            if (result.Count <= 0)
            {
                return new List<CustomerModel>();
            }
            return MapDataModel.MapToCustomerModel(result);
        }

        public async Task<bool> UpdateCustomer(CustomerModel customer)
        {
            TbCustomer tbCustomer = MapDataModel.MapToTbCustomer(customer);
            await _customerRepository.Update(tbCustomer);
            return true;
        }
    }
}
