using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class SchoolLevelMapping : IEntityTypeConfiguration<SchoolLevel>
    {
        public void Configure(EntityTypeBuilder<SchoolLevel> entity)
        {
            entity.ToTable("SchoolLevels");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Mã trường học");
            entity.Property(s => s.Name)
                .HasMaxLength(100)
                .HasComment("Tên Cấp");
            entity.Property(s => s.Code)
                .HasMaxLength(50)
                .HasComment("Mã định danh cấp");
            entity.Property(s => s.Active)
                .HasDefaultValue(true)
                .HasComment("Trạng thái hoạt động (True: Hoạt động, False: Khóa)");
            entity.HasMany(s => s.Schools)
                .WithOne(s => s.SchoolLevel)
                .HasForeignKey(s => s.SchoolLevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
