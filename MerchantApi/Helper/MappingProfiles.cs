using AutoMapper;
using MerchantApi.Dto;
using MerchantApi.Models;
using MerchantApi.Models.Response;

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
            CreateMap<MerchantResponse, MerchantResponseDto>();
            CreateMap<MerchantResponseDto, MerchantResponse>();
        }
    }
}
