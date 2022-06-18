using dotnet_pricing_svc.Models;

namespace dotnet_pricing_svc.Data
{
    public class PriceRepository : IPriceRepository
    {
        private readonly PrincingContext _dbContext;

        public PriceRepository(PrincingContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        //public ModelViewPrice GetByTickerName(string tikerName);
        public ICollection<ModelViewPrice> GetAll(string tickerName)
        {
            if (_dbContext.Prices == null)
                return new List<ModelViewPrice>();
            
            return _dbContext.Prices
                .Where(p => p.Ticker.Name == tickerName)
                .Select(p => new ModelViewPrice() { 
                    TickerName = p.Ticker.Name,
                    Date = p.Data,
                    Price = p.Valor
                }).ToList();
        }

        public Tuple<bool, ModelViewPrice?> Save(ModelViewPrice price)
        {
            ModelViewPrice? mvp = null;

            if (_dbContext.Prices == null)
                return Tuple.Create(false, mvp);
            
            try
            {
                Ticker t = _dbContext.Tickers.FirstOrDefault(t => t.Name == price.TickerName);
                Price p = new Price() {
                    Data = price.Date,
                    Valor = price.Price,
                    TickerId = t.TickerId,
                };
                _dbContext.Prices.Add(p);
                _dbContext.SaveChanges();
                mvp = new ModelViewPrice() { Date = p.Data, Price = p.Valor, TickerName = p.Ticker.Name };

                return Tuple.Create(true, mvp);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Tuple.Create(false, mvp);
            }
        }
        //public ModelViewPrice Delete(ModelViewPrice price);
        //TODO: ajustar DB para termos Ticker/Data únicos
        //TODO: criar um TickerRepo
        //TODO: retonar erro 400 quando o ticker não exitir
    }
}