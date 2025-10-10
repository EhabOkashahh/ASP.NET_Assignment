using ASP.NET_Assignment.DAL.Models;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.CodeDom;
using System.Drawing.Text;

namespace ASP.NET.Assignment.PL.Helpers
{
    public class RequiredRoleLevelAttribute : TypeFilterAttribute
    {
        public RequiredRoleLevelAttribute(int minLevel) : base(typeof(RequiredRoleLevelFilter))
        {
            Arguments = new object[] { minLevel};
        }

        private class RequiredRoleLevelFilter : IAsyncAuthorizationFilter
        {
            private readonly int _minLevel;
            public UserManager<AppUser> _userManager { get; }
            public RoleService _roleSerivce { get; }

            public RequiredRoleLevelFilter(int minLevel , UserManager<AppUser> userManager , RoleService roleSerivce)
            {
                _minLevel = minLevel;
                _userManager = userManager;
                _roleSerivce = roleSerivce;
            }


            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;

                if (user?.Identity?.IsAuthenticated != true)
                {
                    context.Result = new RedirectToActionResult("SignIn", "Account",null);
                    return;
                }

                var appUserID = _userManager.GetUserId(user);
                var appUser = await _userManager.FindByIdAsync(appUserID);
                if (appUser == null) {
                    context.Result = new ViewResult { ViewName = "Error" };
                    return;
                }

                var Roles = await _userManager.GetRolesAsync(appUser);
                var level =await _roleSerivce.GetHighetRoleLevelAsync(Roles);

                if(level  <  _minLevel)
                {
                    context.Result = new ViewResult { ViewName = "AccessDenied" };
                    return;
                }
            }
        }
    }
}
