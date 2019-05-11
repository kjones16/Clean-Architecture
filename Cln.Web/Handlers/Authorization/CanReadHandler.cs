using Cln.Api.Handlers.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Cln.Api.Authorization
{
    public class CanReadHandler : AuthorizationHandler<CanReadRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanReadRequirement requirement)
        {
            // Do some logic to verify they can read data.
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
