using dotnet_pricing_svc.Models;

namespace dotnet_pricing_svc.Data
{
    public interface IRepository<T>
    {
        Tuple<DbActionResponsesEnum, T?> Save(T value);
    }
}