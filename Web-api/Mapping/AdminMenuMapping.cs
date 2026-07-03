using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class AdminMenuMapping : IEntityTypeConfiguration<AdminMenu>
    {
        public void Configure(EntityTypeBuilder<AdminMenu> entity)
        {
            entity.ToTable("AdminMenu");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .UseIdentityColumn(seed: 1001, increment: 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã menu");

            entity.Property(s => s.ParentId)
                .HasComment("Mã menu cha");

            entity.Property(s => s.MenuCode)
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("Mã chức năng menu");

            entity.Property(s => s.MenuName)
                .HasMaxLength(250)
                .IsRequired()
                .HasComment("Tên menu");

            entity.Property(s => s.Url)
                .HasMaxLength(250)
                .HasComment("Đường dẫn menu");

            entity.Property(s => s.Icon)
                .HasMaxLength(250)
                .HasComment("Icon menu");

            entity.Property(s => s.SortOrder)
                .HasDefaultValue(0)
                .HasComment("Thứ tự hiển thị");

            entity.Property(s => s.Active)
                .HasDefaultValue(true)
                .HasComment("Trạng thái hoạt động");

            entity.Property(s => s.IsSystem)
                .HasDefaultValue(false)
                .HasComment("Menu hệ thống");

            entity.Property(s => s.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");

            entity.Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("datetime")
                .HasComment("Ngày tạo");

            entity.Property(s => s.UpdatedDate)
                .HasColumnType("datetime")
                .HasComment("Ngày cập nhật");

            entity.HasIndex(s => s.MenuCode)
                .IsUnique();

            entity.HasIndex(s => s.ParentId);

            entity.HasOne(s => s.Parent)
                .WithMany(s => s.Children)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}