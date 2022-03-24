using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CyS___DeportesConcepcioin_v2.Models;

namespace CyS___DeportesConcepcioin_v2.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult IndexLogin()
        {
            return View();
        }

        public ActionResult Acceso(string rut_usuario, string contrasenia)
        {
            try
            {

                using (Entities db = new Entities())
                {
                    var lts = from d in db.usuario
                              where d.rut_usuario == rut_usuario.Trim() && d.contrasenia == contrasenia.Trim()
                              select d;

                    int tipo_user = (from d in db.usuario
                                     where d.rut_usuario == rut_usuario.Trim() && d.contrasenia == contrasenia.Trim()
                                     select d.tipo_usuario).SingleOrDefault();

                    if (lts.Count() > 0 && tipo_user == 0)
                    {
                        usuario oUser = lts.First();
                        Session["rut_usuario"] = oUser;
                        Session["tipo_usuario"] = "cero";
                        return Content("Administrador");

                    }
                    else if (lts.Count() > 0 && tipo_user == 1)
                    {
                        usuario oUser = lts.First();
                        Session["rut_usuario"] = oUser;
                        Session["tipo_usuario"] = "uno";
                        return Content("Kinesiologo");
                    }
                    else if (lts.Count() > 0 && tipo_user == 2)
                    {
                        usuario oUser = lts.First();
                        Session["rut_usuario"] = oUser;
                        Session["tipo_usuario"] = "dos";
                        return Content("Nutricionista");
                    }
                    else if (lts.Count() > 0 && tipo_user == 3)
                    {
                        usuario oUser = lts.First();
                        Session["rut_usuario"] = oUser;
                        Session["tipo_usuario"] = "tres";
                        return Content("CTecnico");
                    }
                    else if (lts.Count() > 0 && tipo_user == 4)
                    {
                        usuario oUser = lts.First();
                        Session["rut_usuario"] = oUser;
                        Session["tipo_usuario"] = "cuatro";
                        return Content("Futbolista");
                    }
                    else
                    {
                        return Content("Usuario o Contraseña Incorrectos");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Content("Ocurrio un error" + ex.Message);
            }
        }
        public virtual RedirectToRouteResult CerrarSession()
        {
            Session.Abandon();
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "IndexLogin" }));
        }

    }
}