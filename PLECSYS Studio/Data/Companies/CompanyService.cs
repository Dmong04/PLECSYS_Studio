using PLECSYS_Studio.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.Companies
{
    public class CompanyService(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<CompanyResponse>>> LoadCompanies()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Company>>>("company/all");

            var companies = response?.Data?.Select(c => new CompanyResponse()
            {
                Company_id = c.Company_id,
                Company_name = c.Company_name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email
            }).ToList();

            return new APIResponse<List<CompanyResponse>>()
            {
                Data = companies,
                Success = response.Success,
                Message = response.Message
            };
        }

        public async Task<APIResponse<CompanyResponse>> CreateCompany(CompanyRequest request)
        {
            var new_company = await _http.PostAsJsonAsync($"company/create", request);

            var response = await new_company.Content.ReadFromJsonAsync<APIResponse<Company>>();

            var success = new CompanyResponse()
            {
                Company_id = response.Data.Company_id,
                Company_name = response.Data.Company_name,
                Address = response.Data.Address,
                Phone = response.Data.Phone,
                Email = response.Data.Email
            };

            return new APIResponse<CompanyResponse>()
            {
                Data = success,
                Success = response.Success,
                Message = response.Message
            };
        }
    }
}
