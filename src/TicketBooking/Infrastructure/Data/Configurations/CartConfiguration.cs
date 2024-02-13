using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Core.CartAggregate;

namespace TicketBooking.Infrastructure.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(cart => cart.Id);

            builder.Property(cart => cart.CreatedBy);
            builder.Property(cart => cart.LastModifiedBy);
            builder.Property(cart => cart.Created);
            builder.Property(cart => cart.LastModified);
        }
    }
}
