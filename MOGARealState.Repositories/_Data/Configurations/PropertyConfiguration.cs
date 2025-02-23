using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MOGARealState.Core.Entities;
using MOGARealState.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Property = MOGARealState.Core.Entities.Property;


namespace MOGARealState.Repositories._Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(5000);

            builder.Property(p => p.Location)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Size)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Purpose)
                     .HasConversion(
                     (type) => type.ToString(),
                     (gen) => (Purpose)Enum.Parse(typeof(Purpose), gen, true));

            builder.Property(c => c.Type)
                    .HasConversion(
                    (type) => type.ToString(),
                    (gen) => (PropertyType)Enum.Parse(typeof(PropertyType), gen, true));

            builder.Property(c => c.Status)
                    .HasConversion(
                    (type) => type.ToString(),
                    (gen) => (PropertyStatus)Enum.Parse(typeof(PropertyStatus), gen, true));

            builder.Property(p => p.HasParking)
                .IsRequired();

            builder.Property(p => p.HasWifi)
                .IsRequired();

            builder.Property(p => p.HasElevator)
                .IsRequired();

            builder.HasOne(p => p.Agent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
