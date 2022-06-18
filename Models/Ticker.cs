using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{
    public class Ticker
    {
        public int TickerId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public ICollection<Price>? Prices { get; set; }
    }
}