using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public int TickerId { get; set; }
        public Ticker Ticker { get; set; } = null!;
    }
}