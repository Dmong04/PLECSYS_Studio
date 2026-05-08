using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Models
{
    public class Invoice
    {
        public int Invoice_id { get; set; }

        public int Consecutive { get; set; }

        public decimal Total_voucher { get; set; }

        public User User { get; set; } = new User();

        public Company Sell_company { get; set; } = new Company();

        public Company Charged_company { get; set; } = new Company();

        public DateTime Invoice_date { get; set; }

        public Currency Currency { get; set; } = new Currency();
    }
}
