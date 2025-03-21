using AioiTest.Entity;
using AioiTest.Helper;

namespace AioiTest.Model
{
    public class MapDataModel
    {
        public static TbCustomer MapToTbCustomer(CustomerModel customer)
        {
            return new TbCustomer
            {
                Id = customer.Id,
                FullName = customer.FullName,
                CitizenId = EncryptionHelper.Encrypt(customer.CitizenId),
                Address = customer.Address,
                BirthDate = customer.BirthDate
            };
        }

        public static List<TbCustomer> MapToTbCustomers(List<CustomerModel> customers)
        {
            List<TbCustomer> reuslt = new List<TbCustomer>();
            foreach (var customer in customers)
            {
                reuslt.Add(MapToTbCustomer(customer));
            }
            return reuslt;
        }
        
        public static List<CustomerModel> MapToCustomerModel(List<TbCustomer> customers)
        {
            List<CustomerModel> reuslt = new List<CustomerModel>();
            foreach (var customer in customers)
            {
                reuslt.Add(new CustomerModel(customer));
            }
            return reuslt;
        }


    }
}
