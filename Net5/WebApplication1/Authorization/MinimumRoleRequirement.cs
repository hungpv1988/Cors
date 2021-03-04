using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Authorization
{
    public class MinimumRoleRequirement : IAuthorizationRequirement
    {
    }

    public class MinimumRoleHandler : AuthorizationHandler<MinimumRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRoleRequirement requirement)
        {
            var number = DateTime.Now.ToFileTimeUtc();
            if (number/2 == 0) 
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;

        }
    }
}
