using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.Services;

namespace SemesterWork.Pages
{

    public class diModel : PageModel
    {
        public string Hello => this.service.GetHello();

        private HelloService service;

        public diModel(HelloService service)
        {
            this.service = service;
        }

        public void OnGet()
        {
            Page();
        }
    }
}
