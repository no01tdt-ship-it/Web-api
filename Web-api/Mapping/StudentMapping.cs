using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
           builder.ToTable("Students");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn(seed:1001, increment:1);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.ClassName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Image).HasMaxLength(100);
            builder.Property(s => s.Gender).IsRequired().HasDefaultValue(true);
            builder.Property(s => s.Email).HasMaxLength(50);
            builder.Property(s => s.BirthDay).IsRequired().HasColumnType("date");
            builder.Property(s => s.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.CitizenId).IsRequired().HasMaxLength(12);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(12);
            builder.Property(s => s.Province).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Ward).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Address).HasMaxLength(100);
            builder.Property(s => s.Course).HasMaxLength(50);
            builder.Property(s => s.IsRetained).HasDefaultValue(false);
        }
    }
}
