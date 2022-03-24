using CyS___DeportesConcepcioin_v2.Controllers;
using CyS___DeportesConcepcioin_v2.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace CyS___DeportesConcepcioin_v2.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var oUser = (usuario)HttpContext.Current.Session["rut_usuario"];
                string oTipo = (string)HttpContext.Current.Session["tipo_usuario"];

                if (oUser == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Login/IndexLogin");
                    }
                }
                else
                {
                    if (filterContext.Controller is LoginController == true)
                    {
                        if (oTipo == "cero")
                        {
                            filterContext.HttpContext.Response.Redirect("~/Admin/IndexAdmin");
                        }
                        else if (oTipo == "uno")
                        {
                            filterContext.HttpContext.Response.Redirect("~/Kine/IndexKine");
                        }
                        else if (oTipo == "dos")
                        {
                            filterContext.HttpContext.Response.Redirect("~/Nutri/IndexNutri");
                        }
                        else if (oTipo == "tres")
                        {
                            filterContext.HttpContext.Response.Redirect("~/CTecnico/IndexCTecnico");
                        }
                    }
                }

                base.OnActionExecuting(filterContext);

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("/Login/login");
            }
        }
    }
}