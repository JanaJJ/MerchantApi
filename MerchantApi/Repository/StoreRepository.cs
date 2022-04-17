using AutoMapper;
using MerchantApi.Database;
using MerchantApi.Dto;
using MerchantApi.Models;

namespace MerchantApi.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Merchant_StoreDbContext _merchant_storeDbContext;
        private readonly IMapper _mapper;
        public StoreRepository(Merchant_StoreDbContext merchant_StoreDbContext, IMapper mapper)
        {
            _merchant_storeDbContext = merchant_StoreDbContext;
            _mapper = mapper;
        }

        public void CreateStore(StoreDto _store)
        {
            var stores = _mapper.Map<Store>(_store);
            _merchant_storeDbContext.Stores.Add(stores);
            _merchant_storeDbContext.SaveChanges();
        }
       

        public bool DeleteStore(string storeCode)
        {
            var store = _merchant_storeDbContext.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefault();

            if (store == null)
            {
                return false;
            }
            _merchant_storeDbContext.Stores.Remove(store);
            _merchant_storeDbContext.SaveChanges();

            return true;
        }

        public Store GetStore(string storeCode)
        {
            var store = _merchant_storeDbContext.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefault();
            return store;
        }

        public IEnumerable<Store> GetStores()
        {
            return _merchant_storeDbContext.Stores.ToList();
        }

        public bool UpdateStore(string storeCode, Store store)
        {
            var storeFromDb = _merchant_storeDbContext.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefault();
            if (storeFromDb == null)
            {
                return false;
            }
            storeFromDb.StoreName = store.StoreName;
            storeFromDb.Address = store.Address;
            storeFromDb.PhoneNumber = store.PhoneNumber;
            storeFromDb.Email = store.Email;
        
            _merchant_storeDbContext.SaveChanges();
            return true;
        }
    }
}
