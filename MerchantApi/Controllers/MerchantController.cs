using AutoMapper;
using MerchantApi.Dto;
using MerchantApi.Models;
using MerchantApi.Models.Response;
using MerchantApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
        ////response  200
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantDto))]
        public ActionResult<MerchantResponse> GetMerchants([FromQuery] int? page, string? firstName)
        {
           if (!page.HasValue || page == 0)
               page = 1;

            var merchants = _merchantRepository.GetMerchants(page.Value, firstName);
            return Ok(merchants);
        }



        [HttpPost]
        //responses 201 and 400
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MerchantDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateMerchant([FromBody] MerchantDto merchant)
        {
           var reviewMap=_mapper.Map<Merchant>(merchant);
            _merchantRepository.CreateMerchant(reviewMap);
            return Ok();

        }


        //merchants/{merchant-code}:get
        //respones 200 and 404
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{merchantCode}")]
        public ActionResult GetMerchantbyId([FromRoute] string merchantCode)
        {
            var merchant = _mapper.Map<MerchantDto>(_merchantRepository.GetMerchant(merchantCode));
            if (merchant == null)
            {
                return NotFound(); //404 not found
            }
            return Ok(merchant); //200ok,found

        }

        //merchants/{merchant-code}:put
        //responses 200 and 400
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantDto))]
        [ProducesResponseType(typeof(object), 400)]
        [HttpPut("{merchantCode}")]
        public ActionResult UpdateMerchant([FromRoute] string merchantCode, [FromBody] MerchantDto merchant)
        {
         
            var result = _mapper.Map<Merchant>(merchant);
            var updateMerchant=_merchantRepository.UpdateMerchant(merchantCode, result);
            if (!ModelState.IsValid)
            return BadRequest(ModelState); //400 bad request
            return Ok(updateMerchant); //200,ok
        }

        //merchants/{merchant-code}:delete
        //responses 200 and 400
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), 400)]
        [HttpDelete("{merchantCode}")]
        public ActionResult DeleteMerchant([FromRoute] string merchantCode)
        {
            var merchant = _merchantRepository.DeleteMerchant(merchantCode);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400
            if (!merchant)
            {
                ModelState.AddModelError("", "Something went wrong deleting merchant");
            }
            return Ok(merchant); //200,ok
        }

        ///merchants/{merchant-code}/stores:get
        ///responses 200
        [HttpGet("{merchantCode}/stores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDto))]
        public ActionResult GetStoresByMerchantId([FromRoute] string merchantCode)
        {
            var stores=_merchantRepository.GetStoresbyMerchantCode(merchantCode);
            return Ok(stores); 
        }
        

        ///merchants/{merchant-code}/stores:post
        ///responses 201 and 400
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{merchantCode}/stores")]
        public ActionResult CreateStoreFromMerchantCode([FromRoute] string MerchantCode, [FromBody] StoreDto store)
        {
            _storeRepository.CreateStore(store);
            return Ok();

        }
        

        ///merchants/{merchant-code}/stores/{store-code}:get
        ///200 and 404
        [HttpGet("{merchantCode}/stores/{storeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStoreInfo([FromRoute] string MerchantCode, [FromRoute] string storeCode)
        {
            var store = _mapper.Map<StoreDto>(_merchantRepository.GetStoreInfo(MerchantCode, storeCode));
            if (store == null)
            {
                return NotFound(); //404
            }
            return Ok(store);
        }

        ///merchants/{merchant-code}/stores/{store-code}:put
        ///200 and 400
        [HttpPut("{merchantCode}/stores/{storeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDto))]
        [ProducesResponseType(typeof(object), 400)]
        public ActionResult UpdateStore([FromRoute] string MerchantCode, [FromRoute] string storeCode, [FromBody] StoreDto store)
        {
            var result = _mapper.Map<Store>(store);
            _merchantRepository.UpdateStore(MerchantCode, storeCode,result);
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400
            return Ok();
        }

        ///merchants/{merchant-code}/stores/{store-code}:delete
        ///200 and 400
        [HttpDelete("{merchantCode}/stores/{storeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), 400)]
        public ActionResult DeleteStore([FromRoute] string MerchantCode, [FromRoute] string storeCode)
        {
            var store = _merchantRepository.DeleteStore(MerchantCode, storeCode);
            if (!store)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }


    }
}
