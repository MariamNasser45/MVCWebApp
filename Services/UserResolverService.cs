using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using ProductCatalog.Data;
using ProductCatalog.Interfaces;
using System.Security.Claims;

namespace ProductCatalog.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserResolverService(IHttpContextAccessor httpContext, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _context = context;

        }

        public async Task<bool> CheckRole(string userId, string roleName)
        {
            var user =await _userManager.FindByIdAsync(userId);

            if(user!=null)
                 return await _userManager.IsInRoleAsync(user, roleName);
            return false;
        }

        public string GetUserId()
        {
            string userId = "";

            var con = _httpContext.HttpContext;

            if (con == null)
            {
                return "";
            }
            else
            {
                var user = _httpContext.HttpContext.User;

                if (user == null)
                {
                    return "";
                }
                else
                {
                    var userData = user.Claims.FirstOrDefault(c => c.Type == "uid");

                    userId = "";

                    if (userData != null)
                    {
                        userId = userData.Value;
                        return userId;
                    }
                    else
                    {

                        userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        return userId;

                    }
                }
            }




        }

        public async Task<string> GetUserName(string userId)
        {
            if (userId!=null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user!=null)
                    return  user.UserName;
                else
                    return "";
            }
            else
                return "";
        }
    }
}
