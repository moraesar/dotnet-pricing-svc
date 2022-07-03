using dotnet_pricing_svc.Data;
using dotnet_pricing_svc.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_pricing_svc.Controllers
{
    [ApiController]
    [Route("tickers")]
    public class TickerController : ControllerBase
    {
        private readonly PricingContext _dbContext;
        private readonly ILogger<TickerController> _logger;

        public TickerController(ILogger<TickerController> logger, PricingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{ticker}")]
        public ModelViewTicker Get(string ticker)
        {
            return new TickerRepository(_dbContext).GetOne(ticker);
        }

        [HttpPost(Name = "PostTicker")]
        public ActionResult<ModelViewTicker> Post(ModelViewTicker ticker)
        {
            Tuple<DbActionResponsesEnum, ModelViewTicker?> ret = new TickerRepository(_dbContext).Save(ticker);
            if (ret.Item1 == DbActionResponsesEnum.ItemAlreadyExists)
                return StatusCode(409);
            
            if (ret.Item1 != DbActionResponsesEnum.Ok)
                return StatusCode(500);
            
            return CreatedAtAction(null, null);
        }
    }
}