using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web_api.Interfaces.IRepositories
{
    /// <summary>
    /// Interface Repository cơ sở (Generic Repository Pattern) dùng chung cho các Entity.
    /// Giúp tái sử dụng mã nguồn cho các thao tác CRUD cơ bản.
    /// </summary>
    /// <typeparam name="TModel">Kiểu dữ liệu của Entity (ví dụ: Student, SchoolClass,...)</typeparam>
    public interface IBaseRepository<TModel> where TModel : class
    {
        /// <summary>
        /// Lấy toàn bộ danh sách bản ghi kèm theo thông tin phân trang/đếm tổng số bản ghi.
        /// </summary>
        /// <returns>
        /// Trả về một Tuple chứa:
        /// - data: Danh sách các thực thể của trang hiện tại.
        /// - total: Tổng số lượng bản ghi thực tế trong cơ sở dữ liệu.
        /// - totalFiltered: Tổng số lượng bản ghi sau khi đã áp dụng các bộ lọc tìm kiếm.
        /// </returns>
        Task<(List<TModel> data, int total, int totalFiltered)> GetAllAsync();

        /// <summary>
        /// Lấy chi tiết một bản ghi dựa trên ID (khóa chính).
        /// </summary>
        /// <param name="id">Mã định danh duy nhất của bản ghi (kiểu long).</param>
        /// <returns>Trả về thực thể tương ứng hoặc null nếu không tồn tại bản ghi mang mã số này.</returns>
        Task<TModel?> GetAsync(long id);

        /// <summary>
        /// Thêm mới một bản ghi vào DbContext (Lưu ý: Hành động này chỉ thêm vào bộ nhớ tạm thời của EF Core).
        /// </summary>
        /// <param name="obj">Thực thể cần thêm mới.</param>
        Task InsertAsync(TModel obj);

        /// <summary>
        /// Thêm mới hàng loạt bản ghi vào DbContext cùng một lúc.
        /// </summary>
        /// <param name="objs">Danh sách các thực thể cần thêm mới.</param>
        Task InsertManyAsync(IEnumerable<TModel> objs);

        /// <summary>
        /// Cập nhật thông tin một bản ghi trong DbContext.
        /// </summary>
        /// <param name="obj">Thực thể cần cập nhật thông tin.</param>
        Task UpdateAsync(TModel obj);

        /// <summary>
        /// Cập nhật hàng loạt bản ghi trong DbContext cùng một lúc.
        /// </summary>
        /// <param name="objs">Danh sách các thực thể cần cập nhật.</param>
        Task UpdateManyAsync(IEnumerable<TModel> objs);

        /// <summary>
        /// Xóa mềm một bản ghi (Thường là cập nhật trạng thái Active = false hoặc IsDeleted = true thay vì xóa vĩnh viễn).
        /// </summary>
        /// <param name="id">Mã định danh của bản ghi cần xóa mềm.</param>
        /// <returns>Trả về true nếu xóa mềm thành công, ngược lại trả về false.</returns>
        Task<bool> SoftDeleteAsync(long id);

        /// <summary>
        /// Khôi phục một bản ghi đã bị xóa mềm trước đó (Ví dụ đặt Active = true hoặc IsDeleted = false).
        /// </summary>
        /// <param name="id">Mã định danh của bản ghi cần khôi phục.</param>
        /// <returns>Trả về true nếu khôi phục thành công, ngược lại trả về false.</returns>
        Task<bool> RestoreAsync(long id);

        /// <summary>
        /// Xóa cứng (Hard Delete) - Xóa vĩnh viễn bản ghi khỏi cơ sở dữ liệu vật lý.
        /// </summary>
        /// <param name="id">Mã định danh của bản ghi cần xóa vĩnh viễn.</param>
        /// <returns>Trả về true nếu xóa cứng thành công, ngược lại trả về false.</returns>
        Task<bool> HardDeleteAsync(long id);

        /// <summary>
        /// Xác nhận và lưu mọi thay đổi trong DbContext (thêm, sửa, xóa,...) xuống cơ sở dữ liệu SQL Server.
        /// </summary>
        /// <returns>Trả về true nếu lưu thành công ít nhất một bản ghi, ngược lại trả về false.</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Lấy đối tượng DatabaseFacade để quản lý trực tiếp các tác vụ cấp thấp của Database (như Transactions, Migrations, hoặc chạy các câu lệnh SQL raw).
        /// </summary>
        /// <returns>Đối tượng DatabaseFacade liên kết với DbContext hiện tại.</returns>
        DatabaseFacade GetDatabase();
    }
}
