using PLECSYS_Studio.Wrappers.Products;

namespace PLECSYS_Studio.Wrappers.SaleOrderDetails
{
    public class DetailsRequest
    {
        public int Order_id { get; set; }

        public int Product_id { get; set; }

        public int Quantity { get; set; }

        public ProductResponse? Product { get; set; }

        public decimal Subtotal => Quantity * Product.Unit_price;

        public decimal Total => Subtotal;
    }
}
