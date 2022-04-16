using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantApi.Dto
{
    public class StoreDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }
        [Required]
        public string StoreCode { get; set; }
        public string MerchantCode { get; set; }
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
