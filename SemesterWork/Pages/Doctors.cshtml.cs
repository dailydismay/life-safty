using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.DAL.Dto;
using SemesterWork.Libs.Filters;
using SemesterWork.Services;

namespace SemesterWork.Pages
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class Doctors : PageModel
    {
        private DoctorService _doctorService;

        public Pageable<DoctorResponse> DoctorsResponse;

        [FromQuery(Name = "page")] public int PageQ { get; set; } = 0;


        [FromQuery(Name = "perPage")] public int PerPage { get; set; } = 15;

        [FromQuery(Name = "spec")] public string Specialization { get; set; } = "";

        
        public Doctors(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        
        public async Task OnGet()
        {

            var page = "0";

            if (!Request.Query["page"].Equals(""))
            {
                page = Request.Query["page"];
            }
            
            var perPage = "15";
            if (!Request.Query["perPage"].Equals(""))
            {
                perPage = Request.Query["page"];
            }
            
            var spec = "";
            
            if (!Request.Query["spec"].Equals(String.Empty))
            {
                spec = Request.Query["page"];
            }




            var q = new ListDoctorsDto() {Page = PageQ, PerPage = PerPage, Specialization = Specialization};

            DoctorsResponse = await _doctorService.List(q);
            Page();
        }
    }
}