using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{

    public class ModelViewPrice
    {
        public string TickerName { get; set; } = String.Empty;
        public DateTime Date { get; set; }
        public float Price { get; set; }
    }
}