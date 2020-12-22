using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.DAL.Dto;
using SemesterWork.Libs.Filters;
using SemesterWork.Services;

namespace SemesterWork.Pages
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class RegisterModel : PageModel
    {
        public string Title = "Регистрация";

        [BindProperty]
        public RegisterDto Input { get; set; }

        private AuthService authService;
        public RegisterModel(AuthService authService)
        {
            this.authService = authService;
        }

        public void OnGet()
        {
            Input = new RegisterDto();
            Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await authService.Register(Input);
                return new RedirectToPageResult(pageName: "Login");
            }
            else
            {
                Console.WriteLine(ModelState.Values);
                return new RedirectToPageResult(pageName: "Register");
            }
        }
    }
}
