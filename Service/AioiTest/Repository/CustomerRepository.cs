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
            _context.TbCustomers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<TbCustomer> GetById(int id)
        {
            var customer = await _context.TbCustomers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Not found customer");
            }
            return customer;
        }

       
        public async Task<List<TbCustomer>> GetList(string search)
        {
            var query = _context.TbCustomers.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.FullName.Trim().ToLower().Contains(search.Trim().ToLower()) || c.CitizenId.Contains(search.Trim().ToLower()));
            }
            return await query.ToListAsync();
        }
        public async Task Update(int id, TbCustomer customer)
        {
            var customerUpdate = await GetById(id);
            if (customerUpdate == null) {
                return;
            }
            customerUpdate.BirthDate = customer.BirthDate;
            customerUpdate.CitizenId = customer.CitizenId;
            customerUpdate.Address = customer.Address;
            customerUpdate.FullName = customer.FullName;
            customerUpdate.UpdatedDate = DateTime.Now.ToUniversalTime();
            await _context.SaveChangesAsync();
        }
    }
}
