using AutoMapper;
using MerchantApi.Dto;
using MerchantApi.Models;
using MerchantApi.Models.Response;
using MerchantApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MerchantApi.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public MerchantController(IMerchantRepository merchantRepository, IStoreRepository storeRepository,IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        //response  200
        [ProducesResponseType(200, Type = typeof(IEnumerable<Merchant>))]
        public ActionResult<MerchantResponse> GetMerchants([FromQuery] int? page, string? firstName)
        {
            if (!page.HasValue || page == 0)
                page = 1;

            return _merchantRepository.GetMerchants(page.Value, firstName);
        }

        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Merchant>))]
        //public ActionResult GetAllMerchants()
        //{
        //    var merchants = _mapper.Map<List<MerchantDto>>(_merchantRepository.GetAll());
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return Ok();

        //}


        [HttpPost] 
        //responses 201 and 400
        [ProducesResponseType(400)]
        //public ActionResult CreateMerchant([FromBody]Merchant merchant)
        //{
        //   _merchantRepository.CreateMerchant(merchant);
        //    return Ok();
        //}
        public ActionResult CreateMerchant([FromBody] MerchantDto merchant)
        {
           var reviewMap=_mapper.Map<Merchant>(merchant);
            _merchantRepository.CreateMerchant(reviewMap);
            return Ok();

        }


        //merchants/{merchant-code}:get
        //respones 200 and 404
        [HttpGet("{merchantCode}")]
        public ActionResult GetMerchantbyId([FromRoute] string merchantCode)
        {
            var merchant = _mapper.Map<MerchantDto>(_merchantRepository.GetMerchant(merchantCode));
            if (merchant == null)
            {
                return NotFound();
            }
            return Ok(merchant);

        }

        //merchants/{merchant-code}:put
        //responses 200 and 400
        [HttpPut("{merchantCode}")]
        public ActionResult UpdateMerchant([FromRoute] string merchantCode, [FromBody] MerchantDto merchant)
        {
         
            var result = _mapper.Map<Merchant>(merchant);
            _merchantRepository.UpdateMerchant(merchantCode, result);
            return Ok();
            //var result = _merchantRepository.UpdateMerchant(merchantCode, merchant);
            //if (!result)
            //{
            //    return NotFound();
            //}
            //return Ok();
        }

        //merchants/{merchant-code}:delete
        //responses 200 and 400
        [HttpDelete("{merchantCode}")]
        public ActionResult DeleteMerchant([FromRoute] string merchantCode)
        {
            var merchant = _merchantRepository.DeleteMerchant(merchantCode);
            if (!merchant)
            {
                return NotFound();
            }

            return Ok();
        }

        ///merchants/{merchant-code}/stores:get
        ///responses 200
        [HttpGet("{merchantCode}/stores")]
        public ActionResult GetStoresByMerchantId(string merchantCode)
        {
            var stores=_mapper.Map<List<StoreDto>>(_merchantRepository.GetStoresbyMerchantCode(merchantCode));
            return Ok(stores); 
        }

        ///merchants/{merchant-code}/stores:post
        ///responses 201 and 400
        [HttpPost("{merchantCode}/stores")]
        public ActionResult CreateStoreFromMerchantCode([FromRoute] string merchantCode, [FromBody] StoreDto store)
        {
            var createStores = _mapper.Map<Store>(store);
            createStores.Merchant=_merchantRepository.GetMerchant(merchantCode);
            _merchantRepository.CreateStore(createStores);
            return Ok();
        }

        ///merchants/{merchant-code}/stores/{store-code}:get
        ///200 and 404
        [HttpGet("{merchantCode}/stores/{storeCode}")]
        public ActionResult GetStoreInfo([FromRoute] string merchantCode, [FromRoute] string storeCode)
        {
            var store = _mapper.Map<StoreDto>(_merchantRepository.GetStoreInfo(merchantCode,storeCode));
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        ///merchants/{merchant-code}/stores/{store-code}:put
        ///200 and 400
        [HttpPut("{merchantCode}/stores/{storeCode}")]
        public ActionResult UpdateStore([FromRoute] string merchantCode, [FromRoute] string storeCode, [FromBody] StoreDto store)
        {
            var result = _mapper.Map<Store>(store);
            _merchantRepository.UpdateStore(merchantCode,storeCode,result);
            return Ok();
        }

        ///merchants/{merchant-code}/stores/{store-code}:delete
        ///200 and 400
        [HttpDelete("{merchantCode}/stores/{storeCode}")]
        public ActionResult DeleteStore([FromRoute] string merchantCode, [FromRoute] string storeCode)
        {
            var store = _merchantRepository.DeleteStore(merchantCode,storeCode);
            if (!store)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
