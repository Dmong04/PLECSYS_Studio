
using PLECSYS_Studio.Wrappers.Products;
using PLECSYS_Studio.Wrappers.SaleOrders;

namespace PLECSYS_Studio.Wrappers.SaleOrderDetails
{
    public class DetailsResponse
    {
        public int Detail_id { get; set; }

        public SaleOrderResponse? Order { get; set; }

        public ProductResponse? Product { get; set; }

        public int Quantity { get; set; }

        public decimal Unit_price { get; set; }

        public decimal Subtotal { get; set; }
    }
}
