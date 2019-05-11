using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Cln.Api.Handlers.Authorization
{
    public class ProjectAuthorizationHandler : AuthorizationHandler<ProjectAccessRequirement, long>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAccessRequirement requirement, long resource)
        {
            // TODO: Implement better example of validation
            if (resource > 0 && resource <= 5)
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}
