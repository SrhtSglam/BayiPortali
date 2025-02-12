using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace project.webapp.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int[] _allowedRoles;

        public CustomAuthorizeAttribute(params int[] roles)
        {
            _allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Session.GetInt32("UserRole");

            if (!userRole.HasValue || userRole == 0 || !_allowedRoles.Contains(userRole.Value))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
