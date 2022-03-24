using CyS___DeportesConcepcioin_v2.Models;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Administrador;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using LesionesViewModel = CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo.LesionesViewModel;

namespace CyS___DeportesConcepcioin_v2.Controllers
{
    public class KineController : Controller
    {
        #region Vistas del Kinesiologo
        public ActionResult IndexKine()
        {
            KineViewModel kineViewModel = new KineViewModel();

            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                if (oUser != null)
                {
                    string oUsuario = oUser.rut_usuario.ToString();
                    var oKine = db.usuario.Find(oUsuario);
                    var oKinesiologo = db.kinesiologo.Where(k => k.usuario_rut_usuario == oUsuario).FirstOrDefault();

                    kineViewModel.TelefonoKinesiologo = oKinesiologo.telefono_kine;
                    kineViewModel.EmailKinesiologo = oKinesiologo.email_kine;
                    kineViewModel.PassKinesiologo = oKine.contrasenia;
                }

            }

            return View(kineViewModel);
        }

        public ActionResult ListadoJugadores()
        {
            List<FutbolistaViewModelKine> listFutbolistas = null;

            using (Entities db = new Entities())
            {
                listFutbolistas = (from f in db.futbolista
                                   join u in db.usuario on f.usuario_rut_usuario equals u.rut_usuario
                                   where f.estado_futb == "1"
                                   orderby f.appaterno_futb
                                   select new FutbolistaViewModelKine()
                                   {
                                       RutFutbolista = u.rut_usuario,
                                       NomFutbolista = f.nom_futb,
                                       ApPaternoFutbolista = f.appaterno_futb,
                                       EmailFutbolista = f.email_futb,
                                       CategoriaFutbolista = f.categoria_id_categoria
                                   }).ToList();
            }

            return View(listFutbolistas);
        }

        [HttpGet]
        public ActionResult DatosFutbolistaSeleccionadoKine(string RutFutbolista)
        {
            var DetallesFutbolista = new DetallesFutbolistaViewModel();
            FutbolistaViewModelKine futbolistaViewModelKine = new FutbolistaViewModelKine();
            List<HistorialClinicoFutbolista> listHistClini = null;
            List<InsumoFutbolista> listInsumo = null;
            List<EvaluacionNutricionalViewModel> listEvNutri = null;
            List<SuplementoViewModel> listSuplemento = null;

            using (Entities db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutFutbolista);
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();

                if (oFutbolista != null)
                {
                    futbolistaViewModelKine.NomFutbolista = oFutbolista.nom_futb + " " + oFutbolista.appaterno_futb + " " + oFutbolista.apmaterno_futb;
                    futbolistaViewModelKine.RutFutbolista = oUsuario.rut_usuario;
                    futbolistaViewModelKine.EdadFutbolista = oFutbolista.edad_futb;
                    futbolistaViewModelKine.FechaNacimientoFutbolista = oFutbolista.fecha_nacim_futb;
                    futbolistaViewModelKine.PosicionFutbolista = oFutbolista.posicion_futb;
                    futbolistaViewModelKine.NacionalidadFutbolista = oFutbolista.nacionalidad_futb;
                    futbolistaViewModelKine.TelefonoFutbolista = oFutbolista.telefono_futb.ToString();
                    futbolistaViewModelKine.EmailFutbolista = oFutbolista.email_futb;

                    var oAntecedentesFam = db.ant_familiar.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oAntecedentesFam != null)
                    {
                        futbolistaViewModelKine.Hipertension = oAntecedentesFam.hipertension;
                        futbolistaViewModelKine.Cardiopatias = oAntecedentesFam.cardiopatia;
                        futbolistaViewModelKine.Diabetes = oAntecedentesFam.diabete;
                        futbolistaViewModelKine.Otros = oAntecedentesFam.otro;
                    }

                    var OAntecedentesPers = db.ant_personales.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (OAntecedentesPers != null)
                    {
                        futbolistaViewModelKine.EnferInfancia = OAntecedentesPers.enferme_infancia;
                        futbolistaViewModelKine.Alergias = OAntecedentesPers.alergias;
                        futbolistaViewModelKine.Operaciones = OAntecedentesPers.operaciones;
                        futbolistaViewModelKine.Traumatismos = OAntecedentesPers.traumatismos;
                        futbolistaViewModelKine.AlterVisual = OAntecedentesPers.alteracion_visual;
                        futbolistaViewModelKine.Talla = (float)OAntecedentesPers.talla;
                        futbolistaViewModelKine.Peso = (float)OAntecedentesPers.peso;
                        futbolistaViewModelKine.FC = (float)OAntecedentesPers.fc;
                        futbolistaViewModelKine.PA = (float)OAntecedentesPers.pa;
                        futbolistaViewModelKine.AlterracionPost = OAntecedentesPers.alteracion_postural;
                        futbolistaViewModelKine.GenuValgo = OAntecedentesPers.genu_valgo_varu;
                        futbolistaViewModelKine.PiePlano = OAntecedentesPers.pie_plano_valgo;
                        futbolistaViewModelKine.Otro = OAntecedentesPers.otro;
                    }

                    var oHistClinico = db.hist_clinico.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oHistClinico != null)
                    {
                        listHistClini = (from h in db.hist_clinico
                                         join le in db.lesion on h.lesion_id_lesion equals le.id_lesion
                                         where h.futbolista_id_futbolista == oHistClinico.futbolista_id_futbolista
                                         select new HistorialClinicoFutbolista()
                                         {
                                             fecha_lesion = h.fecha_lesion,
                                             lugar_lesion = h.lugar_lesion,
                                             nomb_lesion = le.nomb_lesion,
                                             relacion_lesion_anterior = h.relacion_lesion_anterior,
                                             examenes = h.examenes,
                                             fecha_baja = h.fecha_baja,
                                             dias_baja = h.dias_baja,
                                             tratamiento = h.tratamiento,
                                             RutFutbolista = RutFutbolista
                                         }).ToList();

                        DetallesFutbolista.historialClinicoFutbolistas = listHistClini;
                    }

                    var oInsumo = db.insumo.Where(f => f.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    if (oInsumo != null)
                    {
                        listInsumo =   (from i in db.insumo
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
                        listSuplemento = (from s in db.suplemento
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

                    DetallesFutbolista.FutbolistaViewModelKine = futbolistaViewModelKine;
                }
            }

            return View(DetallesFutbolista);
        }

        public ActionResult AntecedentesFamiliaresFutbolista(string RutFutbolista)
        {
            return View();
        }

        public ActionResult AntecedentesPersonalesFutbolista(string RutFutbolista)
        {
            return View();
        }

        public ActionResult AgregarHistorialClinicoFutbolista(string RutFutbolista)
        {
            List<LesionesViewModel> lstLesiones = null;

            using (var db = new Entities())
            {
                lstLesiones = (from le in db.lesion
                               orderby le.nomb_lesion ascending
                               select new LesionesViewModel
                               {
                                   IdLesion = le.id_lesion,
                                   NombLesion = le.nomb_lesion
                               }).ToList();
            }

            List<SelectListItem> lesiones = lstLesiones.ConvertAll(le =>
            {
                return new SelectListItem()
                {
                    Text = le.NombLesion.ToString(),
                    Value = le.IdLesion.ToString(),
                    Selected = false
                };
            });

            ViewBag.Lesiones = lesiones;

            return View();
        }

        public ActionResult AgregarInsumo(string RutFutbolista)
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditarAntecedentesFam(string RutFutbolista)
        {
            AntecedentesFamiliaresFutbolista antecedentesFamiliaresFutbolista = new AntecedentesFamiliaresFutbolista();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oAntecedentesFam = db.ant_familiar.Where(a => a.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    antecedentesFamiliaresFutbolista.Cardiopatias = oAntecedentesFam.cardiopatia;
                    antecedentesFamiliaresFutbolista.Hipertension = oAntecedentesFam.hipertension;
                    antecedentesFamiliaresFutbolista.Diabetes = oAntecedentesFam.diabete;
                    antecedentesFamiliaresFutbolista.Otros = oAntecedentesFam.otro;
                }
            }

            return View(antecedentesFamiliaresFutbolista);
        }

        [HttpGet]
        public ActionResult EditarAntecedentesPers(string RutFutbolista)
        {
            AntecedentePersonalFutbolista antecedentesPersonalesFutbolista = new AntecedentePersonalFutbolista();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oAntecedentesPers = db.ant_personales.Where(a => a.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    antecedentesPersonalesFutbolista.EnferInfancia = oAntecedentesPers.enferme_infancia;
                    antecedentesPersonalesFutbolista.Alergias = oAntecedentesPers.alergias;
                    antecedentesPersonalesFutbolista.Operaciones = oAntecedentesPers.operaciones;
                    antecedentesPersonalesFutbolista.Traumatismos = oAntecedentesPers.traumatismos;
                    antecedentesPersonalesFutbolista.AlterVisual = oAntecedentesPers.alteracion_visual;
                    antecedentesPersonalesFutbolista.Talla = (int)oAntecedentesPers.talla;
                    antecedentesPersonalesFutbolista.Peso = (int)oAntecedentesPers.peso;
                    antecedentesPersonalesFutbolista.FC = (int)oAntecedentesPers.fc;
                    antecedentesPersonalesFutbolista.PA = (int)oAntecedentesPers.pa;
                    antecedentesPersonalesFutbolista.AlterracionPost = oAntecedentesPers.alteracion_postural;
                    antecedentesPersonalesFutbolista.GenuValgo = oAntecedentesPers.genu_valgo_varu;
                    antecedentesPersonalesFutbolista.PiePlano = oAntecedentesPers.pie_plano_valgo;
                    antecedentesPersonalesFutbolista.Otro = oAntecedentesPers.otro;
                }
            }

            return View(antecedentesPersonalesFutbolista);
        }

        [HttpGet]
        public ActionResult EditarHistorialClinico(string RutFutbolista)
        {
            HistorialClinicoFutbolista historialClinicoFutbolista = new HistorialClinicoFutbolista();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oHistorialClin = db.hist_clinico.Where(h => h.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();
                    var oLesion = db.lesion.Where(le => le.id_lesion == oHistorialClin.lesion_id_lesion).FirstOrDefault();

                    historialClinicoFutbolista.fecha_lesion = oHistorialClin.fecha_lesion;
                    historialClinicoFutbolista.lugar_lesion = oHistorialClin.lugar_lesion;
                    historialClinicoFutbolista.nomb_lesion = oLesion.nomb_lesion;
                    historialClinicoFutbolista.relacion_lesion_anterior = oHistorialClin.relacion_lesion_anterior;
                    historialClinicoFutbolista.zona_lesion = oHistorialClin.zona_lesion;
                    historialClinicoFutbolista.examenes = oHistorialClin.examenes;
                    historialClinicoFutbolista.fecha_baja = oHistorialClin.fecha_baja;
                    historialClinicoFutbolista.dias_baja = oHistorialClin.dias_baja;
                    historialClinicoFutbolista.tratamiento = oHistorialClin.tratamiento;
                }
            }

            List<LesionesViewModel> lstLesiones = null;

            using (var db = new Entities())
            {
                lstLesiones = (from le in db.lesion
                               orderby le.nomb_lesion ascending
                               select new LesionesViewModel
                               {
                                   IdLesion = le.id_lesion,
                                   NombLesion = le.nomb_lesion
                               }).ToList();
            }

            List<SelectListItem> lesiones = lstLesiones.ConvertAll(le =>
            {
                return new SelectListItem()
                {
                    Text = le.NombLesion.ToString(),
                    Value = le.IdLesion.ToString(),
                    Selected = false
                };
            });

            ViewBag.Lesiones = lesiones;

            return View(historialClinicoFutbolista);
        }

        [HttpGet]
        public ActionResult EditarInsumo(string RutFutbolista)
        {
            InsumoFutbolista insumoFutbolista = new InsumoFutbolista();

            using (var db = new Entities())
            {
                if (RutFutbolista != null)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();
                    var oInsumo = db.insumo.Where(i => i.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                    insumoFutbolista.descripcion_insumo = oInsumo.descripcion_insumo;
                    insumoFutbolista.tipo_insumo = oInsumo.tipo_insumo;
                    insumoFutbolista.cantidad_insumo = oInsumo.cantidad_insumo;
                    insumoFutbolista.fecha_insumo = oInsumo.fecha_insumo;
                    insumoFutbolista.comentarioinsumo = oInsumo.comentarioinsumo;
                }
            }

            return View(insumoFutbolista);
        }

        public ActionResult VerEstadisticas()
        {
            List<CategoriaViewModel> lstCategorias = null;

            using (var db = new Entities())
            {
                lstCategorias = (from ca in db.categoria
                                 orderby ca.nomb_categoria ascending
                                 select new CategoriaViewModel
                                 {
                                     IdCategoria = ca.id_categoria,
                                     NomCategoria = ca.nomb_categoria
                                 }).ToList();
            }

            List<SelectListItem> categorias = lstCategorias.ConvertAll(ca =>
            {
                return new SelectListItem()
                {
                    Text = ca.NomCategoria.ToString(),
                    Value = ca.IdCategoria.ToString(),
                    Selected = false
                };
            });

            ViewBag.Categorias = categorias;

            List<LesionesViewModel> lstLesiones = null;

            using (var db = new Entities())
            {
                lstLesiones = (from le in db.lesion
                               orderby le.nomb_lesion ascending
                               select new LesionesViewModel
                               {
                                   IdLesion = le.id_lesion,
                                   NombLesion = le.nomb_lesion
                               }).ToList();
            }

            List<SelectListItem> lesiones = lstLesiones.ConvertAll(le =>
            {
                return new SelectListItem()
                {
                    Text = le.NombLesion.ToString(),
                    Value = le.IdLesion.ToString(),
                    Selected = false
                };
            });
            lesiones.Insert(0, new SelectListItem() { Value = "0", Text = "Seleccione Tipo de Lesión" });

            ViewBag.Lesiones = lesiones;

            return View();
        }

        public ActionResult Estadisticas(int tipoCategoria, DateTime FechaUno, DateTime FechaDos, int tipoLesion)
        {
            List<int> cantLesiones = new List<int>();
            List<string> nombLesiones = new List<string>();
            List<string> lstMeses = new List<string>();

            using (var db = new Entities())
            {
                if (tipoLesion == 0)
                {
                    var Tipolesiones = (from hc in db.hist_clinico
                                        join le in db.lesion on hc.lesion_id_lesion equals le.id_lesion
                                        join fu in db.futbolista on hc.futbolista_id_futbolista equals fu.id_futbolista
                                        join ca in db.categoria on fu.categoria_id_categoria equals ca.id_categoria
                                        where (hc.fecha_lesion >= FechaUno && hc.fecha_lesion <= FechaDos) && ca.id_categoria == tipoCategoria
                                        select le.nomb_lesion);

                    foreach (var item in Tipolesiones.Distinct())
                    {
                        nombLesiones.Add(item);
                        cantLesiones.Add(db.lesion.Count(le => le.nomb_lesion == item));
                    }

                    TempData["Lesiones"] = nombLesiones.ToList();
                    TempData["CantLesiones"] = cantLesiones.ToList();

                    return Content("1");
                }
                else
                {
                    var MesLesion = (from hc in db.hist_clinico
                                     join le in db.lesion on hc.lesion_id_lesion equals le.id_lesion
                                     join fu in db.futbolista on hc.futbolista_id_futbolista equals fu.id_futbolista
                                     join ca in db.categoria on fu.categoria_id_categoria equals ca.id_categoria
                                     where (hc.fecha_lesion >= FechaUno && hc.fecha_lesion <= FechaDos) && ca.id_categoria == tipoCategoria && le.id_lesion == tipoLesion
                                     group hc by new
                                     {
                                         Meses = hc.fecha_lesion.Month
                                     } 
                                     into grupo select new
                                     {
                                          Mes = grupo.Key.Meses,
                                          Total = grupo.Count(le => le.lesion_id_lesion == tipoLesion)
                                     });
                        
                    foreach (var item in MesLesion)
                    {
                        if (item.Mes == 1)
                        {
                            lstMeses.Add("Enero");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 2)
                        {
                            lstMeses.Add("Febrero");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 3)
                        {
                            lstMeses.Add("Marzo");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 4)
                        {
                            lstMeses.Add("Abril");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 5)
                        {
                            lstMeses.Add("Mayo");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 6)
                        {
                            lstMeses.Add("Junio");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 7)
                        {
                            lstMeses.Add("Julio");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 8)
                        {
                            lstMeses.Add("Agosto");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 9)
                        {
                            lstMeses.Add("Septiembre");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 10)
                        {
                            lstMeses.Add("Octubre");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 11)
                        {
                            lstMeses.Add("Noviembre");
                            cantLesiones.Add(item.Total);
                        }
                        else if (item.Mes == 12)
                        {
                            lstMeses.Add("Diciembre");
                            cantLesiones.Add(item.Total);
                        }
                    }

                    TempData["Meses"] = lstMeses.ToList();
                    TempData["Lesion"] = tipoLesion;
                    TempData["CantLesiones"] = cantLesiones.ToList();

                    return Content("2");
                }
            }
        }

        public ActionResult Grafico()
        {
            return View();
        }

        public ActionResult Grafico2()
        {
            return View();
        }
        #endregion

        #region Metodos para Agregar
        [HttpPost]
        public ActionResult AntecedentesFamiliaresFutbolista(FutbolistaViewModelKine futbolistaViewModelKine)
        {
            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelKine.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                ant_familiar ant_Familiar = new ant_familiar();

                ant_Familiar.cardiopatia = futbolistaViewModelKine.Cardiopatias;
                ant_Familiar.diabete = futbolistaViewModelKine.Diabetes;
                ant_Familiar.hipertension = futbolistaViewModelKine.Hipertension;
                ant_Familiar.otro = futbolistaViewModelKine.Otros;
                ant_Familiar.futbolista_id_futbolista = IdFutbolista;

                db.ant_familiar.Add(ant_Familiar);

                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + futbolistaViewModelKine.RutFutbolista));
        }

        [HttpPost]
        public ActionResult AntecedentesPersonalesFutbolista(FutbolistaViewModelKine futbolistaViewModelKine)
        {
            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelKine.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                ant_personales ant_Personales = new ant_personales();

                ant_Personales.enferme_infancia = futbolistaViewModelKine.EnferInfancia;
                ant_Personales.alergias = futbolistaViewModelKine.Alergias;
                ant_Personales.operaciones = futbolistaViewModelKine.Operaciones;
                ant_Personales.traumatismos = futbolistaViewModelKine.Traumatismos;
                ant_Personales.alteracion_visual = futbolistaViewModelKine.AlterVisual;
                ant_Personales.talla = (int?)futbolistaViewModelKine.Talla;
                ant_Personales.peso = (int?)futbolistaViewModelKine.Peso;
                ant_Personales.fc = (int?)futbolistaViewModelKine.FC;
                ant_Personales.pa = (int?)futbolistaViewModelKine.PA;
                ant_Personales.alteracion_postural = futbolistaViewModelKine.AlterracionPost;
                ant_Personales.genu_valgo_varu = futbolistaViewModelKine.GenuValgo;
                ant_Personales.pie_plano_valgo = futbolistaViewModelKine.PiePlano;
                ant_Personales.otro = futbolistaViewModelKine.Otro;
                ant_Personales.futbolista_id_futbolista = IdFutbolista;

                db.ant_personales.Add(ant_Personales);

                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + futbolistaViewModelKine.RutFutbolista));
        }

        [HttpPost]
        public ActionResult AgregarHistorialClinicoFutbolista(FutbolistaViewModelKine futbolistaViewModelKine)
        {
            using (Entities db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelKine.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                hist_clinico hist_Clinico = new hist_clinico();

                hist_Clinico.fecha_lesion = futbolistaViewModelKine.fecha_lesion;
                hist_Clinico.lugar_lesion = futbolistaViewModelKine.lugar_lesion;
                hist_Clinico.lesion_id_lesion = futbolistaViewModelKine.id_lesion;
                hist_Clinico.zona_lesion = futbolistaViewModelKine.zona_lesion;
                hist_Clinico.relacion_lesion_anterior = futbolistaViewModelKine.relacion_lesion_anterior;
                hist_Clinico.examenes = futbolistaViewModelKine.examenes;
                hist_Clinico.fecha_baja = futbolistaViewModelKine.fecha_baja;
                hist_Clinico.dias_baja = futbolistaViewModelKine.dias_baja;
                hist_Clinico.tratamiento = futbolistaViewModelKine.tratamiento;
                hist_Clinico.futbolista_id_futbolista = IdFutbolista;

                db.hist_clinico.Add(hist_Clinico);
                db.SaveChanges();

            }
            return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + futbolistaViewModelKine.RutFutbolista));
        }

        [HttpPost]
        public ActionResult AgregarInsumo(FutbolistaViewModelKine futbolistaViewModelKine)
        {
            using (Entities db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModelKine.RutFutbolista).FirstOrDefault();
                var IdFutbolista = oFutbolista.id_futbolista;

                usuario User = (usuario)Session["rut_usuario"];

                var oKinesiologo = db.kinesiologo.Where(f => f.usuario_rut_usuario == User.rut_usuario).FirstOrDefault();

                insumo insumo = new insumo();

                insumo.descripcion_insumo = futbolistaViewModelKine.descripcion_insumo;
                insumo.tipo_insumo = futbolistaViewModelKine.tipo_insumo;
                insumo.cantidad_insumo = futbolistaViewModelKine.cantidad_insumo;
                insumo.fecha_insumo = futbolistaViewModelKine.fecha_insumo;
                insumo.comentarioinsumo = futbolistaViewModelKine.comentarioinsumo;
                insumo.futbolista_id_futbolista = IdFutbolista;
                insumo.estado_insumo = "1";
                insumo.kinesiologo_id_kinesiologo = oKinesiologo.id_kinesiologo;

                db.insumo.Add(insumo);
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + futbolistaViewModelKine.RutFutbolista));
        }
        #endregion

        #region Metodos para Editar
        [HttpPost]
        public ActionResult IndexKine(KineViewModel kineViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUser = (usuario)Session["rut_usuario"];
                string oUsuario = oUser.rut_usuario.ToString();
                var oKine = db.usuario.Find(oUsuario);
                var oKinesiologo = db.kinesiologo.Where(k => k.usuario_rut_usuario == oUsuario).FirstOrDefault();

                if (kineViewModel.PassKinesiologo.Equals(kineViewModel.ConfirmarPassKinesiologo))
                {
                    oKine.contrasenia = kineViewModel.PassKinesiologo;
                    oKinesiologo.email_kine = kineViewModel.EmailKinesiologo;
                    oKinesiologo.telefono_kine = kineViewModel.TelefonoKinesiologo;

                    db.Entry(oKine).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oKinesiologo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return Redirect(Url.Content("~/Kine/IndexKine"));
        }

        [HttpPost]
        public ActionResult EditarAntecedentesFam(AntecedentesFamiliaresFutbolista antecedentesFamiliaresFutbolista)
        {
            if (!ModelState.IsValid)
            {
                return View("ListadoJugadores");
            }

            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == antecedentesFamiliaresFutbolista.RutFutbolista).FirstOrDefault();
                var oAntecedentesFam = db.ant_familiar.Where(a => a.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                oAntecedentesFam.cardiopatia = antecedentesFamiliaresFutbolista.Cardiopatias;
                oAntecedentesFam.diabete = antecedentesFamiliaresFutbolista.Diabetes;
                oAntecedentesFam.hipertension = antecedentesFamiliaresFutbolista.Hipertension;
                oAntecedentesFam.otro = antecedentesFamiliaresFutbolista.Otros;

                db.Entry(oAntecedentesFam).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }

        [HttpPost]
        public ActionResult EditarAntecedentesPers(AntecedentePersonalFutbolista antecedentePersonalFutbolista)
        {
            if (!ModelState.IsValid)
            {
                return View("ListadoJugadores");
            }

            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == antecedentePersonalFutbolista.RutFutbolista).FirstOrDefault();
                var oAntecedentesPers = db.ant_personales.Where(a => a.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                oAntecedentesPers.enferme_infancia = antecedentePersonalFutbolista.EnferInfancia;
                oAntecedentesPers.alergias = antecedentePersonalFutbolista.Alergias;
                oAntecedentesPers.operaciones = antecedentePersonalFutbolista.Operaciones;
                oAntecedentesPers.traumatismos = antecedentePersonalFutbolista.Traumatismos;
                oAntecedentesPers.alteracion_visual = antecedentePersonalFutbolista.AlterVisual;
                oAntecedentesPers.talla = (int?)antecedentePersonalFutbolista.Talla;
                oAntecedentesPers.peso = (int?)antecedentePersonalFutbolista.Peso;
                oAntecedentesPers.fc = (int?)antecedentePersonalFutbolista.FC;
                oAntecedentesPers.pa = (int?)antecedentePersonalFutbolista.PA;
                oAntecedentesPers.alteracion_postural = antecedentePersonalFutbolista.AlterracionPost;
                oAntecedentesPers.genu_valgo_varu = antecedentePersonalFutbolista.GenuValgo;
                oAntecedentesPers.pie_plano_valgo = antecedentePersonalFutbolista.PiePlano;
                oAntecedentesPers.otro = antecedentePersonalFutbolista.Otro;

                db.Entry(oAntecedentesPers).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }

        [HttpPost]
        public ActionResult EditarHistorialClinico(HistorialClinicoFutbolista historialClinicoFutbolista)
        {
            if (!ModelState.IsValid)
            {
                return View("ListadoJugadores");
            }

            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == historialClinicoFutbolista.RutFutbolista).FirstOrDefault();
                var oHistorialClinico = db.hist_clinico.Where(a => a.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();
                var oLesion = db.lesion.Where(le => le.id_lesion == oHistorialClinico.lesion_id_lesion).FirstOrDefault();

                oHistorialClinico.fecha_lesion = historialClinicoFutbolista.fecha_lesion;
                oHistorialClinico.lugar_lesion = historialClinicoFutbolista.lugar_lesion;
                oHistorialClinico.lesion_id_lesion = historialClinicoFutbolista.id_lesion;
                oHistorialClinico.zona_lesion = historialClinicoFutbolista.zona_lesion;
                oHistorialClinico.relacion_lesion_anterior = historialClinicoFutbolista.relacion_lesion_anterior;
                oHistorialClinico.examenes = historialClinicoFutbolista.examenes;
                oHistorialClinico.fecha_baja = historialClinicoFutbolista.fecha_baja;
                oHistorialClinico.dias_baja = historialClinicoFutbolista.dias_baja;
                oHistorialClinico.tratamiento = historialClinicoFutbolista.tratamiento;

                db.Entry(oHistorialClinico).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }

        [HttpPost]
        public ActionResult EditarInsumo(InsumoFutbolista insumoFutbolista)
        {
            if (!ModelState.IsValid)
            {
                return View("ListadoJugadores");
            }

            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == insumoFutbolista.RutFutbolista).FirstOrDefault();
                var oInsumo = db.insumo.Where(i => i.futbolista_id_futbolista == oFutbolista.id_futbolista).FirstOrDefault();

                oInsumo.descripcion_insumo = insumoFutbolista.descripcion_insumo;
                oInsumo.tipo_insumo = insumoFutbolista.tipo_insumo;
                oInsumo.cantidad_insumo = insumoFutbolista.cantidad_insumo;
                oInsumo.fecha_insumo = insumoFutbolista.fecha_insumo;
                oInsumo.comentarioinsumo = insumoFutbolista.comentarioinsumo;

                db.Entry(oInsumo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Kine/DatosFutbolistaSeleccionadoKine/?RutFutbolista=" + oFutbolista.usuario_rut_usuario));
            }
        }
        #endregion

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

                var oKinesiologo = db.kinesiologo.Where(k => k.usuario_rut_usuario == oUsuario).FirstOrDefault();

                var NombreKine = oKinesiologo.nom_kinesiologo;
                var ApPaternoKine = oKinesiologo.appaterno_kine;
                var ApMaternoKine = oKinesiologo.apmaterno_kine;

                return Content(NombreKine + " " + ApPaternoKine + " " + ApMaternoKine);
            }
        }
        #endregion
    }
}