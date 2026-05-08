using PLECSYS_Studio.Data.GPS;
using PLECSYS_Studio.Models.GPS;
using PLECSYS_Studio.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.GPS
{
    public class LocationService(LocationData _data) : ILocationService
    {
        public async Task<APIResponse<SaveLocationResponse>> SaveLocation(SaveLocationRequest request)
        {
            try
            {
                return await _data.SaveLocation(request);
            }
            catch (Exception ex)
            {
                return new APIResponse<SaveLocationResponse>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
