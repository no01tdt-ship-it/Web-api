using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class ProvinceMapping : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> entity)
        {
            entity.ToTable("Province");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .UseIdentityColumn(seed: 1001, increment: 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã tỉnh/thành phố");

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Tên tỉnh/thành phố");

            entity.Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Mã định danh tỉnh/thành phố");

            entity.HasIndex(s => s.Code)
                .IsUnique();

            entity.Property(s => s.Active)
                .HasDefaultValue(true)
                .HasComment("Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)");
        }
    }
}
