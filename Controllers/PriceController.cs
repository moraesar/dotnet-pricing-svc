using dotnet_pricing_svc.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_pricing_svc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly PrincingContext _dbContext;
        private readonly ILogger<PriceController> _logger;

        public PriceController(ILogger<PriceController> logger, PrincingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetPrice")]
        public IEnumerable<Price> Get()
        {
            // Ticker ticker = new Ticker() {TickerId = 1, Nome = "FLRY3", Tipo = "Acao"};
            // Price[] valores = new Price[2];
            // valores[0] = new Price() {PrecoId = 1, Data = new DateTime(2022, 4, 2), Valor = 16.12F, Ticker = ticker};
            // valores[1] = new Price() {PrecoId = 1, Data = new DateTime(2022, 4, 3), Valor = 14.15F, Ticker = ticker};
            if (_dbContext.Prices == null)
                return new List<Price>();
            
            return _dbContext.Prices.Select(p => p);
        }
    }
}