using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace RealEstateManagement_MinhNTSE196686.Filter
{
    public class RoleAuthorizationFilter : IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var pageType = context.HandlerInstance?.GetType();
            var attribute = pageType?.GetCustomAttribute<RequireRoleAttribute>();

            if (attribute == null) return;

            var userRole = context.HttpContext.Session.GetInt32("UserRole");

            if (userRole == null || !attribute.AllowedRoles.Contains(userRole.Value))
            {
                context.Result = new RedirectToPageResult("/Login");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
