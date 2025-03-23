using AioiTest.Entity;
using AioiTest.Helper;
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
            if (!Validate.IsValidThaiCitizenId(customer.CitizenId))
            {
                throw new Exception("Invalid Citizen ID");
            }
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

        public async Task<List<CustomerModel>> GetCustomerList(string? search)
        {
            List<TbCustomer> result = await _customerRepository.GetList(search);
            if (result.Count <= 0)
            {
                return new List<CustomerModel>();
            }
            return MapDataModel.MapToCustomerModel(result);
        }

        public async Task<bool> UpdateCustomer(int Id, CustomerModel customer)
        {
            if (!Validate.IsValidThaiCitizenId(customer.CitizenId))
            {
                throw new Exception("Invalid Citizen ID");
            }
            TbCustomer tbCustomer = MapDataModel.MapToTbCustomer(customer);

            await _customerRepository.Update(Id, tbCustomer);
            return true;
        }
    }
}
