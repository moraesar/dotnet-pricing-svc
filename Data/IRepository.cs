using dotnet_pricing_svc.Models;

namespace dotnet_pricing_svc.Data
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll(string tickerName);
        Tuple<DbActionResponsesEnum, T?> Save(T price);
    }
}