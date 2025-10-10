using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASP.NET.Assignment.PL.Helpers
{
    public class RoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CanEditUserAsync(AppUser current , AppUser target)
        {
            var currentRoles =await _userManager.GetRolesAsync(current);
            var targetRoles =await _userManager.GetRolesAsync(target);
            
            var currentLevel =await GetHighetRoleLevelAsync(currentRoles);
            var targetLevel = await GetHighetRoleLevelAsync(targetRoles);


            return currentLevel > targetLevel;
        }

        public async Task<int> GetHighetRoleLevelAsync(IEnumerable<string> RoleNames)
        {
            var rolesLevel = _roleManager.Roles.Where(R=> RoleNames.Contains(R.Name)).Select(R => R.Level);

            return await rolesLevel.AnyAsync() ? await rolesLevel.MaxAsync() : 1;
        }
        
    }
}
