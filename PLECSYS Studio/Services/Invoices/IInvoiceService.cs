using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.Invoices
{
    public interface IInvoiceService
    {
        Task<APIResponse<List<InvoiceResponse>>> LoadInvoices();

        Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByClient(string email);

        Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByCurrency(int currency);

        Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByDate(DateTime date);

        Task<APIResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest request);

        Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByExpiryDate(DateTime expiryDate); // 👈
    }
}
