using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOGARealState.Core.Entities;
using MOGARealState.Core.Enums;
using MOGARealState.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Repositories._Data.Configurations
{
    public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrders>
    {
        public void Configure(EntityTypeBuilder<UserOrders> builder)
        {
            builder.Property(c => c.Status)
                    .HasConversion(
                    (type) => type.ToString(),
                    (gen) => (OrderStatus)Enum.Parse(typeof(OrderStatus), gen, true));
        }
    }
}
