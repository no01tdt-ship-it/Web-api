using Microsoft.EntityFrameworkCore;
using Web_api.Models;

namespace Web_api.Data
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {
        }
      
        public virtual DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .UseIdentityColumn(seed: 1001, increment: 1)
                      .HasComment("Mã học sinh");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasComment("Họ và tên học sinh");

                entity.Property(e => e.ClassName)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasComment("Tên lớp");

                entity.Property(e => e.Image)
                      .HasMaxLength(250)
                      .HasComment("Ảnh đại diện");

                entity.Property(e => e.Gender)
                      .IsRequired()
                      .HasDefaultValue(true)
                      .HasComment("Giới tính (True: Nam, False: Nữ)");

                entity.Property(e => e.Email)
                      .HasMaxLength(50)
                      .HasComment("Địa chỉ email");

                entity.Property(e => e.BirthDay)
                      .IsRequired()
                      .HasColumnType("date")
                      .HasComment("Ngày sinh");

                entity.Property(e => e.CreatedDate)
                      .HasDefaultValueSql("GETDATE()")
                      .HasColumnType("datetime")
                      .HasComment("Ngày tạo hồ sơ");

                entity.Property(e => e.CitizenId)
                      .IsRequired()
                      .HasMaxLength(12)
                      .HasComment("Số căn cước công dân");

                entity.Property(e => e.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(12)
                      .HasComment("Số điện thoại liên hệ");

                entity.Property(e => e.Province)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasComment("Tỉnh/Thành phố");

                entity.Property(e => e.Ward)
                      .HasMaxLength(50)
                      .HasComment("Phường/Xã");

                entity.Property(e => e.Address)
                      .HasMaxLength(100)
                      .HasComment("Địa chỉ chi tiết");

                entity.Property(e => e.Course)
                      .HasMaxLength(50)
                      .HasComment("Khóa học");

                entity.Property(e => e.IsRetained)
                      .HasDefaultValue(false)
                      .HasComment("Trạng thái lưu ban (True: Lưu ban, False: Không lưu ban)");
            });
        }


    }
}
