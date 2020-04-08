using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayRelation.Data.Models
{
    public enum PaymentMethod
    {
        Paypal,
        CreditCard,
        Wire
    }

    public class PaymentInfo
    {
        public int Id { get; set; }
        public Registration Registration { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
        public override string ToString()
        {
            return $"{{ Id: {Id}, Primary: {Registration.Id} }}";
        }
    }
}
