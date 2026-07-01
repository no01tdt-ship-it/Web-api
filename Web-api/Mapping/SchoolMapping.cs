using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class SchoolMapping : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> entity)
        {
            entity.ToTable("Schools");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
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
            entity.Property(s => s.Province)
                .HasMaxLength(50)
                .HasComment("Nhập tỉnh");
            entity.Property(s => s.Ward)
                .HasMaxLength(50)
                .HasComment("Nhập Xã");
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
