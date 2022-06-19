using dotnet_pricing_svc.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Data
{
    public class PriceRepository : IRepository<ModelViewPrice>
    {
        private readonly PricingContext _dbContext;

        public PriceRepository(PricingContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public ICollection<ModelViewPrice> GetAll(string tickerName)
        {
            if (_dbContext.Prices == null)
                return new List<ModelViewPrice>();
            
            return _dbContext.Prices
                .Where(p => p.Ticker.Name == tickerName)
                .Select(p => new ModelViewPrice() { 
                    TickerName = p.Ticker.Name,
                    Date = p.Date,
                    Price = p.Value
                }).ToList();
        }

        public Tuple<DbActionResponsesEnum, ModelViewPrice?> Save(ModelViewPrice price)
        {
            ModelViewPrice? mvp = null;

            if (_dbContext.Prices == null || _dbContext.Tickers == null)
                return Tuple.Create(DbActionResponsesEnum.CannotAcessContext, mvp);
            
            try
            {
                Ticker? t = _dbContext.Tickers.FirstOrDefault(t => t.Name == price.TickerName);

                if (t == null)
                    return Tuple.Create(DbActionResponsesEnum.CannotAcessContext, mvp);

                Price p = new Price() {
                    Date = price.Date,
                    Value = price.Price,
                    TickerId = t.TickerId,
                };
                _dbContext.Prices.Add(p);
                _dbContext.SaveChanges();
                mvp = new ModelViewPrice() { Date = p.Date, Price = p.Value, TickerName = p.Ticker.Name };

                return Tuple.Create(DbActionResponsesEnum.Ok, (ModelViewPrice?) mvp);
            }
            catch (DbUpdateException)
            {
                return Tuple.Create(DbActionResponsesEnum.ItemAlreadyExists, (ModelViewPrice?) mvp);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Tuple.Create(DbActionResponsesEnum.GenericError, mvp);
            }
        }
    }
}