using ForTestIdeas.Controllers;
using ForTestIdeas.Domain;
using HelperAPI.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTestIdeas.Middlaware
{
    public class HelperAunthenticationMiddleware : IMiddleware
    {
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly TestContext _dbContext;

        public HelperAunthenticationMiddleware(IDataProtectionProvider protectionProvider, TestContext dbContext)
        {
            _protectionProvider = protectionProvider;
            _dbContext = dbContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Query.TryGetValue("authKey", out var key) )
            {
                try
                {
                    var protector = _protectionProvider.CreateProtector("User-auth");
                    var decryptedKey = protector.Unprotect(key);
                    var user = JsonConvert.DeserializeObject<User>(decryptedKey);
                    var actualUser = _dbContext.Users.SingleOrDefault(x => x.Id == user.Id);
                    context.Items.Add("auth-key", actualUser);
                    //context.Session.GetString("auth-key", actualUser);
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                } 
            }                  
            await next(context);
        }
    }
}
