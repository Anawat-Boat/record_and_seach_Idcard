using System.ComponentModel.DataAnnotations;

namespace AioiTest.Entity
{
    public class TbCustomer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string CitizenId { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedDate { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
    