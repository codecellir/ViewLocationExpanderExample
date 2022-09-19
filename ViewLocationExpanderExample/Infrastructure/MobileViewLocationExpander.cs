using Microsoft.AspNetCore.Mvc.Razor;

namespace ViewLocationExpanderExample.Infrastructure
{
    public class MobileViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var finalViewLocations=new List<string>();

            if (context.Values.ContainsKey("CodeCellMobileKey"))
            { 
                foreach (var viewLocation in viewLocations)
                {
                    finalViewLocations.Add(viewLocation.Replace(".cshtml", ".mobile.cshtml"));
                }
            }
            finalViewLocations.AddRange(viewLocations);

            return finalViewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var userAgent = context.ActionContext.HttpContext.Request.Headers["User-Agent"].ToString();
            if (userAgent.RequestFromMobileDevice())
            {
                context.Values["CodeCellMobileKey"] = "mobile";
            }
        }
    }
}
