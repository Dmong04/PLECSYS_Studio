using PLECSYS_Studio.Wrappers.History;
using PLECSYS_Studio.Data.History;
using PLECSYS_Studio.Wrappers;

namespace PLECSYS_Studio.Services.History
{
    public class InvoiceHistoryService : IInvoiceHistoryService
    {
        private readonly InvoiceHistoryData _data;

        public InvoiceHistoryService(InvoiceHistoryData data)
        {
            _data = data;
        }

        public async Task<APIResponse<InvoiceHistoryResponse>> GetInvoicesHistorybyId(int historyId)
        {
            try
            {
                var response = await _data.GetInvoicesHistorybyId(historyId);
                if (response.Data is null)
                {
                    return new APIResponse<InvoiceHistoryResponse>()
                    {
                        Data = null,
                        Success = false,
                        Message = response.Message,
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new APIResponse<InvoiceHistoryResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
