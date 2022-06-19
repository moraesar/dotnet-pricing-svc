using dotnet_pricing_svc.Models;

namespace dotnet_pricing_svc.Data
{
    public enum DbActionResponsesEnum
    {
        Ok,
        GenericError,
        ItemAlreadyExists,
        CannotAcessContext,
    }
}