using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Companies
{
    public class CompanyResponse
    {
        public int Company_id { get; set; }

        public string? Company_name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}
