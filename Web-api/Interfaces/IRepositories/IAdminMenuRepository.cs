using Web_api.Models;

namespace Web_api.Interfaces.IRepositories
{
    public interface IAdminMenuRepository : IBaseRepository<AdminMenu>
    {
        /// <summary>
        /// Kiểm tra xem mã menu (MenuCode) đã tồn tại hay chưa (loại trừ menu có ID được chỉ định).
        /// </summary>
        Task<bool> IsExistMenuCodeAsync(string menuCode, long id = 0);

        /// <summary>
        /// Kiểm tra xem tên menu (MenuName) đã tồn tại hay chưa (loại trừ menu có ID được chỉ định).
        /// </summary>
        Task<bool> IsExistMenuNameAsync(string menuName, long id = 0);

        /// <summary>
        /// Lấy danh sách tất cả các menu cha (ParentId là null) đang hoạt động.
        /// </summary>
        Task<List<AdminMenu>> GetParentMenusAsync();
    }
}
