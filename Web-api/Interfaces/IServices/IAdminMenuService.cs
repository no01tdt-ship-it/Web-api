using Web_api.Models;
using Web_api.ViewModels.AdminMenu;

namespace Web_api.Interfaces.IServices
{
    /// <summary>
    /// Giao diện Service quản lý Menu quản trị (Admin Menu).
    /// Chứa các phương thức xử lý nghiệp vụ liên quan đến AdminMenu.
    /// </summary>
    public interface IAdminMenuService
    {
        /// <summary>
        /// Tạo mới một Menu quản trị.
        /// </summary>
        /// <param name="viewModel">Dữ liệu Menu cần tạo mới.</param>
        /// <returns>True nếu tạo thành công, ngược lại trả về False.</returns>
        Task<bool> CreateMenuAsync(AddAdminMenuViewModel viewModel);

        /// <summary>
        /// Lấy danh sách tất cả các Menu dưới dạng ViewModel.
        /// </summary>
        /// <returns>Danh sách các ListAdminMenuViewModel.</returns>
        Task<List<ListAdminMenuViewModel>> GetAllAsync();

        /// <summary>
        /// Lấy chi tiết Menu quản trị theo mã ID.
        /// </summary>
        /// <param name="id">Mã ID của Menu.</param>
        /// <returns>DetailAdminMenuViewModel hoặc null nếu không tìm thấy.</returns>
        Task<DetailAdminMenuViewModel?> GetByIdAsync(long id);

        /// <summary>
        /// Cập nhật thông tin Menu quản trị.
        /// </summary>
        /// <param name="model">Dữ liệu Menu cần cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, ngược lại trả về False.</returns>
        Task<bool> UpdateAsync(UpdateAdminMenuViewModel model);

        /// <summary>
        /// Xóa mềm một Menu quản trị theo ID.
        /// </summary>
        /// <param name="id">ID của Menu cần xóa mềm.</param>
        /// <returns>True nếu thành công, ngược lại trả về False.</returns>
        Task<bool> SoftDeleteAsync(long id);

        /// <summary>
        /// Khôi phục một Menu quản trị đã xóa mềm trước đó.
        /// </summary>
        /// <param name="id">ID của Menu cần khôi phục.</param>
        /// <returns>True nếu thành công, ngược lại trả về False.</returns>
        Task<bool> RestoreAsync(long id);

        /// <summary>
        /// Xóa vĩnh viễn (xóa cứng) một Menu quản trị theo ID.
        /// </summary>
        /// <param name="id">ID của Menu cần xóa vĩnh viễn.</param>
        /// <returns>True nếu thành công, ngược lại trả về False.</returns>
        Task<bool> HardDeleteAsync(long id);

        /// <summary>
        /// Lấy danh sách các Menu cha (ParentId là null).
        /// </summary>
        /// <returns>Danh sách các AdminMenu cha.</returns>
        Task<List<AdminMenu>> GetParentMenusAsync();

        /// <summary>
        /// Kiểm tra xem mã menu (MenuCode) đã được sử dụng hay chưa (loại trừ menu có ID được chỉ định).
        /// </summary>
        Task<bool> IsExistMenuCodeAsync(string menuCode, long id = 0);

        /// <summary>
        /// Kiểm tra xem tên menu (MenuName) đã được sử dụng hay chưa (loại trừ menu có ID được chỉ định).
        /// </summary>
        Task<bool> IsExistMenuNameAsync(string menuName, long id = 0);
    }
}
