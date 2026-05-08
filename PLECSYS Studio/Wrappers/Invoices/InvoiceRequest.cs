using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Invoices
{
    public class InvoiceRequest
    {
        public int Invoice_id { get; set; }

        public int Consecutive { get; set; }

        public decimal Total_voucher { get; set; }

        public int User { get; set; }

        public int Sell_company { get; set; }

        public int Charged_company { get; set; }

        public int Currency { get; set; }
    }
}
