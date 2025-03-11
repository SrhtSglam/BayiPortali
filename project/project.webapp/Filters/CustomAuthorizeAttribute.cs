using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using project.entity;

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
            var userRole = WebLoginUser.Visibility;

            if (userRole == 0)
                context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }
}
