using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SemesterWork.Pages
{
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Response.Cookies.Delete("x-token");
            return new RedirectToPageResult(pageName: "Register");
        }
    }
}