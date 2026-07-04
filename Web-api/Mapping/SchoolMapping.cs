using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class SchoolMapping : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> entity)
        {
            entity.ToTable("School");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .UseIdentityColumn(seed: 1001, increment: 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã trường học");

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Tên trường học");

            entity.Property(s => s.Code)
                .HasMaxLength(20)
                .HasComment("Mã định danh trường");

            entity.HasIndex(s => s.Code)
                .IsUnique();
            entity.Property(s => s.ProvinceId)
                .HasComment("Mã tỉnh/thành phố");

            entity.Property(s => s.WardId)
                .HasComment("Mã phường/xã");

            entity.Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("datetime")
                .HasComment("Ngày tạo");

            entity.HasOne(s => s.Province)
                .WithMany()
                .HasForeignKey(s => s.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Ward)
                .WithMany()
                .HasForeignKey(s => s.WardId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(s => s.Address)
                .HasMaxLength(250)
                .HasComment("Nhập địa chỉ");

            entity.Property(s => s.Active)
                .HasDefaultValue(true)
                .HasComment("Trạng thái hoạt động (True: Hoạt động, False: Khóa)");

            entity.HasOne(s => s.SchoolLevel)
                .WithMany(s => s.Schools)
                .HasForeignKey(s => s.SchoolLevelId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(s => s.SchoolClasses)
                .WithOne(s => s.School)
                .HasForeignKey(s => s.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
