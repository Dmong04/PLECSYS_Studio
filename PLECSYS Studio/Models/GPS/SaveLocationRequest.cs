using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Models.GPS
{
    public class SaveLocationRequest
    {
        public string? SellerId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
