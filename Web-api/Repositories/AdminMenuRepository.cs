using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web_api.Data;
using Web_api.Interfaces.IRepositories;
using Web_api.Models;

namespace Web_api.Repositories
{
    public class AdminMenuRepository : IAdminMenuRepository

    {
        private readonly ApplicationDb _db;

        public AdminMenuRepository(ApplicationDb db)
        {
            _db = db;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách bản ghi menu quản trị kèm theo thông tin tổng số lượng.
        /// </summary>
        /// <returns>Tuple chứa danh sách AdminMenu, tổng số bản ghi và tổng số bản ghi sau lọc.</returns>
        public async Task<(List<AdminMenu> data , int total, int totalFiltered)> GetAllAsync()
        {
            var query = _db.AdminMenus
                .Where(x => x.Active)
                .AsNoTracking()
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.CreatedDate);

            var total = await query.CountAsync();

            var data = await query.ToListAsync();

            var totalFiltered = total;

            return (data, total, totalFiltered);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một menu quản trị dựa trên ID (khóa chính).
        /// Có bao gồm thông tin Menu cha (Parent) và các Menu con (Children).
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của menu.</param>
        /// <returns>Thực thể AdminMenu tương ứng hoặc null nếu không tìm thấy.</returns>
        public async Task<AdminMenu?> GetAsync(long id)
        {
            return await _db.AdminMenus
                .Include(m => m.Parent)
                .Include(m => m.Children)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// Lấy đối tượng DatabaseFacade để quản lý trực tiếp các tác vụ cấp thấp của Database (Transactions, SQL raw,...).
        /// </summary>
        /// <returns>Đối tượng DatabaseFacade liên kết với DbContext hiện tại.</returns>
        public DatabaseFacade GetDatabase()
        {
            return _db.Database;
        }

        /// <summary>
        /// Xóa cứng (Hard Delete) - Xóa vĩnh viễn menu quản trị khỏi cơ sở dữ liệu vật lý.
        /// </summary>
        /// <param name="id">Mã định danh của menu cần xóa vĩnh viễn.</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False.</returns>
        public async Task<bool> HardDeleteAsync(long id)
        {
            var menu = await _db.AdminMenus.FindAsync(id);

            if (menu == null)
            {
                return false;
            }

            if (menu.IsSystem)
            {
                return false;
            }

            var hasChildren = await _db.AdminMenus.AnyAsync(x => x.ParentId == id);

            if (hasChildren)
            {
                return false;
            }

            _db.AdminMenus.Remove(menu);

            return await SaveChangesAsync();
        }

        /// <summary>
        /// Thêm mới một menu quản trị vào DbContext.
        /// </summary>
        /// <param name="obj">Thực thể menu cần thêm mới.</param>
        public async Task InsertAsync(AdminMenu obj)
        {
            obj.CreatedDate = DateTime.Now;
            obj.Active = true;
            await _db.AdminMenus.AddAsync(obj);
        }

        /// <summary>
        /// Thêm mới hàng loạt menu quản trị vào DbContext.
        /// </summary>
        /// <param name="objs">Danh sách thực thể menu cần thêm mới.</param>
        public async Task InsertManyAsync(IEnumerable<AdminMenu> objs)
        {
            foreach (var obj in objs)
            {
                obj.CreatedDate = DateTime.Now;
            }
            await _db.AdminMenus.AddRangeAsync(objs);
        }

        /// <summary>
        /// Khôi phục một menu quản trị đã bị xóa mềm trước đó bằng cách đặt Active = true.
        /// </summary>
        /// <param name="id">Mã định danh của menu cần khôi phục.</param>
        /// <returns>True nếu khôi phục thành công, ngược lại trả về False.</returns>
        public async Task<bool> RestoreAsync(long id)
        {
            var menu = await _db.AdminMenus.FindAsync(id);
            if (menu == null)
            {
                return false;
            }

            menu.Active = true;
            menu.UpdatedDate = DateTime.Now;
            _db.AdminMenus.Update(menu);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Xác nhận và lưu mọi thay đổi trong DbContext xuống cơ sở dữ liệu SQL Server.
        /// </summary>
        /// <returns>True nếu lưu thành công ít nhất một bản ghi, ngược lại trả về False.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa mềm một menu quản trị bằng cách đặt thuộc tính Active = false.
        /// </summary>
        /// <param name="id">Mã định danh của menu cần xóa mềm.</param>
        /// <returns>True nếu xóa mềm thành công, ngược lại trả về False.</returns>
        public async Task<bool> SoftDeleteAsync(long id)
        {
            var menu = await _db.AdminMenus.FindAsync(id);

            if (menu == null)
            {
                return false;
            }

            if (menu.IsSystem)
            {
                return false;
            }

            menu.Active = false;
            menu.UpdatedDate = DateTime.Now;

            _db.AdminMenus.Update(menu);

            return await SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật thông tin một menu quản trị.
        /// </summary>
        /// <param name="obj">Thực thể menu cần cập nhật.</param>
        public Task UpdateAsync(AdminMenu obj)
        {
            obj.UpdatedDate = DateTime.Now;
            _db.AdminMenus.Update(obj);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Cập nhật hàng loạt menu quản trị.
        /// </summary>
        /// <param name="objs">Danh sách thực thể menu cần cập nhật.</param>
        public Task UpdateManyAsync(IEnumerable<AdminMenu> objs)
        {
            foreach (var obj in objs)
            {
                obj.UpdatedDate = DateTime.Now;
            }
            _db.AdminMenus.UpdateRange(objs);
            return Task.CompletedTask;
        }
        public async Task<bool> IsExistMenuCodeAsync(string menuCode, long id = 0)
        {
            return await _db.AdminMenus
                .AnyAsync(x => x.MenuCode == menuCode && x.Id != id);
        }

        public async Task<bool> IsExistMenuNameAsync(string menuName, long id = 0)
        {
            return await _db.AdminMenus
                .AnyAsync(x => x.MenuName == menuName && x.Id != id);
        }

        public async Task<List<AdminMenu>> GetParentMenusAsync()
        {
            return await _db.AdminMenus
                .Where(x => x.ParentId == null && x.Active)
                .AsNoTracking()
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
        }
    }
}
