using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantApi.Models
{
    public class Merchant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MerchantId  { get; set; }
        [Required]
        public string MerchantCode { get; set; }
        public string Name { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Website { get; set; }
        [Required]
        public string AccountNum { get; set; }

        //One to Many Relationship with Store
        public ICollection<Store> Stores { get; set; }

    }
}
