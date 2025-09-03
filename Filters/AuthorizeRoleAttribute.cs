using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace CarRentalManagement.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public AuthorizeRoleAttribute(params string[] roles) => _roles = roles;


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var userId = httpContext.Session.GetInt32("UserID");
            var role = httpContext.Session.GetString("Role");


            if (userId is null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { error = "Please log in." });
                return;
            }


            if (_roles.Length > 0 && (role is null || !_roles.Contains(role)))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { error = "Unauthorized" });
            }
        }
    }
}