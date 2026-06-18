using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web_api.Interfaces.IServices;
using Web_api.ViewModels.Student;

namespace Web_api.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("admin/list")]
        public IActionResult Index()
        {
            return View("AdminList");
        }

        [HttpPost("api/add")]
        public async Task<IActionResult> Add([FromBody] AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var isSuccess = await _studentService.CreateStudentAsync(viewModel);
                if (isSuccess)
                {
                    return Ok(new { success = true, message = "Thêm mới học sinh thành công!" });
                }
                return BadRequest(new { success = false, message = "Không thể lưu dữ liệu." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }



        [HttpPost("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] StudentViewModelParameters parameters)
        {
            try
            {
                var result = await _studentService.ListServerSide(parameters);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống khi tải danh sách: " + ex.Message });
            }
        }


        [HttpGet("api/List")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var data = await _studentService.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }

        [HttpGet("api/detail/{id}")]
        public async Task<IActionResult> GetByIdAync(int id)
        {
            try
            {
                var data = await _studentService.GetByIdAsync(id);
                if(data == null){
                    return NotFound(new {message = $"Không tìm thấy học sinh có mã {id}"});
                }
                return Ok(data);
            }
            catch (Exception ex){
                return StatusCode(500,$"Lỗi hệ thống khi lấy chi tiết: {ex.Message}" );
            }
        }


    }
}
