using MerchantApi.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantApi.Models.Response
{
    public class MerchantResponse
    {
        public ICollection<MerchantDto> Merchant { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
