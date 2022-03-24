using CyS___DeportesConcepcioin_v2.Models;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels.CTecnico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyS___DeportesConcepcioin_v2.Controllers
{
    public class CTecnicoController : Controller
    {
        #region Vistas Cuerpo Tecnico
        public ActionResult IndexCTecnico()
        {
            CTecnicoViewModel cTecnicoViewModel = new CTecnicoViewModel();

            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                if (oUser != null)
                {
                    string oUsuario = oUser.rut_usuario.ToString();
                    var oCTec = db.usuario.Find(oUsuario);
                    var ocTecnico = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == oUsuario).FirstOrDefault();

                    cTecnicoViewModel.TelefonoCTecnico = ocTecnico.telefono_ct;
                    cTecnicoViewModel.EmailCTecnico = ocTecnico.email_ct;
                    cTecnicoViewModel.PassCTecnico = oCTec.contrasenia;
                }
            }

            return View(cTecnicoViewModel);
        } 

        public ActionResult ListadoEntrenamientos()
        {
            List<EntrenamientosViewModel> listEntrenamientos = null;

            using (Entities db = new Entities())
            {
                listEntrenamientos =  (from en in db.entrenamiento
                                       join te in db.tipo_entrenam on en.tipo_entrenam_id_tipo_entr equals te.id_tipo_entr
                                       join ct in db.cuerpo_tec on en.cuerpo_tec_id_cu_tec equals ct.id_cu_tec
                                       orderby en.fecha_entr descending
                                       select new EntrenamientosViewModel()
                                       {
                                           IdEntrenamiento = en.id_entrenamiento,
                                           tipo_entr = te.nomb_entrenamiento,
                                           volumen_entr = en.volumen_entr,
                                           objetivo_entr = en.objetivo_entr,
                                           fecha_entr = en.fecha_entr,
                                           categoria_id_categoria = en.categoria_id_categoria,
                                           nom_ct = ct.nom_ct,
                                           appaterno_ct = ct.appaterno_ct,
                                           apmaterno_ct = ct.apmaterno_ct
                                       }).ToList();
            }
            return View(listEntrenamientos);
        }

        public ActionResult AgregarEntrenamiento()
        {
            List<TipoEntrenamientoViewModel> lstEntrenamientos = null;

            using (var db = new Entities())
            {
                lstEntrenamientos = (from te in db.tipo_entrenam
                                     orderby te.nomb_entrenamiento ascending
                                     select new TipoEntrenamientoViewModel
                                     {
                                         IdTipoEntrenamiento = te.id_tipo_entr,
                                         NombEntrenamiento = te.nomb_entrenamiento
                                     }).ToList();
            }

            List<SelectListItem> entrenamientos = lstEntrenamientos.ConvertAll(te =>
            {
                return new SelectListItem()
                {
                    Text = te.NombEntrenamiento.ToString(),
                    Value = te.IdTipoEntrenamiento.ToString(),
                    Selected = false
                };
            });

            ViewBag.Entrenamientos = entrenamientos;

            return View();
        }

        [HttpGet]
        public ActionResult ActualizarEntrenamiento(int IdEntrenamiento)
        {
            EntrenamientosViewModel entrenamientosViewModel = new EntrenamientosViewModel();

            using (var db = new Entities())
            {
                var oEntrenamiento = db.entrenamiento.Where(en => en.id_entrenamiento == IdEntrenamiento).FirstOrDefault();
                var oTipoEntrenamiento = db.tipo_entrenam.Where(te => te.id_tipo_entr == oEntrenamiento.tipo_entrenam_id_tipo_entr).FirstOrDefault();

                entrenamientosViewModel.tipo_entr = oTipoEntrenamiento.nomb_entrenamiento;
                entrenamientosViewModel.volumen_entr = oEntrenamiento.volumen_entr;
                entrenamientosViewModel.fecha_entr = oEntrenamiento.fecha_entr;
                entrenamientosViewModel.categoria_id_categoria = oEntrenamiento.categoria_id_categoria;
                entrenamientosViewModel.objetivo_entr = oEntrenamiento.objetivo_entr;
            }

            List<TipoEntrenamientoViewModel> lstEntrenamientos = null;

            using (var db = new Entities())
            {
                lstEntrenamientos = (from te in db.tipo_entrenam
                                     orderby te.nomb_entrenamiento ascending
                                     select new TipoEntrenamientoViewModel
                                     {
                                         IdTipoEntrenamiento = te.id_tipo_entr,
                                         NombEntrenamiento = te.nomb_entrenamiento
                                     }).ToList();
            }

            List<SelectListItem> entrenamientos = lstEntrenamientos.ConvertAll(te =>
            {
                return new SelectListItem()
                {
                    Text = te.NombEntrenamiento.ToString(),
                    Value = te.IdTipoEntrenamiento.ToString(),
                    Selected = false
                };
            });

            ViewBag.Entrenamientos = entrenamientos;

            return View(entrenamientosViewModel);
        }
        #endregion

        #region Metodos para Agregar
        [HttpPost]
        public ActionResult AgregarEntrenamiento(EntrenamientosViewModel entrenamientosViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];

                if (oUser != null)
                {
                    string oUsuario = oUser.rut_usuario.ToString();
                    var oCTec = db.usuario.Find(oUsuario);
                    var oCTecnico = db.cuerpo_tec.Where(en => en.usuario_rut_usuario == oUsuario).FirstOrDefault();

                    entrenamiento entrenamiento = new entrenamiento();

                    entrenamiento.tipo_entrenam_id_tipo_entr = entrenamientosViewModel.tipo_entrenam_id_tipo_entr;
                    entrenamiento.volumen_entr = entrenamientosViewModel.volumen_entr;
                    entrenamiento.fecha_entr = entrenamientosViewModel.fecha_entr;
                    entrenamiento.objetivo_entr = entrenamientosViewModel.objetivo_entr;
                    entrenamiento.categoria_id_categoria = entrenamientosViewModel.categoria_id_categoria;
                    entrenamiento.cuerpo_tec_id_cu_tec = oCTecnico.id_cu_tec;

                    db.entrenamiento.Add(entrenamiento);
                    db.SaveChanges();
                }
            }

            return Redirect(Url.Content("~/CTecnico/ListadoEntrenamientos"));
        } 
        #endregion

        #region Metodos Para Editar
        [HttpPost]
        public ActionResult IndexCTecnico(CTecnicoViewModel cTecnicoViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                string oUsuario = oUser.rut_usuario.ToString();
                var oCTec = db.usuario.Find(oUsuario);
                var ocTecnico = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == oUsuario).FirstOrDefault();

                if (cTecnicoViewModel.PassCTecnico.Equals(cTecnicoViewModel.ConfirmarPassCTecnico))
                {
                    oCTec.contrasenia = cTecnicoViewModel.PassCTecnico;
                    ocTecnico.email_ct = cTecnicoViewModel.EmailCTecnico;
                    ocTecnico.telefono_ct = cTecnicoViewModel.TelefonoCTecnico;

                    db.Entry(oCTec).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ocTecnico).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return Redirect(Url.Content("~/CTecnico/IndexCTecnico"));
        }

        [HttpPost]
        public ActionResult ActualizarEntrenamiento(EntrenamientosViewModel entrenamientosViewModel)
        {
            using (var db = new Entities())
            {
                var oEntrenamiento = db.entrenamiento.Where(en => en.id_entrenamiento == entrenamientosViewModel.IdEntrenamiento).FirstOrDefault();
                
                oEntrenamiento.tipo_entrenam_id_tipo_entr = entrenamientosViewModel.tipo_entrenam_id_tipo_entr;
                oEntrenamiento.volumen_entr = entrenamientosViewModel.volumen_entr;
                oEntrenamiento.fecha_entr = entrenamientosViewModel.fecha_entr;
                oEntrenamiento.categoria_id_categoria = entrenamientosViewModel.categoria_id_categoria;
                oEntrenamiento.objetivo_entr = entrenamientosViewModel.objetivo_entr;

                db.Entry(oEntrenamiento).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/CTecnico/ListadoEntrenamientos"));
        }
        #endregion

        public ActionResult EliminarEntrenamiento(int IdEntrenamiento)
        {
            using (var db = new Entities())
            {
                var oEntrenamiento = db.entrenamiento.Where(en => en.id_entrenamiento == IdEntrenamiento).FirstOrDefault();

                db.entrenamiento.Remove(oEntrenamiento);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/CTecnico/ListadoEntrenamientos"));
        }

        #region Otros Metodos
        public ActionResult ObtenerFechaActual()
        {

            string dia = DateTime.Now.ToString("dd");
            string mes = DateTime.Now.ToString("MMMM").ToUpper();
            string anio = DateTime.Now.ToString("yyyy");

            return Content(dia + " DE " + mes + " DE " + anio);
        }

        public ActionResult ObtenerNombreProfesional()
        {
            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                string oUsuario = oUser.rut_usuario.ToString();

                var ocTecnico = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == oUsuario).FirstOrDefault();

                var NombreCTecnico = ocTecnico.nom_ct;
                var ApPaternoCTecnico = ocTecnico.appaterno_ct;
                var ApMaternoCTecnico = ocTecnico.apmaterno_ct;

                return Content(NombreCTecnico + " " + ApPaternoCTecnico + " " + ApMaternoCTecnico);
            }
        } 
        #endregion
    }
}