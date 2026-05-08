using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.History;

namespace PLECSYS_Studio.Services.History
{
    public interface IInvoiceHistoryService
    {
        Task<APIResponse<InvoiceHistoryResponse>> GetInvoicesHistorybyId(int historyId);
    }
}