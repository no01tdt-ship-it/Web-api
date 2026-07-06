using Microsoft.AspNetCore.Mvc;

namespace Web_api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Chuyển hướng khi người dùng truy cập /admin sang trang quản trị mong muốn.
        /// </summary>
        [HttpGet("/admin")]
        public IActionResult AdminRedirect()
        {
            // Hiện tại chuyển hướng về danh sách học sinh. 
            // Bạn có thể đổi sang "/admin-menu/admin/list" nếu muốn vào trang quản lý menu trước.
            return Redirect("/student/admin/list");
        }
    }
}
