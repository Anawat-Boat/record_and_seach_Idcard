using System.ComponentModel.DataAnnotations;

namespace AioiTest.Entity
{
    public class TbCustomer
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CitizenId { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
    