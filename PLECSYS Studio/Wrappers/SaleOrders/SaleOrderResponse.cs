
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.Wrappers.SaleOrders
{
    public class SaleOrderResponse
    {
        public int Order_id { get; set; }

        public UserResponse? Client { get; set; }

        public DateTime? Order_date { get; set; }
    }
}
