using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;
using SemesterWork.Libs.Filters;
using SemesterWork.Services;

namespace SemesterWork.Pages
{
    [ServiceFilter(typeof(OnlyAuthorized))]
    public class Apply : PageModel
    {
        public List<string> Specializations;
        private DoctorService _doctorService;

        [BindProperty]
        public ApplyDoctorDto Input { get; set; }
        
        public Apply(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        
        public async Task OnGet()
        {
            Input = new ApplyDoctorDto();
            Specializations = await _doctorService.ListAllSpecializations();
            Page();
        }

        public async Task OnPost()
        {
            var userId = HttpContext.Items["userId"].ToString();
            await _doctorService.Create(long.Parse(userId), Input);
            Specializations = await _doctorService.ListAllSpecializations();
            Redirect("Profile");
        }
    }
}