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
                .ValueGeneratedOnAdd()
                .HasComment("Mã lớp học");
            entity.
        }
    }
}
