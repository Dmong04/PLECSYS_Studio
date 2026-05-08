using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.GPS
{
    public class CoordinateRequest
    {
        public required string Seller_id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public required string Start_location_name { get; set; }

        public required LocationSeller Start_location { get; set; }

        public required string End_location_name { get; set; }

        public required LocationSeller End_location { get; set; }
    }
}
