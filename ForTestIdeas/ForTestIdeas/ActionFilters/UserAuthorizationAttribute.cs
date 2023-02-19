using ForTestIdeas.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.ActionFilters
{
    public class UserAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        private List<string> _allowedRoles;

        public UserAuthorizationAttribute(string roles)
        {
            _allowedRoles = roles.Split(",").ToList();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Items.TryGetValue("auth-key", out var authenticateUser) == false)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }

            var worker = authenticateUser as User;

            if (worker != null &&  _allowedRoles.Contains(worker.Role))
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
               
        }
    }
}
