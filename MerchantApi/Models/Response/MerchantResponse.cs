namespace MerchantApi.Models.Response
{
    public class MerchantResponse
    {
        public ICollection<Merchant> Merchant { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
