using dotnet_pricing_svc.Models;

namespace dotnet_pricing_svc.Data
{
    public interface IPriceRepository
    {
        //ModelViewPrice GetByTickerName(string tikerName);
        ICollection<ModelViewPrice> GetAll(string tickerName);
        Tuple<bool, ModelViewPrice?> Save(ModelViewPrice price);
        //ModelViewPrice Delete(ModelViewPrice price);
    }
}