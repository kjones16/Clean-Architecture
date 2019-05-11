using Microsoft.AspNetCore.Mvc.Filters;

namespace Cln.Web.Filters.ExceptionFilters
{
    public class ExceptionToStatusCodeFilter : ExceptionFilterAttribute
    {
        public ExceptionToStatusCodeFilter()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
                context.Result = context.Exception.ToStatusCode(context.ModelState);
        }
    }
}

