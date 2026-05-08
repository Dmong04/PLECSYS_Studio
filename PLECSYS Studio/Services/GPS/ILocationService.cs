using PLECSYS_Studio.Models.GPS;
using PLECSYS_Studio.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.GPS
{
    public interface ILocationService
    {
        Task<APIResponse<SaveLocationResponse>> SaveLocation(SaveLocationRequest request);
    }
}
