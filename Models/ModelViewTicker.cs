using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{

    public class ModelViewTicker
    {
        public string Name { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
    }
}