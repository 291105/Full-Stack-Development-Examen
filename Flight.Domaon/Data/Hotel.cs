using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Domain.Data
{
    public class Hotel
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double StarRating { get; set; }
        public string HotelUrl { get; set; } = string.Empty;
    }
}
