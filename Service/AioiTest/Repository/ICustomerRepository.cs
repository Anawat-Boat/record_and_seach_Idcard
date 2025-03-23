using AioiTest.Entity;

namespace AioiTest.Repository
{
    public interface ICustomerRepository
    {
        Task<List<TbCustomer>> GetList(string search);
        Task<TbCustomer> GetById(int id);
        Task Add(TbCustomer customer);
        Task Update(int id, TbCustomer customer);
        Task Delete(int id);
    }
}
