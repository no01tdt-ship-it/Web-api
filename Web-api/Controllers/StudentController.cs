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
            return View();
        }

        [HttpPost("api/add")]
        public async Task<IActionResult> Add([FromBody] AddStudentViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
                {
                var isSuccess = await _studentService.CreateStudentAsync(viewModel);
                if (isSuccess){return Ok(new{success = true,message = "Thêm mới học sinh thành công!"});
                }
                return BadRequest(new {success = false,message = "Không thể lưu dữ liệu."});
            }
            catch(Exception ex){
                return StatusCode(500, new {success = false,message = "Lỗi hệ thống: " + ex.Message});
            }
        }


    }
}
