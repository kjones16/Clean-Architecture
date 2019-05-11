﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cln.Web.Api.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// Checks to see if any controller parameter implements ITodoListMember. If so it will validate that the user has access to the list.
    /// </summary>
    public sealed class ProjectAuthorizationFilter : ActionFilterAttribute
    {
        private readonly IAuthorizationService _authorizationService;

        public ProjectAuthorizationFilter(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("projectId", out object projectId) &&
                _authorizationService.AuthorizeAsync(context.HttpContext.User, projectId, "TodoAccessPolicy").Result.Succeeded)
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}
