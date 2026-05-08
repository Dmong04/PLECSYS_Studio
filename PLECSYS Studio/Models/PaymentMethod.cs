namespace PLECSYS_PROTOTYPE_MAUI.Models
{
    public class PaymentMethod
    {
        public int payment_method_id { get; set; }
        public int Payment_method_Code { get; set; } //99 code for another
        public string Payment_method_Name { get; set; } = string.Empty;
    }
}