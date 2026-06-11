using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.History;

namespace PLECSYS_Studio.Services.History
{
    public interface IInvoiceHistoryService
    {
        Task<APIResponse<List<InvoiceHistoryResponse>>> GetInvoicesHistorybyUseraAndCompanyId(FindHistoryRequest request);

        Task<APIResponse<List<InvoiceHistoryResponse>>> GetPaymentHistory(int invoiceId);

        Task<APIResponse<List<InvoiceHistoryResponse>>> GetClaimHistory(int invoiceId);
    }
}