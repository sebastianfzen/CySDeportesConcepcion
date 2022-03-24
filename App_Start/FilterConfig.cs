using System.Web;
using System.Web.Mvc;

namespace CyS___DeportesConcepcioin_v2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificarSession());
        }
    }
}
