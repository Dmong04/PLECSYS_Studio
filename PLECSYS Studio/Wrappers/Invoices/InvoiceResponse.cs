using PLECSYS_Studio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Invoices
{
    public class InvoiceResponse
    {
        public int Invoice_id { get; set; }

        public int Consecutive { get; set; }

        public decimal Total_voucher { get; set; }

        public User? User { get; set; }

        public Company? Sell_company { get; set; }

        public Company? Charged_company { get; set; }

        public DateTime Invoice_date { get; set; }

        public Currency? Currency { get; set; }
    }
}
