using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.GPS
{
    public class CoordinateResponse
    {
        public ObjectId? Id { get; set; }

        public string? Seller_id { get; set; }

        public DateTime? Timestamp { get; set; }

        public string? Start_location_name { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates>? Start_location { get; set; }

        public string? End_location_name { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates>? End_location { get; set; }
    }
}
