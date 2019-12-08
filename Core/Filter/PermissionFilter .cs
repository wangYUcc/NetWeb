using Core.implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Filter
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
  {
    public PermissionFilter(string name)
    {
      Name = name;
    }

    private string Name { get; set; }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

       if (context.HttpContext.User.IsInRole("admin")) {   //return;
      }

      var permissionClaim = context. HttpContext
        .User.Claims
        .Where(c =>c.Type == "Permission")
        .ToList();

      if (permissionClaim.Any(c => c.Value == Name))  return;
      else  context.Result = new ForbidResult();   
    }
  }
}
