
namespace PLECSYS_Studio.Wrappers.Products
{
    public class ProductResponse
    {
        public int Product_id { get; set; }

        public string? Product_name { get; set; }

        public string? Product_detail { get; set; }

        public decimal Unit_price { get; set; }

        public string Display_name => $"{Product_name} - ₡{Unit_price:N2}";
    }
}
