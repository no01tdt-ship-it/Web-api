using Microsoft.EntityFrameworkCore;
using Web_api.Mapping;
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
            modelBuilder.ApplyConfiguration(new StudentMapping());
            base.OnModelCreating(modelBuilder);
        }


    }
}
