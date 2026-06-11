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

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetClaimHistory(int invoiceId)
        {
            try
            {
                var response = await _data.GetClaimHistory(invoiceId);

                if (response.Data is null)
                {
                    return new APIResponse<List<InvoiceHistoryResponse>>
                    {
                        Data = null,
                        Success = false,
                        Message = response.Message
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceHistoryResponse>>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetInvoicesHistorybyUseraAndCompanyId(FindHistoryRequest request)
        {
            try
            {
                var response = await _data.GetInvoiceHistoryByUserAndCompanyId(request);
                if (response.Data is null)
                {
                    return new APIResponse<List<InvoiceHistoryResponse>>()
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
                return new APIResponse<List<InvoiceHistoryResponse>>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetPaymentHistory(int invoiceId)
        {
            try
            {
                var response = await _data.GetPaymentHistory(invoiceId);

                if (response.Data is null)
                {
                    return new APIResponse<List<InvoiceHistoryResponse>>
                    {
                        Data = null,
                        Success = false,
                        Message = response.Message
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceHistoryResponse>>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
