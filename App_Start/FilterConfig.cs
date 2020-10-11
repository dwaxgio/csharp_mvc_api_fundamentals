using System.Web;
using System.Web.Mvc;

namespace MVC_API_FUNDAMENTALS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
