using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class WardMapping : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> entity)
        {
            entity.ToTable("Ward");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .UseIdentityColumn(seed: 1001, increment: 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã phường/xã");

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Tên phường/xã");

            entity.Property(s => s.Code)
                .HasMaxLength(50)
                .HasComment("Mã định danh phường/xã");

            entity.HasIndex(s => s.Code)
                .IsUnique();

            entity.Property(s => s.ProvinceId)
                .IsRequired()
                .HasComment("Mã tỉnh/thành phố trực thuộc");

            entity.Property(s => s.Active)
                .HasDefaultValue(true)
                .HasComment("Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)");

            entity.HasOne(x => x.Province)
                .WithMany(x => x.Wards)
                .HasForeignKey(x => x.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
