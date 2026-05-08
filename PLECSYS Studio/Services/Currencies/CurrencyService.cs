using PLECSYS_Studio.Data.Currencies;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.Currencies
{
    public class CurrencyService(CurrencyData _data) : ICurrencyService
    {
        public async Task<APIResponse<List<CurrencyResponse>>> GetCurrencies()
        {
            try
            {
                var response = await _data.GetAllCurrencies();
                if (response?.Data?.Count is 0)
                {
                    return new APIResponse<List<CurrencyResponse>>()
                    {
                        Data = null,
                        Success = false,
                        Message = response?.Message
                    };
                }

                return new APIResponse<List<CurrencyResponse>>()
                {
                    Data = response?.Data,
                    Success = true,
                    Message = response?.Message
                };
            } catch (Exception ex)
            {
                return new APIResponse<List<CurrencyResponse>>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
