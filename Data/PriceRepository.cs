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
        
        public ModelViewPrice GetOne(string tickerName, DateTime date)
        {
            var mvPrice = new ModelViewPrice();
            var price = _dbContext.Prices
                        .Where(p => p.Ticker.Name == tickerName && p.Date == date)
                        .Include(p => p.Ticker)
                        .FirstOrDefault();
            
            if (price != null)
            {
                mvPrice.TickerName = price.Ticker.Name;
                mvPrice.Date = price.Date;
                mvPrice.Price = price.Value;
            }

            return mvPrice;
        }
        
        public ICollection<ModelViewPrice> GetAll(string tickerName)
        {
            return _dbContext.Prices
                .Where(p => p.Ticker.Name == tickerName)
                .Select(p => new ModelViewPrice() { 
                    TickerName = p.Ticker.Name,
                    Date = p.Date,
                    Price = p.Value
                })
                .OrderByDescending(p => p.Date)
                .Take(10)
                .ToList();
        }

        public Tuple<DbActionResponsesEnum, ModelViewPrice?> Save(ModelViewPrice value)
        {
            ModelViewPrice? mvp = null;

            try
            {
                Ticker? t = _dbContext.Tickers.FirstOrDefault(t => t.Name == value.TickerName);

                if (t == null)
                    return Tuple.Create(DbActionResponsesEnum.CannotAcessContext, mvp);

                Price p = new Price() {
                    Date = value.Date,
                    Value = value.Price,
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