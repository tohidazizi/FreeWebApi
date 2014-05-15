using System.Web;
using System.Web.Mvc;

namespace Thrita.Web.Api.FreeWebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
