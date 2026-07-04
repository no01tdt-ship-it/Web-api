using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.ToTable("Student");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn(seed: 1001, increment: 1)
                   .HasComment("Mã học sinh");

            entity.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasComment("Họ và tên học sinh");

            entity.Property(s => s.Image)
                   .HasMaxLength(250)
                   .HasComment("Ảnh đại diện");

            entity.Property(s => s.Gender)
                   .IsRequired(false)
                   .HasColumnType("int")
                   .HasComment("Giới tính (1: Nam, 2: Nữ, 3: Khác, null: Chưa chọn)");

            entity.Property(s => s.Email)
                   .HasMaxLength(50)
                   .HasComment("Địa chỉ email");

            entity.Property(s => s.BirthDay)
                   .IsRequired()
                   .HasColumnType("date")
                   .HasComment("Ngày sinh");

            entity.Property(s => s.CreatedDate)
                   .HasDefaultValueSql("GETDATE()")
                   .HasColumnType("datetime")
                   .HasComment("Ngày tạo hồ sơ");

            entity.Property(s => s.CitizenId)
                   .IsRequired()
                   .HasMaxLength(12)
                   .HasComment("Số căn cước công dân");

            entity.Property(s => s.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(12)
                   .HasComment("Số điện thoại liên hệ");

            entity.Property(s => s.ProvinceId)
                   .HasComment("Mã tỉnh/thành phố");

            entity.Property(s => s.WardId)
                   .HasComment("Mã phường/xã");

            entity.HasOne(s => s.Province)
                   .WithMany()
                   .HasForeignKey(s => s.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Ward)
                   .WithMany()
                   .HasForeignKey(s => s.WardId)
                   .OnDelete(DeleteBehavior.Restrict);

            entity.Property(s => s.Address)
                   .HasMaxLength(100)
                   .HasComment("Địa chỉ chi tiết");

            entity.Property(s => s.Course)
                   .HasMaxLength(50)
                   .HasComment("Khóa học");

            entity.Property(s => s.IsRetained)
                   .HasDefaultValue(false)
                   .HasComment("Trạng thái lưu ban (True: Lưu ban, False: Không lưu ban)");
            entity.Property(s => s.Active)
                   .HasDefaultValue(true)
                   .HasComment("Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)");
        }
    }


}