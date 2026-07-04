using Microsoft.AspNetCore.Mvc;
using Web_api.Interfaces.IServices;
using Web_api.ViewModels.AdminMenu;

namespace Web_api.Controllers
{
    [Route("adminmenu")]
    [ApiController]
    public class AdminMenuController : Controller
    {
        private readonly IAdminMenuService _adminMenuService;

        public AdminMenuController(IAdminMenuService adminMenuService)
        {
            _adminMenuService = adminMenuService;
        }

        /// <summary>
        /// Trả về view danh sách quản lý menu.
        /// </summary>
        [HttpGet("admin/list")]
        public IActionResult Index()
        {
            return View("AdminList");
        }

        /// <summary>
        /// API thêm mới một menu quản trị.
        /// </summary>
        [HttpPost("api/add")]
        public async Task<IActionResult> Add([FromBody] AddAdminMenuViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isSuccess = await _adminMenuService.CreateMenuAsync(viewModel);
                if (!isSuccess)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Mã menu hoặc tên menu đã tồn tại, hoặc menu cha không hợp lệ."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Thêm mới menu thành công!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// API lấy danh sách tất cả các menu.
        /// </summary>
        [HttpGet("api/list")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var data = await _adminMenuService.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi tải danh sách: " + ex.Message });
            }
        }

        /// <summary>
        /// API lấy chi tiết một menu theo ID.
        /// </summary>
        [HttpGet("api/detail/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var data = await _adminMenuService.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFound(new { success = false, message = $"Không tìm thấy menu có mã {id}" });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Lỗi hệ thống khi lấy chi tiết: {ex.Message}" });
            }
        }

        /// <summary>
        /// API cập nhật thông tin menu.
        /// </summary>
        [HttpPut("api/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAdminMenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _adminMenuService.UpdateAsync(model);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Cập nhật menu thất bại. Hãy kiểm tra lại trùng lặp mã/tên, phân cấp vòng lặp hoặc menu cha không tồn tại." });
                }

                return Ok(new { success = true, message = "Cập nhật thông tin menu thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Lỗi hệ thống khi cập nhật: {ex.Message}" });
            }
        }

        /// <summary>
        /// API xóa mềm menu (đặt Active = false).
        /// </summary>
        [HttpDelete("api/delete/{id}")]
        public async Task<IActionResult> DeleteMenu(long id)
        {
            try
            {
                var result = await _adminMenuService.SoftDeleteAsync(id);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Xóa menu thất bại. Không tìm thấy menu hoặc menu này là menu hệ thống không thể xóa." });
                }
                return Ok(new { success = true, message = "Xóa menu thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi xóa menu: " + ex.Message });
            }
        }

        /// <summary>
        /// API khôi phục menu đã bị xóa mềm (đặt Active = true).
        /// </summary>
        [HttpPost("api/restore/{id}")]
        public async Task<IActionResult> RestoreMenu(long id)
        {
            try
            {
                var result = await _adminMenuService.RestoreAsync(id);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Khôi phục menu thất bại. Không tìm thấy menu." });
                }
                return Ok(new { success = true, message = "Khôi phục menu thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi khôi phục menu: " + ex.Message });
            }
        }

        /// <summary>
        /// API xóa cứng (Xóa vĩnh viễn menu khỏi database).
        /// </summary>
        [HttpDelete("api/hard-delete/{id}")]
        public async Task<IActionResult> HardDeleteMenu(long id)
        {
            try
            {
                var result = await _adminMenuService.HardDeleteAsync(id);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Xóa vĩnh viễn thất bại. Không tìm thấy menu, menu hệ thống, hoặc menu đang chứa menu con." });
                }
                return Ok(new { success = true, message = "Xóa vĩnh viễn menu thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống khi xóa vĩnh viễn menu: " + ex.Message });
            }
        }

        /// <summary>
        /// API lấy danh sách các menu cha đang hoạt động (dùng cho dropdown lựa chọn menu cha).
        /// </summary>
        [HttpGet("api/parent-menus")]
        public async Task<IActionResult> GetParentMenus()
        {
            try
            {
                var parents = await _adminMenuService.GetParentMenusAsync();
                return Ok(parents.Select(p => new { id = p.Id, name = p.MenuName }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
