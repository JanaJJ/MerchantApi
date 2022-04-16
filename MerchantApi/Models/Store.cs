using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantApi.Models
{
    public class Store
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }
        [Required]
        public string StoreCode { get; set; }
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        //Many to One Relationship with Merchant
        [Required]
        public string MerchantCode { get; set; }
        //public int MerchantId { get; set; }
        //public Merchant Merchant { get; set; }
    }
}
