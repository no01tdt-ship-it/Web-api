using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class SchoolClassMapping : IEntityTypeConfiguration<SchoolClass>
    {
        public void Configure(EntityTypeBuilder<SchoolClass> entity)
        {
            entity.ToTable("SchoolClass");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .UseIdentityColumn(seed: 1001, increment: 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã lớp học");
            entity.Property(s => s.Name)
                .HasMaxLength(100)
                .HasComment("Tên lớp");
            entity.Property(s => s.Grade)
                .IsRequired(false)
                .HasComment("Khối lớp");
            entity.Property(s => s.Code)
                .HasMaxLength(50)
                .HasComment("Mã định danh lớp");
            entity.HasIndex(s => s.Code)
                .IsUnique();
            entity.Property(s => s.Active)
                .HasDefaultValue(true);

            entity.Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("datetime")
                .HasComment("Ngày tạo");

            entity.HasOne(x => x.School)
               .WithMany(x => x.SchoolClasses)
               .HasForeignKey(x => x.SchoolId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.Students)
                .WithOne(x => x.SchoolClass)
                .HasForeignKey(x => x.SchoolClassId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
