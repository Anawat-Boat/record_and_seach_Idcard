using AioiTest.Entity;

namespace AioiTest.Repository
{
    public interface ICustomerRepository
    {
        Task<List<TbCustomer>> GetList();
        Task<TbCustomer> GetById(int id);
        Task Add(TbCustomer customer);
        Task Update(TbCustomer customer);
        Task Delete(int id);
    }
}
