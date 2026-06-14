using Web_api.Data;
using Web_api.Interfaces.IRepositories;
using Web_api.Models;

namespace Web_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly ApplicationDb _context;

        public StudentRepository(ApplicationDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
