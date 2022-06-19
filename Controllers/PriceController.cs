using dotnet_pricing_svc.Data;
using dotnet_pricing_svc.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_pricing_svc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly PricingContext _dbContext;
        private readonly ILogger<PriceController> _logger;

        public PriceController(ILogger<PriceController> logger, PricingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetPrice")]
        public IEnumerable<ModelViewPrice> Get(string ticker)
        {
            return new PriceRepository(_dbContext).GetAll(ticker);
        }

        [HttpPost(Name = "PostPrice")]
        public ActionResult<ModelViewPrice> Post(ModelViewPrice price)
        {
            Tuple<DbActionResponsesEnum, ModelViewPrice?> ret = new PriceRepository(_dbContext).Save(price);
             if (ret.Item1 == DbActionResponsesEnum.ItemAlreadyExists)
                return StatusCode(409);
            
            if (ret.Item1 != DbActionResponsesEnum.Ok)
                return StatusCode(500);
           
            return CreatedAtAction(null, null);
        }
    }
}