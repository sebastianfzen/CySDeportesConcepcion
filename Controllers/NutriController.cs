using CyS___DeportesConcepcioin_v2.Models;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Nutricionista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyS___DeportesConcepcioin_v2.Controllers
{
    public class NutriController : Controller
    {
        #region Vistas Del Nutricionista
        public ActionResult IndexNutri()
        {
            NutriViewModel nutriViewModel = new NutriViewModel();

            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                if (oUser != null)
                {
                    string oUsuario = oUser.rut_usuario.ToString();
                    var oNutri = db.usuario.Find(oUsuario);
                    var oNutricionista = db.nutricionista.Where(n => n.usuario_rut_usuario == oUsuario).FirstOrDefault();

                    nutriViewModel.TelefonoNutricionista = oNutricionista.telefono_nutri;
                    nutriViewModel.EmailNutricionista = oNutricionista.email_nutri;
                    nutriViewModel.PassNutricionista = oNutri.contrasenia;
                }

            }

            return View(nutriViewModel);
        } 

        public ActionResult ListadoJugadores()
        {
            List<FutbolistaViewModelNutri> listFutbolistas = null;
            var DetallesFutbolista = new DetallesFutbolistaViewModel();
            List<EvaluacionNutricionalViewModel> listEvNutri = null;

            using (Entities db = new Entities())
            {
                listFutbolistas = (from f in db.futbolista
                                   join u in db.usuario on f.usuario_rut_usuario equals u.rut_usuario
                                   where f.estado_futb == "1"
                                   orderby f.appaterno_futb
                                   select new FutbolistaViewModelNutri()
                                   {
                                       RutFutbolista = u.rut_usuario,
                                       NomFutbolista = f.nom_futb,
                                       ApPaternoFutbolista = f.appaterno_futb,
                                       EdadFutbolista = f.edad_futb,
                                       id_futbolista = f.id_futbolista,
                                       CategoriaFutbolista = f.categoria_id_categoria
                                   }).ToList();

                DetallesFutbolista.FutbolistaViewModelNutriList = listFutbolistas;

                listEvNutri = (from f in db.futbolista
                               join u in db.usuario on f.usuario_rut_usuario equals u.rut_usuario
                               join e in db.ev_nutri on f.id_futbolista equals e.futbolista_id_futbolista
                               where f.estado_futb == "1"
                               orderby f.appaterno_futb
                               select new EvaluacionNutricionalViewModel()
                               {
                                   talla_estatura = e.talla_estatura,
                                   peso = e.peso,
                                   kilo_masa_adiposa = e.kilo_masa_adiposa,
                                   kilo_musculo = e.kilo_musculo,
                                   porcent_masa_adiposa = e.porcent_masa_adiposa,
                                   porcent_musculo = e.porcent_musculo,
                                   sumatoria_pliegue = e.sumatoria_pliegue,
                                   imo = e.imo,
                                   estado_uno = e.estado_uno,
                                   estado_uno_dos = e.estado_uno_dos,
                                   estado_dos = e.estado_dos,
                                   estado_dos_dos = e.estado_dos_dos,
                                   estado_tres = e.estado_tres,
                                   estado_cuatro = e.estado_cuatro,
                                   futbolista_id_futbolista = e.futbolista_id_futbolista
                               }).ToList();

                DetallesFutbolista.evaluacionNutricionalViewModels = listEvNutri;
            }

            return View(DetallesFutbolista);
        }

        [HttpGet]
        public ActionResult DatosJugador(string RutFutbolista)
        {
            var DetallesFutbolista = new DetallesFutbolistaViewModel();
            FutbolistaViewModelNutri futbolistaViewModelNutri = new FutbolistaViewModelNutri();
            List<EvaluacionNutricionalViewModel> listEvNutri = null;
            List<SuplementoViewModel> listSuplemento = null;
            List<InsumoFutbolista> listInsumo = null;

            using (Entities db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutFutbolista);
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();

                if (oFutbolista != null)
                {
                    futbolistaViewModelNutri.NomFutbolista = oFutbolista.nom_futb + " " + oFutbolista.appaterno_futb + " " + oFutbolista.apmaterno_futb;
                    futbolistaViewModelNutri.RutFutbolista = oUsuario.rut_usuario;
                    futbolistaViewModelNutri.EdadFutbolista = oFutbolista.edad_futb;
                    futbolistaViewModelNutri.FechaNacimientoFutbolista = oFutbolista.fecha_nacim_futb;
                    futbolistaViewModelNutri.PosicionFutbolista = oFutbolista.posicion_futb;
                    futbolistaViewModelNutri.NacionalidadFutbolista = oFutbolista.nacionalidad_futb;
                    futbolistaViewModelNutri.TelefonoFutbolista = oFutbolista.telefono_futb.ToString();
                    futbolistaViewModelNutri.EmailFutbolista = oFutbolista.email_futb;

                    var oEvaNutricional = db.ev_nutri.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oEvaNutricional != null)
                    {
                        listEvNutri = (from e in db.ev_nutri
                                       where e.futbolista_id_futbolista == oEvaNutricional.futbolista_id_futbolista
                                       select new EvaluacionNutricionalViewModel()
                                       {
                                           peso = e.peso,
                                           talla_estatura = e.talla_estatura,
                                           porcent_masa_adiposa = e.porcent_masa_adiposa,
                                           estado_uno = e.estado_uno,
                                           kilo_masa_adiposa = e.kilo_masa_adiposa,
                                           estado_uno_dos = e.estado_uno_dos,
                                           porcent_musculo = e.porcent_musculo,
                                           estado_dos = e.estado_dos,
                                           kilo_musculo = e.kilo_musculo,
                                           estado_dos_dos = e.estado_dos_dos,
                                           sumatoria_pliegue = e.sumatoria_pliegue,
                                           estado_tres = e.estado_tres,
                                           imo = e.imo,
                                           estado_cuatro = e.estado_cuatro
                                       }).ToList();

                        DetallesFutbolista.evaluacionNutricionalViewModels = listEvNutri;
                    }

                    var oSuplemento = db.suplemento.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oSuplemento != null)
                    {
                        listSuplemento =  (from s in db.suplemento
                                           where s.futbolista_id_futbolista == oSuplemento.futbolista_id_futbolista
                                           select new SuplementoViewModel()
                                           {
                                               desc_suplemento = s.desc_suplemento,
                                               tipo_suplemento = s.tipo_suplemento,
                                               cant_suplemento = s.cant_suplemento,
                                               fecha_suplem = s.fecha_suplem,
                                               comentariosuplem = s.comentariosuplem,
                                               RutFutbolista = oFutbolista.usuario_rut_usuario
                                           }).ToList();

                        DetallesFutbolista.suplementoViewModels = listSuplemento;
                    }

                    var oInsumo = db.insumo.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oInsumo != null)
                    {
                        listInsumo = (from i in db.insumo
                                      where i.futbolista_id_futbolista == oInsumo.futbolista_id_futbolista
                                      select new InsumoFutbolista()
                                      {
                                          descripcion_insumo = i.descripcion_insumo,
                                          tipo_insumo = i.tipo_insumo,
                                          cantidad_insumo = i.cantidad_insumo,
                                          fecha_insumo = i.fecha_insumo,
                                          comentarioinsumo = i.comentarioinsumo,
                                          RutFutbolista = RutFutbolista
                                      }).ToList();

                        DetallesFutbolista.insumoFutbolistas = listInsumo;
                    }

                    DetallesFutbolista.FutbolistaViewModelNutri = futbolistaViewModelNutri;
                }
            }

            return View(DetallesFutbolista);
        }

        public ActionResult EvaluacionNutricional(string RutFutbolista)
        {
            return View();
        }

        public ActionResult AgregarSuplemento(string RutFutbolista)
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditarEvaluacionNutricional(string RutFutbolista)
        {
            EvaluacionNutricionalViewModel evaluacionNutricionalViewModel = new EvaluacionNutricionalViewModel();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oEvaNutri = db.ev_nutri.Where(e => e.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    evaluacionNutricionalViewModel.peso = oEvaNutri.peso;
                    evaluacionNutricionalViewModel.talla_estatura = oEvaNutri.talla_estatura;
                    evaluacionNutricionalViewModel.porcent_masa_adiposa = oEvaNutri.porcent_masa_adiposa;
                    evaluacionNutricionalViewModel.estado_uno = oEvaNutri.estado_uno;
                    evaluacionNutricionalViewModel.kilo_masa_adiposa = oEvaNutri.kilo_masa_adiposa;
                    evaluacionNutricionalViewModel.estado_uno_dos = oEvaNutri.estado_uno_dos;
                    evaluacionNutricionalViewModel.porcent_musculo = oEvaNutri.porcent_musculo;
                    evaluacionNutricionalViewModel.estado_dos = oEvaNutri.estado_dos;
                    evaluacionNutricionalViewModel.kilo_musculo = oEvaNutri.kilo_musculo;
                    evaluacionNutricionalViewModel.estado_dos_dos = oEvaNutri.estado_dos_dos;
                    evaluacionNutricionalViewModel.sumatoria_pliegue = oEvaNutri.sumatoria_pliegue;
                    evaluacionNutricionalViewModel.estado_tres = oEvaNutri.estado_tres;
                    evaluacionNutricionalViewModel.imo = oEvaNutri.imo;
                    evaluacionNutricionalViewModel.estado_cuatro = oEvaNutri.estado_cuatro;
                }
            }

            return View(evaluacionNutricionalViewModel);
        }

        [HttpGet]
        public ActionResult EditarSuplemento(string RutFutbolista)
        {
            SuplementoViewModel suplementoViewModel = new SuplementoViewModel();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oSuplemento = db.suplemento.Where(e => e.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    suplementoViewModel.desc_suplemento = oSuplemento.desc_suplemento;
                    suplementoViewModel.tipo_suplemento = oSuplemento.tipo_suplemento;
                    suplementoViewModel.cant_suplemento = oSuplemento.cant_suplemento;
                    suplementoViewModel.fecha_suplem = oSuplemento.fecha_suplem;
                    suplementoViewModel.comentariosuplem = oSuplemento.comentariosuplem;
                }
            }

            return View(suplementoViewModel);
        }
        #endregion

        #region Metodos para Agregar
        [HttpPost]
        public ActionResult EvaluacionNutricional(FutbolistaViewModelNutri futbolistaViewModelNutri)
        {
            usuario User = (usuario)Session["rut_usuario"];
            string oUser = User.rut_usuario.ToString();

            using (Entities db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelNutri.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                var oNutricionista = db.nutricionista.Where(n => n.usuario_rut_usuario == oUser).FirstOrDefault();

                ev_nutri ev_Nutri = new ev_nutri();

                ev_Nutri.peso = futbolistaViewModelNutri.peso;
                ev_Nutri.talla_estatura = futbolistaViewModelNutri.talla_estatura;
                ev_Nutri.porcent_masa_adiposa = futbolistaViewModelNutri.porcent_masa_adiposa;
                ev_Nutri.estado_uno = futbolistaViewModelNutri.estado_uno;
                ev_Nutri.kilo_masa_adiposa = futbolistaViewModelNutri.kilo_masa_adiposa;
                ev_Nutri.estado_uno_dos = futbolistaViewModelNutri.estado_uno_dos;
                ev_Nutri.porcent_musculo = futbolistaViewModelNutri.porcent_musculo;
                ev_Nutri.estado_dos = futbolistaViewModelNutri.estado_dos;
                ev_Nutri.kilo_musculo = futbolistaViewModelNutri.kilo_musculo;
                ev_Nutri.estado_dos_dos = futbolistaViewModelNutri.estado_dos_dos;
                ev_Nutri.sumatoria_pliegue = futbolistaViewModelNutri.sumatoria_pliegue;
                ev_Nutri.estado_tres = futbolistaViewModelNutri.estado_tres;
                ev_Nutri.imo = futbolistaViewModelNutri.imo;
                ev_Nutri.estado_cuatro = futbolistaViewModelNutri.estado_cuatro;
                ev_Nutri.nutricionista_id_nutri = oNutricionista.id_nutri;
                ev_Nutri.futbolista_id_futbolista = IdFutbolista;

                db.ev_nutri.Add(ev_Nutri);
                db.SaveChanges();

            }

            return Redirect(Url.Content("~/Nutri/DatosJugador/?RutFutbolista=" + futbolistaViewModelNutri.RutFutbolista));
        }

        [HttpPost]
        public ActionResult AgregarSuplemento(FutbolistaViewModelNutri futbolistaViewModelNutri)
        {
            usuario User = (usuario)Session["rut_usuario"];
            string oUser = User.rut_usuario.ToString();

            using (Entities db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelNutri.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                var oNutricionista = db.nutricionista.Where(n => n.usuario_rut_usuario == oUser).FirstOrDefault();

                suplemento suplemento = new suplemento();

                suplemento.desc_suplemento = futbolistaViewModelNutri.desc_suplemento;
                suplemento.tipo_suplemento = futbolistaViewModelNutri.tipo_suplemento;
                suplemento.cant_suplemento = futbolistaViewModelNutri.cant_suplemento;
                suplemento.fecha_suplem = futbolistaViewModelNutri.fecha_suplem;
                suplemento.comentariosuplem = futbolistaViewModelNutri.comentariosuplem;
                suplemento.nutricionista_id_nutri = oNutricionista.id_nutri;
                suplemento.estd_suplemento = "1";
                suplemento.futbolista_id_futbolista = IdFutbolista;

                db.suplemento.Add(suplemento);
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Nutri/DatosJugador/?RutFutbolista=" + futbolistaViewModelNutri.RutFutbolista));
        }
        #endregion

        #region Metodos Para Editar
        [HttpPost]
        public ActionResult IndexNutri(NutriViewModel nutriViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                string oUsuario = oUser.rut_usuario.ToString();
                var oNutri = db.usuario.Find(oUsuario);
                var oNutricionista = db.nutricionista.Where(n => n.usuario_rut_usuario == oUsuario).FirstOrDefault();

                if (nutriViewModel.PassNutricionista.Equals(nutriViewModel.ConfirmarPassNutricionista))
                {
                    oNutri.contrasenia = nutriViewModel.PassNutricionista;
                    oNutricionista.email_nutri = nutriViewModel.EmailNutricionista;
                    oNutricionista.telefono_nutri = nutriViewModel.TelefonoNutricionista;

                    db.Entry(oNutri).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oNutricionista).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return Redirect(Url.Content("~/Nutri/IndexNutri"));
        }

        [HttpPost]
        public ActionResult EditarEvaluacionNutricional(EvaluacionNutricionalViewModel evaluacionNutricionalViewModel)
        {
            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == evaluacionNutricionalViewModel.RutFutbolista).FirstOrDefault();
                var oEvalNutri = db.ev_nutri.Where(e => e.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                oEvalNutri.peso = evaluacionNutricionalViewModel.peso;
                oEvalNutri.talla_estatura = evaluacionNutricionalViewModel.talla_estatura;
                oEvalNutri.porcent_masa_adiposa = evaluacionNutricionalViewModel.porcent_masa_adiposa;
                oEvalNutri.estado_uno = evaluacionNutricionalViewModel.estado_uno;
                oEvalNutri.kilo_masa_adiposa = evaluacionNutricionalViewModel.kilo_masa_adiposa;
                oEvalNutri.estado_uno_dos = evaluacionNutricionalViewModel.estado_uno_dos;
                oEvalNutri.porcent_musculo = evaluacionNutricionalViewModel.porcent_musculo;
                oEvalNutri.estado_dos = evaluacionNutricionalViewModel.estado_dos;
                oEvalNutri.kilo_musculo = evaluacionNutricionalViewModel.kilo_musculo;
                oEvalNutri.estado_dos_dos = evaluacionNutricionalViewModel.estado_dos_dos;
                oEvalNutri.sumatoria_pliegue = evaluacionNutricionalViewModel.sumatoria_pliegue;
                oEvalNutri.estado_tres = evaluacionNutricionalViewModel.estado_tres;
                oEvalNutri.imo = evaluacionNutricionalViewModel.imo;
                oEvalNutri.estado_cuatro = evaluacionNutricionalViewModel.estado_cuatro;

                db.Entry(oEvalNutri).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Nutri/DatosJugador/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }

        [HttpPost]
        public ActionResult EditarSuplemento(SuplementoViewModel suplementoViewModel)
        {
            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == suplementoViewModel.RutFutbolista).FirstOrDefault();
                var oSuplemento = db.suplemento.Where(e => e.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                oSuplemento.desc_suplemento = suplementoViewModel.desc_suplemento;
                oSuplemento.tipo_suplemento = suplementoViewModel.tipo_suplemento;
                oSuplemento.cant_suplemento = suplementoViewModel.cant_suplemento;
                oSuplemento.fecha_suplem = suplementoViewModel.fecha_suplem;
                oSuplemento.comentariosuplem = suplementoViewModel.comentariosuplem;

                db.Entry(oSuplemento).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Nutri/DatosJugador/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }
        #endregion

        public ActionResult EliminarSuplemento(string RutFutbolista)
        {
            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oSuplemento = db.suplemento.Where(e => e.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    db.suplemento.Remove(oSuplemento);
                    db.SaveChanges();
                }
            }

            return Redirect(Url.Content("~/Nutri/DatosJugador/?RutFutbolista=" + RutFutbolista));
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

                var oNutricionista = db.nutricionista.Where(k => k.usuario_rut_usuario == oUsuario).FirstOrDefault();

                var NombreNutri = oNutricionista.nom_nutri;
                var ApPaternoNutri = oNutricionista.appaterno_nutri;
                var ApMaternoNutri = oNutricionista.apmaterno_nutri;

                return Content(NombreNutri + " " + ApPaternoNutri + " " + ApMaternoNutri);
            }
        } 
        #endregion

    }
}