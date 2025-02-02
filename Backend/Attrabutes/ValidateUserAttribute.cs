using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Attrabutes
{
    public class ValidateUserAttribute : Attribute, IAsyncAuthorizationFilter
    {
        //IAsnycAuthorization gives access to httpcontext and provides the values within the token
        private readonly List<string> _permissions;


        public ValidateUserAttribute(params string[] permissions){
            _permissions = permissions.ToList();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context){
            var userValidationService = context.HttpContext.RequestServices.GetRequiredService<IUserValidationService>();
            //Reason the depedency injection is because we cannot make dependency injection in a constructor for attrabutes
            var user = context.HttpContext.User;   
            var username = user.FindFirst(ClaimTypes.Name)?.Value;
            var projectId = Convert.ToInt32(user.FindFirst("Project")?.Value);
            var role = user.FindFirst(ClaimTypes.Role)?.Value;


            if(string.IsNullOrEmpty(username) || projectId ==0 || string.IsNullOrEmpty(role)){
                context.Result = new UnauthorizedResult();

                return;
            }

            var IsValid = await userValidationService.ValidationUserAndProjectAsync(username,projectId,role,_permissions);
            if(!IsValid)
            {
                context.Result = new ForbidResult();

            }

        }
        
    }
}