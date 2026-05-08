using PLECSYS_Studio.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Invoices;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Data.Invoices
{
    public class InvoiceData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<InvoiceResponse>>> LoadInvoices()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Invoice>>>("invoice/all");

            return new APIResponse<List<InvoiceResponse>>()
            {
                Data = MapInvoices(response?.Data),
                Success = response?.Success ?? false,
                Message = response?.Message ?? "Error al cargar facturas"
            };
        }

        public async Task<bool> RegenerateInvoicePdfAsync(int consecutive, CancellationToken ct = default)
        {
            var url = $"invoices/{consecutive}/regenerate"; //Homologar con la ruta correcta
            var response = await _http.PostAsync(url, null, ct);
            return response.IsSuccessStatusCode;

        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByClient(string email)
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Invoice>>>($"invoice/client/{email}");

            return new APIResponse<List<InvoiceResponse>>()
            {
                Data = MapInvoices(response?.Data),
                Success = response?.Success ?? false,
                Message = response?.Message ?? "error al cargar facturas"
            };
        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByCurrency(int currency)
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Invoice>>>($"invoice/currency/{currency}");

            return new APIResponse<List<InvoiceResponse>>()
            {
                Data = MapInvoices(response?.Data),
                Success = response?.Success ?? false,
                Message = response?.Message ?? "error al cargar facturas"
            };
        }

        public async Task<APIResponse<List<InvoiceResponse>>> GetInvoicesByDate(DateTime date)
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Invoice>>>($"invoice/date/{date}");

            return new APIResponse<List<InvoiceResponse>>()
            {
                Data = MapInvoices(response?.Data),
                Success = response?.Success ?? false,
                Message = response?.Message ?? "error al cargar facturas"
            };
        }

        public async Task<APIResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest request)
        {
            try
            {
                var new_invoice = await _http.PostAsJsonAsync($"invoice/create", request);
                new_invoice.EnsureSuccessStatusCode();

                var response = await new_invoice.Content.ReadFromJsonAsync<APIResponse<Invoice>>();

                var success = new InvoiceResponse()
                {
                    Invoice_id = response.Data.Invoice_id,
                    Consecutive = response.Data.Consecutive,
                    Total_voucher = response.Data.Total_voucher,
                    User = response.Data.User,
                    Sell_company = response.Data.Sell_company,
                    Charged_company = response.Data.Charged_company,
                    Invoice_date = response.Data.Invoice_date,
                    Currency = response.Data.Currency
                };

                return new APIResponse<InvoiceResponse>()
                {
                    Data = success,
                    Success = response.Success,
                    Message = response.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<InvoiceResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<HttpResponseMessage> GetInvoicePdfAsync(int consecutive, CancellationToken ct = default)
        {
            var url = $"invoices/{consecutive}/pdf"; //Homologar con la ruta correcta
            var response = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);
            return response;
        }

        private static List<InvoiceResponse>? MapInvoices(List<Invoice>? invoices)
        {
            return invoices?.Select(i => new InvoiceResponse
            {
                Invoice_id = i.Invoice_id,
                Consecutive = i.Consecutive,
                Total_voucher = i.Total_voucher,
                User = i.User,
                Sell_company = i.Sell_company,
                Charged_company = i.Charged_company,
                Invoice_date = i.Invoice_date,
                Currency = i.Currency
            }).ToList();
        }
    }
}
