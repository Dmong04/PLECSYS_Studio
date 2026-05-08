using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.GPS
{
    public class LocationSeller
    {
        public string Type { get; set; } = "Point";

        public double[] Coordinates { get; set; } = [];
    }
}
