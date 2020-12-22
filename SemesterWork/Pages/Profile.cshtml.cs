using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;
using SemesterWork.DAL.Mapper;
using SemesterWork.Libs.Filters;
using SemesterWork.Services;

namespace SemesterWork.Pages
{
    [ServiceFilter(typeof(OnlyAuthorized))]
    public class Profile : PageModel
    {
        public ProfileResponse ProfileResponse { get; set; }
        [BindProperty]
        public EditProfileDto Input { get; set; }
        
        private AuthService authService;
        private UserService _userService;
        
        public Profile(AuthService authService, UserService userService)
        {
            this.authService = authService;
            _userService = userService;
        }
        
        public async Task OnGet()
        {
            Input = new EditProfileDto();

            var userId = HttpContext.Items["userId"].ToString();
            var user = await authService.GetUserById(long.Parse(userId));
            ProfileResponse = UserEntityMapper.MapUserToProfileResponse(user);
            Page();
        }

        public async Task OnPost()
        {
            var userId = HttpContext.Items["userId"].ToString();
            var user = await authService.GetUserById(long.Parse(userId));

            await _userService.EditProfile(user, Input);
            ProfileResponse = UserEntityMapper.MapUserToProfileResponse(user);
            Redirect("Index");
        }
    }
}