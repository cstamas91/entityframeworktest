using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace TwoWayRelation.Data.Models
{
    public enum PaymentMethod
    {
        Paypal,
        CreditCard,
        Wire
    }

    public static class EnumToStringExtensions
    {
        public static string ToFriendlyString(this PaymentMethod paymentMethod) =>
            paymentMethod switch
            {
                PaymentMethod.CreditCard => "hitelkártya",
                PaymentMethod.Paypal => "paypal",
                PaymentMethod.Wire => "utalás",
                _ => string.Empty,
            };
        public static string ToFriendlyString(this Region region) =>
            region switch
            {
                Region.HU => "Magyarország",
                _ => string.Empty,
            };
    }


    public class PaymentInfo : IAuditableEntityNode<Registration>
    {
        public int Id { get; set; }
        public Registration Registration { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            return $"{{ Id: {Id}, Primary: {Registration.Id} }}";
        }

        public void Visit(IAuditableVisitor<Registration> logger, DbContext dbContext, string navigation)
        {
            var entry = dbContext.Entry(this);
            if (entry.State == EntityState.Added)
            {
                logger.LogChange("Fizetési mód", PaymentMethod);
            }
            else if (entry.State == EntityState.Modified)
            {
                AuditHelpers.LogAttributeIfChanged(logger, entry, "Fizetési mód", nameof(PaymentMethod), PaymentMethod);
            }
        }
    }
}
