using AutoMapper;
using MerchantApi.Database;
using MerchantApi.Dto;
using MerchantApi.Models;
using MerchantApi.Models.Response;

namespace MerchantApi.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly Merchant_StoreDbContext _merchant_storeDbContext;
        private readonly IMapper _mapper;
        public MerchantRepository(Merchant_StoreDbContext merchant_StoreDbContext, IMapper mapper)
        {
            _merchant_storeDbContext = merchant_StoreDbContext;
            _mapper = mapper;
        }


        // RETURN ALL MERCHANTS
        public MerchantResponse GetMerchants(int page, string? firstName)
        {
            var defaultPageSize = 10f;
            var merchants = _mapper.Map<List<MerchantDto>>(_merchant_storeDbContext.Merchants.ToList());

            var pageCount = Math.Ceiling(merchants.Count / defaultPageSize);

            if (!string.IsNullOrEmpty(firstName) && merchants.Count > 0)
            {
                merchants = merchants.Where(x => x.Name == firstName).ToList();
                pageCount = Math.Ceiling(merchants.Count / defaultPageSize);
            }

            var merchantsPaged = merchants.Skip((page - 1) * (int)defaultPageSize).Take((int)defaultPageSize).ToList();

            MerchantResponse merchantResponse = new MerchantResponse
            {
                Merchant = merchantsPaged,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return merchantResponse;
        }

        //GET MERCHANT BY ID
        public Merchant GetMerchant(string merchantCode)
        {
            var merchant = _merchant_storeDbContext.Merchants.Where(x => x.MerchantCode == merchantCode).FirstOrDefault();
            return merchant;
        }

        //CREATE MERCHANT
        public bool CreateMerchant(Merchant merchant)
        {
            _merchant_storeDbContext.Merchants.Add(merchant);
            return Save();
        }


        //UPDATE MERCHANT
        public bool UpdateMerchant(string merchantCode, Merchant merchant)
        {
            var merchantFromDb = _merchant_storeDbContext.Merchants.Where(x => x.MerchantCode == merchantCode).FirstOrDefault();
            if (merchantFromDb == null)
            {
                return false;
            }
            merchantFromDb.Name = merchant.Name;
            merchantFromDb.FullName = merchant.FullName;
            merchantFromDb.Address = merchant.Address;
            merchantFromDb.PhoneNumber = merchant.PhoneNumber;
            merchantFromDb.Email = merchant.Email;
            merchantFromDb.Website = merchant.Website;
            merchantFromDb.AccountNum = merchant.AccountNum;
            

            _merchant_storeDbContext.SaveChanges();
            return true;
        }

        //DELETE MERCHANT
        public bool DeleteMerchant(string merchantCode)
        {
            var merchant = _merchant_storeDbContext.Merchants.Where(x => x.MerchantCode == merchantCode).FirstOrDefault();

            if (merchant == null)
            {
                return false;
            }
            _merchant_storeDbContext.Merchants.Remove(merchant);
            _merchant_storeDbContext.SaveChanges();
            
            return true;
        }

        
        //LIST STORES BY MERCHANT ID
        public ICollection<Store> GetStoresbyMerchantCode(string MerchantCode)
        {
            return _merchant_storeDbContext.Stores.Where(e => e.MerchantCode == MerchantCode).ToList();
        }
       

        //SAVE
        public bool Save()
        {
            var saved = _merchant_storeDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        //CREATE STORE FROM MERCHANT ID
        public bool CreateStore(Store store)
        {
            _merchant_storeDbContext.Add(store);
            return Save();
        }

        // UPDATE STORE
        public bool UpdateStore(string MerchantCode,string storeCode, Store store)
        {
            _merchant_storeDbContext.Stores.Where(e => e.MerchantCode == MerchantCode).ToList();
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

        // Retrieves information for a single store
        public Store GetStoreInfo(string MerchantCode,string storeCode)
        {
            _merchant_storeDbContext.Stores.Where(e => e.MerchantCode == MerchantCode).ToList();
            var store = _merchant_storeDbContext.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefault();
            return store;
        }

        //Delete Store
        public bool DeleteStore(string MerchantCode, string storeCode)
        {
            _merchant_storeDbContext.Stores.Where(e => e.MerchantCode == MerchantCode).ToList();
            var store = _merchant_storeDbContext.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefault();

            if (store == null)
            {
                return false;
            }
            _merchant_storeDbContext.Stores.Remove(store);
            _merchant_storeDbContext.SaveChanges();

            return true;
        }
    }
}
