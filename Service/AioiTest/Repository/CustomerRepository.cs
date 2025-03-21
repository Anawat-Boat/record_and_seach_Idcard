using AioiTest.Entity;
using Microsoft.EntityFrameworkCore;

namespace AioiTest.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDBContext _context;
        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task Add(TbCustomer customer)
        {
            _context.TbCustomers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var customer = await GetById(id);
            if (customer == null) {
                return;
            }
            _context.TbCustomers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<TbCustomer> GetById(int id)
        {
            return await _context.TbCustomers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TbCustomer>> GetList()
        {
            return await _context.TbCustomers.ToListAsync();
        }

        public async Task Update(TbCustomer customer)
        {   
            _context.TbCustomers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
