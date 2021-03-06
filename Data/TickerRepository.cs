using dotnet_pricing_svc.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Data
{
    public class TickerRepository : IRepository<ModelViewTicker>
    {
        private readonly PricingContext _dbContext;

        public TickerRepository(PricingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelViewTicker GetOne(string tickerName)
        {
            var mvTicker = new ModelViewTicker();
            var ticker = _dbContext.Tickers
                        .Where(t => t.Name == tickerName)
                        .FirstOrDefault();
            
            if (ticker != null)
            {
                mvTicker.Name = ticker.Name;
                mvTicker.Type = ticker.Type;
            }

            return mvTicker;
        }

        public Tuple<DbActionResponsesEnum, ModelViewTicker?> Save(ModelViewTicker value)
        {
            ModelViewTicker? mvt = null;

            if (_dbContext.Tickers == null)
                return Tuple.Create(DbActionResponsesEnum.CannotAcessContext, mvt);
            
            try
            {
                Ticker t = new Ticker() {
                    Name = value.Name,
                    Type = value.Type,
                };
                _dbContext.Tickers.Add(t);
                _dbContext.SaveChanges();
                mvt = new ModelViewTicker() { Name = t.Name, Type = t.Type };

                return Tuple.Create(DbActionResponsesEnum.Ok, (ModelViewTicker?) mvt);
            }
            catch (DbUpdateException)
            {
                return Tuple.Create(DbActionResponsesEnum.ItemAlreadyExists, (ModelViewTicker?) mvt);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine('-'*20);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine('-'*20);
                return Tuple.Create(DbActionResponsesEnum.GenericError, mvt);
            }
        }
    }
}