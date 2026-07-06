using Web_api.Helpers;
using Web_api.Interfaces.IRepositories;
using Web_api.Interfaces.IServices;
using Web_api.Models;
using Web_api.ViewModels.AdminMenu;

namespace Web_api.Services
{
    /// <summary>
    /// Triển khai dịch vụ quản lý Menu quản trị (Admin Menu).
    /// </summary>
    public class AdminMenuService : IAdminMenuService
    {
        private readonly IAdminMenuRepository _adminMenuRepository;

        public AdminMenuService(IAdminMenuRepository adminMenuRepository)
        {
            _adminMenuRepository = adminMenuRepository;
        }

        /// <summary>
        /// Tạo mới một Menu quản trị.
        /// </summary>
        public async Task<bool> CreateMenuAsync(AddAdminMenuViewModel viewModel)
        {
            // Sinh mã menuCode tự động từ MenuName
            var menuCode = SlugHelper.GenerateCode(viewModel.MenuName);

            // 1. Kiểm tra mã menu đã tồn tại chưa
            if (await _adminMenuRepository.IsExistMenuCodeAsync(menuCode))
            {
                return false;
            }

            // 2. Kiểm tra tên menu đã tồn tại chưa
            if (await _adminMenuRepository.IsExistMenuNameAsync(viewModel.MenuName))
            {
                return false;
            }

            // 3. Nếu có Menu cha, kiểm tra Menu cha có tồn tại không
            if (viewModel.ParentId.HasValue)
            {
                var parent = await _adminMenuRepository.GetAsync(viewModel.ParentId.Value);
                if (parent == null)
                {
                    return false;
                }
            }

            // 4. Ánh xạ ViewModel sang Model
            var menu = new AdminMenu
            {
                ParentId = viewModel.ParentId,
                MenuCode = menuCode,
                MenuName = viewModel.MenuName.Trim(),
                Url = viewModel.Url?.Trim(),
                Icon = viewModel.Icon?.Trim(),
                SortOrder = viewModel.SortOrder,
                Active = viewModel.Active,
                IsSystem = viewModel.IsSystem,
                Description = viewModel.Description?.Trim()
            };

            // 5. Thêm mới và lưu thay đổi
            await _adminMenuRepository.InsertAsync(menu);
            return await _adminMenuRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy danh sách tất cả các Menu quản trị.
        /// </summary>
        public async Task<List<ListAdminMenuViewModel>> GetAllAsync()
        {
            var (data, _, _) = await _adminMenuRepository.GetAllAsync();

            return data.Select(m => new ListAdminMenuViewModel
            {
                Id = m.Id,
                ParentId = m.ParentId,
                ParentName = m.Parent?.MenuName,
                MenuCode = m.MenuCode,
                MenuName = m.MenuName,
                Url = m.Url,
                Icon = m.Icon,
                SortOrder = m.SortOrder,
                Active = m.Active,
                IsSystem = m.IsSystem,
                Description = m.Description,
                CreatedDate = m.CreatedDate,
                UpdatedDate = m.UpdatedDate,
                ChildrenCount = m.Children?.Count ?? 0
            }).ToList();
        }

        /// <summary>
        /// Lấy chi tiết một Menu quản trị.
        /// </summary>
        public async Task<DetailAdminMenuViewModel?> GetByIdAsync(long id)
        {
            var menu = await _adminMenuRepository.GetAsync(id);
            if (menu == null)
            {
                return null;
            }

            return new DetailAdminMenuViewModel
            {
                Id = menu.Id,
                ParentId = menu.ParentId,
                ParentName = menu.Parent?.MenuName,
                MenuCode = menu.MenuCode,
                MenuName = menu.MenuName,
                Url = menu.Url,
                Icon = menu.Icon,
                SortOrder = menu.SortOrder,
                Active = menu.Active,
                IsSystem = menu.IsSystem,
                Description = menu.Description,
                CreatedDate = menu.CreatedDate,
                UpdatedDate = menu.UpdatedDate,
                Children = menu.Children?
                    .Where(c => c.Active)
                    .Select(c => new ListAdminMenuViewModel
                    {
                        Id = c.Id,
                        ParentId = c.ParentId,
                        ParentName = menu.MenuName,
                        MenuCode = c.MenuCode,
                        MenuName = c.MenuName,
                        Url = c.Url,
                        Icon = c.Icon,
                        SortOrder = c.SortOrder,
                        Active = c.Active,
                        IsSystem = c.IsSystem,
                        Description = c.Description,
                        CreatedDate = c.CreatedDate,
                        UpdatedDate = c.UpdatedDate
                    }).OrderBy(c => c.SortOrder).ToList() ?? new List<ListAdminMenuViewModel>()
            };
        }

        /// <summary>
        /// Cập nhật thông tin một Menu quản trị.
        /// </summary>
        public async Task<bool> UpdateAsync(UpdateAdminMenuViewModel model)
        {
            var menu = await _adminMenuRepository.GetAsync(model.Id);
            if (menu == null)
            {
                return false;
            }

            // Tránh gán menu cha là chính nó
            if (model.ParentId.HasValue && model.ParentId.Value == model.Id)
            {
                return false;
            }

            // Sinh mã menuCode tự động từ MenuName
            var menuCode = SlugHelper.GenerateCode(model.MenuName);

            // Kiểm tra trùng lặp MenuCode
            if (await _adminMenuRepository.IsExistMenuCodeAsync(menuCode, model.Id))
            {
                return false;
            }

            // Kiểm tra trùng lặp MenuName
            if (await _adminMenuRepository.IsExistMenuNameAsync(model.MenuName, model.Id))
            {
                return false;
            }

            // Nếu thay đổi menu cha, kiểm tra xem menu cha có tồn tại không
            if (model.ParentId.HasValue)
            {
                var parent = await _adminMenuRepository.GetAsync(model.ParentId.Value);
                if (parent == null)
                {
                    return false;
                }
            }

            // Cập nhật thông tin thực thể
            menu.ParentId = model.ParentId;
            menu.MenuCode = menuCode;
            menu.MenuName = model.MenuName.Trim();
            menu.Url = model.Url?.Trim();
            menu.Icon = model.Icon?.Trim();
            menu.SortOrder = model.SortOrder;
            menu.Active = model.Active;
            menu.IsSystem = model.IsSystem;
            menu.Description = model.Description?.Trim();

            await _adminMenuRepository.UpdateAsync(menu);
            return await _adminMenuRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Xóa mềm một Menu quản trị.
        /// </summary>
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _adminMenuRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// Khôi phục một Menu quản trị đã bị xóa mềm trước đó.
        /// </summary>
        public async Task<bool> RestoreAsync(long id)
        {
            return await _adminMenuRepository.RestoreAsync(id);
        }

        /// <summary>
        /// Xóa vĩnh viễn (xóa cứng) một Menu quản trị.
        /// </summary>
        public async Task<bool> HardDeleteAsync(long id)
        {
            return await _adminMenuRepository.HardDeleteAsync(id);
        }

        /// <summary>
        /// Lấy danh sách các Menu cha.
        /// </summary>
        public async Task<List<AdminMenu>> GetParentMenusAsync()
        {
            return await _adminMenuRepository.GetParentMenusAsync();
        }

        /// <summary>
        /// Kiểm tra trùng lặp MenuCode.
        /// </summary>
        public async Task<bool> IsExistMenuCodeAsync(string menuCode, long id = 0)
        {
            return await _adminMenuRepository.IsExistMenuCodeAsync(menuCode, id);
        }

        /// <summary>
        /// Kiểm tra trùng lặp MenuName.
        /// </summary>
        public async Task<bool> IsExistMenuNameAsync(string menuName, long id = 0)
        {
            return await _adminMenuRepository.IsExistMenuNameAsync(menuName, id);
        }
    }
}
