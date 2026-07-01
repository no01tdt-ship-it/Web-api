using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_api.Models;

namespace Web_api.Mapping
{
    public class SchoolClassMapping : IEntityTypeConfiguration<SchoolClass>
    {
        public void Configure(EntityTypeBuilder<SchoolClass> entity)
        {
            entity.ToTable("SchoolClasses");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Mã lớp học");
            entity.Property(s => s.Name)
                .HasMaxLength(100)
                .HasComment("Tên lớp");
            entity.Property(s => s.Grade)
                .IsRequired(false)
                .HasComment("Tên cấp");
            entity.Property(s => s.Code)
                .HasMaxLength(50)
                .HasComment("Mã lớp");
            entity.Property(s => s.Active)
                .HasDefaultValue(true);

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
