using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.DAL.Dto;
using SemesterWork.Services;

namespace SemesterWork.Pages
{
    public class LoginModel : PageModel
    {
        private AuthService authService;
        public LoginModel(AuthService authService)
        {
            this.authService = authService;
        }

        [BindProperty]
        public LoginDto Input { get; set; }

        public void OnGet()
        {
            Input = new LoginDto();
            Page();
        }

        public async Task<IActionResult> OnPost()
        {

            var tok = await authService.Login(Input);
            HttpContext.Response.Cookies.Append("x-token", tok, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddMinutes(10080),
            });
            
            return new RedirectToPageResult(pageName: "Profile");
        }

    }
}
