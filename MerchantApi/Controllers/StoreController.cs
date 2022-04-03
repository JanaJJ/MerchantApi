using MerchantApi.Models;
using MerchantApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MerchantApi.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            return _storeRepository.GetStores().ToList();
        }
        [HttpPost]
        public ActionResult CreateStore([FromBody] Store store)
        {
            _storeRepository.CreateStore(store);
            return Ok();
        }
    }
}
