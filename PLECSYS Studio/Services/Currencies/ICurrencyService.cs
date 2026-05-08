using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.Currencies
{
    public interface ICurrencyService
    {
        Task<APIResponse<List<CurrencyResponse>>> GetCurrencies();
    }
}
