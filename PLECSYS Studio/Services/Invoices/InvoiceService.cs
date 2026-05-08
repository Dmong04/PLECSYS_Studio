using PLECSYS_Studio.Data.Invoices;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.Invoices
{
    public class InvoiceService(InvoiceData _data) : IInvoiceService
    {
        public Task<APIResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByClient(string email)
        {
            try
            {
                var invoices = await _data.GetInvoicesByClient(email);
                if (invoices is null)
                {
                    return new APIResponse<List<InvoiceResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = invoices?.Message
                    };
                }

                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = invoices.Data,
                    Success = true,
                    Message = invoices?.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = new List<InvoiceResponse>(),
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByCurrency(int currency)
        {
            try
            {
                var invoices = await _data.GetInvoicesByCurrency(currency);
                if (invoices is null)
                {
                    return new APIResponse<List<InvoiceResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = invoices?.Message
                    };
                }

                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = invoices.Data,
                    Success = true,
                    Message = invoices?.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = [],
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByDate(DateTime date)
        {
            try
            {
                var invoices = await _data.GetInvoicesByDate(date);
                if (invoices is null)
                {
                    return new APIResponse<List<InvoiceResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = invoices?.Message
                    };
                }

                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = invoices.Data,
                    Success = true,
                    Message = invoices?.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = [],
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<List<InvoiceResponse>>> LoadInvoices()
        {
            try
            {
                var invoices = await _data.LoadInvoices();
                if (invoices is null)
                {
                    return new APIResponse<List<InvoiceResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = invoices?.Message
                    };
                }

                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = invoices.Data,
                    Success = true,
                    Message = invoices?.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<InvoiceResponse>>()
                {
                    Data = [],
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
