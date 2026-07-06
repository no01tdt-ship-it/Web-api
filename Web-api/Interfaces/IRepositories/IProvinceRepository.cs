using Web_api.Models;
using Web_api.ViewModels.Common;
using Web_api.ViewModels.Province;

namespace Web_api.Interfaces.IRepositories
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        // Phục vụ phân trang phía server (server-side pagination)
        Task<DTResult<ListProvinceViewModel>> ListServerSide(ProvinceViewModelParameters obj);
        // Kiểm tra mã tỉnh/thành phố đã tồn tại hay chưa (dùng cho trường hợp thêm hoặc cập nhật)
        Task<bool> IsExistCodeAsync(string code, long id = 0);
    }
}
