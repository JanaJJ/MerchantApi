using AutoMapper;
using MerchantApi.Dto;
using MerchantApi.Models;

namespace MerchantApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Merchant,MerchantDto>();
            CreateMap<MerchantDto,Merchant>();
            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto,Store>();
        }
    }
}
