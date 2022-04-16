using MerchantApi.Dto;
using MerchantApi.Models;

namespace MerchantApi.Repository
{
    public interface IStoreRepository
    {
        public IEnumerable<Store> GetStores();
        public Store GetStore(string storeCode);
        public void CreateStore(StoreDto store);
        public bool UpdateStore(string storeCode, Store store);
        public bool DeleteStore(string storeCode);
    }
}
