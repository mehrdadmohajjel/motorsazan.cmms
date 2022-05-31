using Motorsazan.CMMS.Client.Filters;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandlerFilter());
            filters.Add(new JsonResultFilter());
        }
    }
}