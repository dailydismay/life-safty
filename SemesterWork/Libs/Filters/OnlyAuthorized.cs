using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterWork.Services;

namespace SemesterWork.Libs.Filters
{
    public class OnlyAuthorized : IAsyncPageFilter
    {
        private AuthService authService { get; set; }
        
        public OnlyAuthorized(AuthService authService)
        {
            this.authService = authService;
        }
        
        public async Task OnPageHandlerExecutionAsync
            (PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var page = context.HandlerInstance as PageModel;
            if (page == null) return;
    
            var cookie = page.HttpContext.Request.Cookies.TryGetValue("x-token", out string token);
    
            if (!cookie)
            {
                context.Result = new RedirectToPageResult("Login");
                return;
            }
            
            var user = await authService.IdentifyUser(token);
            page.HttpContext.Items["userId"] = user.ToString();
            page.ViewData["userId"] = user.ToString();
            
            page.ViewData["Host"] = context.HttpContext.Request.Host.Host;
            await next();
        }
    
        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }
    }
}
