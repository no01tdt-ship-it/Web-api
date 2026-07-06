using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web_api.Data;
using Web_api.Interfaces.IRepositories;
using Web_api.Models;
using Web_api.ViewModels.Common;

namespace Web_api.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDb _db;

        public ProvinceRepository(ApplicationDb db)
        {
            _db = db;
        }

        public async Task<(List<Province> data, int total, int totalFiltered)> GetAllAsync()
        {
            var query = _db.Provinces
                .Where(x => x.Active)
                .AsNoTracking()
                .OrderBy(x => x.Name);
            var total = await query.CountAsync();
            var data = await query.ToListAsync();
            var totalFiltered = total;
            return (data, total, totalFiltered);
        }

        public async Task<Province?> GetSync(long id)
        {
            return await _db.Provinces.FindAsync(id);
        }

        public async Task InsertAsync(Province obj)
        {
            obj.CreatedDate = DateTime.Now;
            obj.Active = true;
            await _db.Provinces.AddAsync(obj);
        }

        public async Task InsertManyAsync(IEnumerable<Province> obj)
        {
            foreach (var objs in obj)
            {
                objs.CreatedDate = DateTime.Now;
                objs.Active = true;
            }
            await _db.Provinces.AddRangeAsync(obj);
        }

        public Task UpdateAsync(Province obj)
        {
            _db.Provinces.Update(obj);
            return Task.CompletedTask;
        }
        public Task UpdateManyAsync(IEnumerable<Province> obj)
        {
            _db.Provinces.UpdateRange(obj);
            return Task.CompletedTask;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var province = await _db.Provinces.FindAsync(id);
            if (province == null)
            {
                return false;
            }
            province.Active = false;
            _db.Provinces.Update(province);
            return await SaveChangesAsync();
        }

        public async Task<bool> RestoreAsync(long id)
        {
            var province = await _db.Provinces.FindAsync(id);
            if (province == null)
            {
                return false;
            }
            province.Active = true;
            _db.Provinces.Update(province);
            return await SaveChangesAsync();
        }

        public async Task<bool> HardDeleteAsync(long id)
        {
            var province = await _db.Provinces.FindAsync(id);
            if (province == null)
            {
                return false;
            }
            _db.Provinces.Remove(province);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public DatabaseFacade GetDatabase()
        {
            return _db.Database;
        }

        public async Task<DTResult<ListProvinceViewModel>> ListServerSide

    }
}
