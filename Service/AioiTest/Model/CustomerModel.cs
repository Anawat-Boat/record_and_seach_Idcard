using AioiTest.Entity;
using AioiTest.Helper;

namespace AioiTest.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CitizenId { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CustomerModel() { }
        public CustomerModel(TbCustomer customer)
        {
            Id = customer.Id;
            FullName = customer.FullName;
            CitizenId = customer.CitizenId;
            Address = customer.Address;
            BirthDate = customer.BirthDate;
            CreatedDate = customer.CreatedDate;
            UpdatedDate = customer.UpdatedDate;
        }
    }
}
