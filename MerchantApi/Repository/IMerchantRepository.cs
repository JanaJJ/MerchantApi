using MerchantApi.Models;
using MerchantApi.Models.Response;

namespace MerchantApi.Repository
{
    public interface IMerchantRepository
    {
        public ICollection<Merchant> GetAll();
        public MerchantResponse GetMerchants(int page,string? firstName);
        public Merchant GetMerchant(string merchantCode);
        bool CreateMerchant(Merchant merchant);
        public bool UpdateMerchant(string merchantCode, Merchant merchant);
        public bool DeleteMerchant(string merchantCode);
        ICollection<Store> GetStoresbyMerchantCode(string MerchantCode);
        bool CreateStore(Store store);
        public Store GetStoreInfo(string MerchantCode, string storeCode);
        public bool UpdateStore(string MerchantCode, string storeCode, Store store);
        public bool DeleteStore(string MerchantCode, string storeCode);
        bool Save();
    }
}
