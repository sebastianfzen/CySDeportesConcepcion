using CyS___DeportesConcepcioin_v2.Models;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels;
using CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Administrador;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace CyS___DeportesConcepcioin_v2.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult IndexAdmin()
        {
            return View();
        }

        #region Vistas del Administrador
        public ActionResult ListarUsuariosView()
        {
            List<KinesiologoViewModel> listKinesiologos = null;
            List<KinesiologoViewModel> listKinesiologosEliminados = null;
            List<NutricionistaViewModel> listNutricionistas = null;
            List<NutricionistaViewModel> listNutricionistasEliminados = null;
            List<FutbolistaViewModel> listFutbolistas = null;
            List<FutbolistaViewModel> listFutbolistasEliminados = null;
            List<CTecnicoViewModel> listCTecnico = null;
            List<CTecnicoViewModel> listCTecnicoEliminados = null;
            dynamic modelos = new ExpandoObject();

            using (Entities db = new Entities())
            {
                listKinesiologos = (from k in db.kinesiologo
                                    join u in db.usuario on k.usuario_rut_usuario equals u.rut_usuario 
                                    where k.estado_kinesiologo == "1"
                                    orderby k.appaterno_kine
                                    select new KinesiologoViewModel()
                                    {
                                        RutKinesiologo = u.rut_usuario,
                                        NomKinesiologo = k.nom_kinesiologo,
                                        ApPaternoKinesiologo = k.appaterno_kine,
                                        EmailKinesiologo = k.email_kine
                                    }).ToList();

                listKinesiologosEliminados = (from k in db.kinesiologo
                                            join u in db.usuario on k.usuario_rut_usuario equals u.rut_usuario
                                            where k.estado_kinesiologo == "0"
                                            orderby k.appaterno_kine
                                            select new KinesiologoViewModel()
                                            {
                                                RutKinesiologo = u.rut_usuario,
                                                NomKinesiologo = k.nom_kinesiologo,
                                                ApPaternoKinesiologo = k.appaterno_kine,
                                                EmailKinesiologo = k.email_kine
                                            }).ToList();

                listNutricionistas = (from n in db.nutricionista
                                      join u in db.usuario on n.usuario_rut_usuario equals u.rut_usuario 
                                      where n.estado_nutricionista == "1"
                                      orderby n.appaterno_nutri
                                      select new NutricionistaViewModel()
                                      {
                                          RutNutricionista = u.rut_usuario,
                                          NomNutricionista = n.nom_nutri,
                                          ApPaternoNutricionista = n.appaterno_nutri,
                                          EmailNutricionista = n.email_nutri
                                      }).ToList();

                listNutricionistasEliminados = (from n in db.nutricionista
                                              join u in db.usuario on n.usuario_rut_usuario equals u.rut_usuario
                                              where n.estado_nutricionista == "0"
                                              orderby n.appaterno_nutri
                                              select new NutricionistaViewModel()
                                              {
                                                  RutNutricionista = u.rut_usuario,
                                                  NomNutricionista = n.nom_nutri,
                                                  ApPaternoNutricionista = n.appaterno_nutri,
                                                  EmailNutricionista = n.email_nutri
                                              }).ToList();

                listCTecnico = (from ct in db.cuerpo_tec
                                join u in db.usuario on ct.usuario_rut_usuario equals u.rut_usuario
                                where ct.estado_ct == "1"
                                orderby ct.appaterno_ct
                                select new CTecnicoViewModel()
                                {
                                    RutCTecnico = u.rut_usuario,
                                    NomCTecnico = ct.nom_ct,
                                    ApPaternoCTecnico = ct.appaterno_ct,
                                    EmailCTecnico = ct.email_ct
                                }).ToList();

                listCTecnicoEliminados = (from ct in db.cuerpo_tec
                                        join u in db.usuario on ct.usuario_rut_usuario equals u.rut_usuario
                                        where ct.estado_ct == "0"
                                        orderby ct.appaterno_ct
                                        select new CTecnicoViewModel()
                                        {
                                            RutCTecnico = u.rut_usuario,
                                            NomCTecnico = ct.nom_ct,
                                            ApPaternoCTecnico = ct.appaterno_ct,
                                            EmailCTecnico = ct.email_ct
                                        }).ToList();

                listFutbolistas = (from f in db.futbolista
                                   join u in db.usuario on f.usuario_rut_usuario equals u.rut_usuario
                                   where f.estado_futb == "1"
                                   orderby f.appaterno_futb
                                   select new FutbolistaViewModel()
                                   {
                                       RutFutbolista = u.rut_usuario,
                                       NomFutbolista = f.nom_futb,
                                       ApPaternoFutbolista = f.appaterno_futb,
                                       EmailFutbolista = f.email_futb
                                   }).ToList();

                listFutbolistasEliminados = (from f in db.futbolista
                                           join u in db.usuario on f.usuario_rut_usuario equals u.rut_usuario
                                           where f.estado_futb == "0"
                                           orderby f.appaterno_futb
                                           select new FutbolistaViewModel()
                                           {
                                               RutFutbolista = u.rut_usuario,
                                               NomFutbolista = f.nom_futb,
                                               ApPaternoFutbolista = f.appaterno_futb,
                                               EmailFutbolista = f.email_futb
                                           }).ToList();

                modelos.Kinesiologos = listKinesiologos;
                modelos.Nutricionistas = listNutricionistas;
                modelos.CTecnicos = listCTecnico;
                modelos.Futbolista = listFutbolistas;

                modelos.KinesiologosEliminados = listKinesiologosEliminados;
                modelos.NutricionistasEliminados = listNutricionistasEliminados;
                modelos.CTecnicosEliminados = listCTecnicoEliminados;
                modelos.FutbolistaEliminados = listFutbolistasEliminados;

            }
            return View(modelos);
        }

        public ActionResult AgregarProfesionalView()
        {
            return View();
        }

        public ActionResult AgregarFutbolistaView()
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

            return View();
        }

        [HttpGet]
        public ActionResult DatosFutbolistaSeleccionadoView(string RutFutbolista)
        {
            FutbolistaViewModel futbolistaViewModel = new FutbolistaViewModel();

            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutFutbolista);
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();

                if (oUsuario != null)
                {
                    futbolistaViewModel.RutFutbolista = oUsuario.rut_usuario;
                    futbolistaViewModel.NomFutbolista = oFutbolista.nom_futb;
                    futbolistaViewModel.ApPaternoFutbolista = oFutbolista.appaterno_futb;
                    futbolistaViewModel.ApMaternoFutbolista = oFutbolista.apmaterno_futb;
                    futbolistaViewModel.EdadFutbolista = oFutbolista.edad_futb;
                    futbolistaViewModel.FechaNacimientoFutbolista = oFutbolista.fecha_nacim_futb;
                    futbolistaViewModel.NacionalidadFutbolista = oFutbolista.nacionalidad_futb;
                    futbolistaViewModel.PosicionFutbolista = oFutbolista.posicion_futb;
                    futbolistaViewModel.EmailFutbolista = oFutbolista.email_futb;
                    futbolistaViewModel.CalzadoFutbolista = oFutbolista.calzado_futb;
                    futbolistaViewModel.TelefonoFutbolista = oFutbolista.telefono_futb;
                    futbolistaViewModel.ClubFutbolista = oFutbolista.club_futb;
                    futbolistaViewModel.CategoriaFutbolista = oFutbolista.categoria_id_categoria;
                    futbolistaViewModel.TemporadaFutbolista = oFutbolista.temporada_futb;
                    futbolistaViewModel.AnioFutbolista = oFutbolista.anio_futb;
                }

            }

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

            return View(futbolistaViewModel);
        }

        [HttpGet]
        public ActionResult DatosProfesionalSeleccionadoView(string RutProfesional)
        {
            ProfesionalViewModel profesionalViewModel = new ProfesionalViewModel();

            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutProfesional);

                if (oUsuario != null)
                {
                    if (oUsuario.tipo_usuario == 1)
                    {
                        var oKinesiologo = db.kinesiologo.Where(k => k.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                        profesionalViewModel.RutProfesional = oUsuario.rut_usuario;
                        profesionalViewModel.NomProfesional = oKinesiologo.nom_kinesiologo;
                        profesionalViewModel.ApPaternoProfesional = oKinesiologo.appaterno_kine;
                        profesionalViewModel.ApMaternoProfesional = oKinesiologo.apmaterno_kine;
                        profesionalViewModel.EmailProfesional = oKinesiologo.email_kine;
                        profesionalViewModel.TelefonoProfesional = oKinesiologo.telefono_kine;
                        profesionalViewModel.TipoProfesional = oUsuario.tipo_usuario;

                        return View(profesionalViewModel);
                    }
                    else if (oUsuario.tipo_usuario == 2)
                    {
                        var oNutricionista = db.nutricionista.Where(n => n.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                        profesionalViewModel.RutProfesional = oUsuario.rut_usuario;
                        profesionalViewModel.NomProfesional = oNutricionista.nom_nutri;
                        profesionalViewModel.ApPaternoProfesional = oNutricionista.appaterno_nutri;
                        profesionalViewModel.ApMaternoProfesional = oNutricionista.apmaterno_nutri;
                        profesionalViewModel.EmailProfesional = oNutricionista.email_nutri;
                        profesionalViewModel.TelefonoProfesional = oNutricionista.telefono_nutri;
                        profesionalViewModel.TipoProfesional = oUsuario.tipo_usuario;

                        return View(profesionalViewModel);
                    }
                    else if (oUsuario.tipo_usuario == 3)
                    {
                        var oCTecnico = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                        profesionalViewModel.RutProfesional = oUsuario.rut_usuario;
                        profesionalViewModel.NomProfesional = oCTecnico.nom_ct;
                        profesionalViewModel.ApPaternoProfesional = oCTecnico.appaterno_ct;
                        profesionalViewModel.ApMaternoProfesional = oCTecnico.apmaterno_ct;
                        profesionalViewModel.EmailProfesional = oCTecnico.email_ct;
                        profesionalViewModel.TelefonoProfesional = oCTecnico.telefono_ct;
                        profesionalViewModel.TipoProfesional = oUsuario.tipo_usuario;

                        return View(profesionalViewModel);
                    }
                }
            }

            return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
        }

        public ActionResult ListarItems()
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

        public ActionResult AgregarTipoLesion()
        {
            return View();
        }

        public ActionResult AgregarTipoEntrenamiento()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditarTipoLesion(int IdLesion)
        {
            LesionesViewModel lesionesViewModel = new LesionesViewModel();

            using (var db = new Entities())
            {
                var oLesion = db.lesion.Where(l => l.id_lesion == IdLesion).FirstOrDefault();

                if (oLesion != null)
                {
                    lesionesViewModel.IdLesion = oLesion.id_lesion;
                    lesionesViewModel.NombLesion = oLesion.nomb_lesion;
                }
            }

            return View(lesionesViewModel);

        }

        [HttpGet]
        public ActionResult EditarTipoEntrenamiento(int IdEntrenamiento)
        {
            TipoEntrenamientoViewModel tipoEntrenamientoViewModel = new TipoEntrenamientoViewModel();

            using (var db = new Entities())
            {
                var oEntrenamiento = db.tipo_entrenam.Where(e => e.id_tipo_entr == IdEntrenamiento).FirstOrDefault();

                if (oEntrenamiento != null)
                {
                    tipoEntrenamientoViewModel.IdTipoEntrenamiento = oEntrenamiento.id_tipo_entr;
                    tipoEntrenamientoViewModel.NombEntrenamiento = oEntrenamiento.nomb_entrenamiento;
                }
            }

            return View(tipoEntrenamientoViewModel);
        }
        #endregion

        #region Metodos para Agregar Usuarios
        [HttpPost]
        public ActionResult AgregarProfesional(ProfesionalViewModel profesionalViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUsuario = new usuario();
                kinesiologo oKinesiologo = new kinesiologo();
                nutricionista oNutricionista = new nutricionista();
                cuerpo_tec oCTecnico = new cuerpo_tec();

                if (profesionalViewModel.TipoProfesional == 1 && profesionalViewModel.PassProfesional.Equals(profesionalViewModel.ConfirmarPassProfesional))
                {
                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = profesionalViewModel.TipoProfesional;
                    oUsuario.contrasenia = profesionalViewModel.PassProfesional;

                    db.usuario.Add(oUsuario);

                    oKinesiologo.nom_kinesiologo = profesionalViewModel.NomProfesional;
                    oKinesiologo.appaterno_kine = profesionalViewModel.ApPaternoProfesional;
                    oKinesiologo.apmaterno_kine = profesionalViewModel.ApMaternoProfesional;
                    oKinesiologo.email_kine = profesionalViewModel.EmailProfesional;
                    oKinesiologo.telefono_kine = profesionalViewModel.TelefonoProfesional;
                    oKinesiologo.estado_kinesiologo = "1";
                    oKinesiologo.usuario_rut_usuario = profesionalViewModel.RutProfesional;

                    db.kinesiologo.Add(oKinesiologo);

                    db.SaveChanges();
                }
                else if (profesionalViewModel.TipoProfesional == 2 && profesionalViewModel.PassProfesional.Equals(profesionalViewModel.ConfirmarPassProfesional))
                {
                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = profesionalViewModel.TipoProfesional;
                    oUsuario.contrasenia = profesionalViewModel.PassProfesional;

                    db.usuario.Add(oUsuario);

                    oNutricionista.nom_nutri = profesionalViewModel.NomProfesional;
                    oNutricionista.appaterno_nutri = profesionalViewModel.ApPaternoProfesional;
                    oNutricionista.apmaterno_nutri = profesionalViewModel.ApMaternoProfesional;
                    oNutricionista.email_nutri = profesionalViewModel.EmailProfesional;
                    oNutricionista.telefono_nutri = profesionalViewModel.TelefonoProfesional;
                    oNutricionista.estado_nutricionista = "1";
                    oNutricionista.usuario_rut_usuario = profesionalViewModel.RutProfesional;

                    db.nutricionista.Add(oNutricionista);

                    db.SaveChanges();
                }
                else if (profesionalViewModel.TipoProfesional == 3 && profesionalViewModel.PassProfesional.Equals(profesionalViewModel.ConfirmarPassProfesional))
                {
                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = profesionalViewModel.TipoProfesional;
                    oUsuario.contrasenia = profesionalViewModel.PassProfesional;

                    db.usuario.Add(oUsuario);

                    oCTecnico.nom_ct = profesionalViewModel.NomProfesional;
                    oCTecnico.appaterno_ct = profesionalViewModel.ApPaternoProfesional;
                    oCTecnico.apmaterno_ct = profesionalViewModel.ApMaternoProfesional;
                    oCTecnico.email_ct = profesionalViewModel.EmailProfesional;
                    oCTecnico.telefono_ct = profesionalViewModel.TelefonoProfesional;
                    oCTecnico.estado_ct = "1";
                    oCTecnico.usuario_rut_usuario = profesionalViewModel.RutProfesional;

                    db.cuerpo_tec.Add(oCTecnico);

                    db.SaveChanges();
                }

                return Redirect(Url.Content("~/Admin/ListarUsuariosView"));

            }

        }

        [HttpPost]
        public ActionResult AgregarFutbolista(FutbolistaViewModel futbolistaViewModel, CategoriaViewModel categoriaViewModel)
        {
            using (var db = new Entities())
            {
                usuario oUsuario = new usuario();
                futbolista oFutbolista = new futbolista();

                if (futbolistaViewModel.PassFutbolista.Equals(futbolistaViewModel.ConfirmarPassFutbolista))
                {
                    oUsuario.rut_usuario = futbolistaViewModel.RutFutbolista;
                    oUsuario.tipo_usuario = 4;
                    oUsuario.contrasenia = futbolistaViewModel.PassFutbolista;

                    db.usuario.Add(oUsuario);

                    oFutbolista.nom_futb = futbolistaViewModel.NomFutbolista;
                    oFutbolista.appaterno_futb = futbolistaViewModel.ApPaternoFutbolista;
                    oFutbolista.apmaterno_futb = futbolistaViewModel.ApMaternoFutbolista;
                    oFutbolista.edad_futb = futbolistaViewModel.EdadFutbolista;
                    oFutbolista.fecha_nacim_futb = futbolistaViewModel.FechaNacimientoFutbolista;
                    oFutbolista.nacionalidad_futb = futbolistaViewModel.NacionalidadFutbolista;
                    oFutbolista.posicion_futb = futbolistaViewModel.PosicionFutbolista;
                    oFutbolista.email_futb = futbolistaViewModel.EmailFutbolista;
                    oFutbolista.calzado_futb = futbolistaViewModel.CalzadoFutbolista;
                    oFutbolista.telefono_futb = futbolistaViewModel.TelefonoFutbolista;
                    oFutbolista.club_futb = futbolistaViewModel.ClubFutbolista;
                    oFutbolista.temporada_futb = futbolistaViewModel.TemporadaFutbolista;
                    oFutbolista.anio_futb = futbolistaViewModel.AnioFutbolista;
                    oFutbolista.categoria_id_categoria = futbolistaViewModel.CategoriaFutbolista;
                    oFutbolista.estado_futb = "1";
                    oFutbolista.usuario_rut_usuario = futbolistaViewModel.RutFutbolista;

                    db.futbolista.Add(oFutbolista);

                    db.SaveChanges();
                }

                return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
            }
        }

        [HttpPost]
        public ActionResult AgregarTipoLesion(LesionesViewModel lesionesViewModel)
        {
            using (var db = new Entities())
            {
                lesion oLesion = new lesion();

                oLesion.nomb_lesion = lesionesViewModel.NombLesion;

                db.lesion.Add(oLesion);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Admin/ListarItems"));
        }

        [HttpPost]
        public ActionResult AgregarTipoEntrenamiento(TipoEntrenamientoViewModel tipoEntrenamientoViewModel)
        {
            using (var db = new Entities())
            {
                tipo_entrenam oEntrenamiento = new tipo_entrenam();

                oEntrenamiento.nomb_entrenamiento = tipoEntrenamientoViewModel.NombEntrenamiento;

                db.tipo_entrenam.Add(oEntrenamiento);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Admin/ListarItems"));
        }
        #endregion

        #region Metodos para Editar Datos
        public ActionResult EditarPassAdmin(string PassAdmin, string confirmarPassAdmin)
        {
            EditUserViewModel euser = new EditUserViewModel();

            if (PassAdmin.Equals(confirmarPassAdmin))
            {
                using (var db = new Entities())
                {
                    usuario User = (usuario)Session["rut_usuario"];
                    string oUser = User.rut_usuario.ToString();

                    var query = (from us in db.usuario where us.rut_usuario == oUser select us).ToList();

                    foreach (var data in query)
                    {
                        data.contrasenia = PassAdmin;
                    }

                    db.SaveChanges();

                    return Content("ContraseñaActualizada");
                }
            }
            else
            {
                return Content("Contraseñas no coinciden");
            }
        }

        [HttpPost]
        public ActionResult DatosFutbolistaSeleccionadoView(FutbolistaViewModel futbolistaViewModel)
        {
            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(futbolistaViewModel.RutFutbolista);
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == futbolistaViewModel.RutFutbolista).FirstOrDefault();

                oUsuario.rut_usuario = futbolistaViewModel.RutFutbolista;
                oFutbolista.nom_futb = futbolistaViewModel.NomFutbolista;
                oFutbolista.appaterno_futb = futbolistaViewModel.ApPaternoFutbolista;
                oFutbolista.apmaterno_futb = futbolistaViewModel.ApMaternoFutbolista;
                oFutbolista.edad_futb = futbolistaViewModel.EdadFutbolista;
                oFutbolista.fecha_nacim_futb = futbolistaViewModel.FechaNacimientoFutbolista;
                oFutbolista.nacionalidad_futb = futbolistaViewModel.NacionalidadFutbolista;
                oFutbolista.posicion_futb = futbolistaViewModel.PosicionFutbolista;
                oFutbolista.email_futb = futbolistaViewModel.EmailFutbolista;
                oFutbolista.calzado_futb = futbolistaViewModel.CalzadoFutbolista;
                oFutbolista.telefono_futb = futbolistaViewModel.TelefonoFutbolista;
                oFutbolista.club_futb = futbolistaViewModel.ClubFutbolista;
                oFutbolista.temporada_futb = futbolistaViewModel.TemporadaFutbolista;
                oFutbolista.anio_futb = futbolistaViewModel.AnioFutbolista;
                oFutbolista.categoria_id_categoria = futbolistaViewModel.CategoriaFutbolista;
                oFutbolista.usuario_rut_usuario = futbolistaViewModel.RutFutbolista;

                db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                db.Entry(oFutbolista).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
            }
        }

        [HttpPost]
        public ActionResult DatosProfesionalSeleccionadoView(ProfesionalViewModel profesionalViewModel)
        {
            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(profesionalViewModel.RutProfesional);

                if (oUsuario.tipo_usuario == 1)
                {
                    var oProfesional = db.kinesiologo.Where(k => k.usuario_rut_usuario == profesionalViewModel.RutProfesional).FirstOrDefault();

                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = 1;
                    oProfesional.nom_kinesiologo = profesionalViewModel.NomProfesional;
                    oProfesional.appaterno_kine = profesionalViewModel.ApPaternoProfesional;
                    oProfesional.apmaterno_kine = profesionalViewModel.ApMaternoProfesional;
                    oProfesional.usuario_rut_usuario = profesionalViewModel.RutProfesional;
                    oProfesional.email_kine = profesionalViewModel.EmailProfesional;
                    oProfesional.telefono_kine = profesionalViewModel.TelefonoProfesional;
                    oUsuario.tipo_usuario = 1;

                    db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));

                }
                else if (oUsuario.tipo_usuario == 2)
                {
                    var oProfesional = db.nutricionista.Where(n => n.usuario_rut_usuario == profesionalViewModel.RutProfesional).FirstOrDefault();

                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = 2;
                    oProfesional.nom_nutri = profesionalViewModel.NomProfesional;
                    oProfesional.appaterno_nutri = profesionalViewModel.ApPaternoProfesional;
                    oProfesional.apmaterno_nutri = profesionalViewModel.ApMaternoProfesional;
                    oProfesional.usuario_rut_usuario = profesionalViewModel.RutProfesional;
                    oProfesional.email_nutri = profesionalViewModel.EmailProfesional;
                    oProfesional.telefono_nutri = profesionalViewModel.TelefonoProfesional;
                    oUsuario.tipo_usuario = 2;

                    db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
                }
                else if (oUsuario.tipo_usuario == 3)
                {
                    var oProfesional = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == profesionalViewModel.RutProfesional).FirstOrDefault();

                    oUsuario.rut_usuario = profesionalViewModel.RutProfesional;
                    oUsuario.tipo_usuario = 3;
                    oProfesional.nom_ct = profesionalViewModel.NomProfesional;
                    oProfesional.appaterno_ct = profesionalViewModel.ApPaternoProfesional;
                    oProfesional.apmaterno_ct = profesionalViewModel.ApMaternoProfesional;
                    oProfesional.usuario_rut_usuario = profesionalViewModel.RutProfesional;
                    oProfesional.email_ct = profesionalViewModel.EmailProfesional;
                    oProfesional.telefono_ct = profesionalViewModel.TelefonoProfesional;
                    oUsuario.tipo_usuario = 3;

                    db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
                }

                return Redirect(Url.Content("~/Admin/ListarUsuariosView"));

            }
        }

        public ActionResult HabilitarUsuarioSeleccionadoView(string RutUsuario)
        {
            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutUsuario);

                if (oUsuario.tipo_usuario == 1)
                {
                    var oProfesional = db.kinesiologo.Where(k => k.usuario_rut_usuario == RutUsuario).FirstOrDefault();

                    oProfesional.estado_kinesiologo = "1";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));

                }
                else if (oUsuario.tipo_usuario == 2)
                {
                    var oProfesional = db.nutricionista.Where(n => n.usuario_rut_usuario == RutUsuario).FirstOrDefault();

                    oProfesional.estado_nutricionista = "1";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
                }
                else if (oUsuario.tipo_usuario == 3)
                {
                    var oProfesional = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == RutUsuario).FirstOrDefault();

                    oProfesional.estado_ct = "1";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
                }
                else if (oUsuario.tipo_usuario == 4)
                {
                    var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutUsuario).FirstOrDefault();

                    oFutbolista.estado_futb = "1";
                    db.Entry(oFutbolista).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
                }

                return Redirect(Url.Content("~/Admin/ListarUsuariosView"));
            }
        }

        [HttpPost]
        public ActionResult EditarTipoLesion(LesionesViewModel lesionesViewModel) { 

            using (var db = new Entities())
            {
                var oLesion = db.lesion.Where(l => l.id_lesion == lesionesViewModel.IdLesion).FirstOrDefault();

                if (oLesion != null)
                {
                    oLesion.nomb_lesion = lesionesViewModel.NombLesion;
                    db.Entry(oLesion).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Redirect(Url.Content("~/Admin/ListarItems"));

        }

        [HttpPost]
        public ActionResult EditarTipoEntrenamiento(TipoEntrenamientoViewModel tipoEntrenamientoViewModel) 
        { 
            using (var db = new Entities())
            {
                var oEntrenamiento = db.tipo_entrenam.Where(e => e.id_tipo_entr == tipoEntrenamientoViewModel.IdTipoEntrenamiento).FirstOrDefault();

                if (oEntrenamiento != null)
                {
                    oEntrenamiento.nomb_entrenamiento = tipoEntrenamientoViewModel.NombEntrenamiento;
                    db.Entry(oEntrenamiento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Redirect(Url.Content("~/Admin/ListarItems"));
        }
        #endregion

        #region Metodo para Eliminar Usuarios
        [HttpPost]
        public ActionResult EliminarProfesionalSeleccionadoView(string RutProfesional)
        {
            using (var db = new Entities())
            {
                var oUsuario = db.usuario.Find(RutProfesional);

                if (oUsuario.tipo_usuario == 1)
                {
                    var oProfesional = db.kinesiologo.Where(k => k.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                    oProfesional.estado_kinesiologo = "0";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Content("1");

                }
                else if (oUsuario.tipo_usuario == 2)
                {
                    var oProfesional = db.nutricionista.Where(n => n.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                    oProfesional.estado_nutricionista = "0";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Content("1");
                }
                else if (oUsuario.tipo_usuario == 3)
                {
                    var oProfesional = db.cuerpo_tec.Where(ct => ct.usuario_rut_usuario == RutProfesional).FirstOrDefault();

                    oProfesional.estado_ct = "0";
                    db.Entry(oProfesional).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Content("1");
                }

                return Content("0");
            }
        }

        [HttpPost]
        public ActionResult EliminarFutbolistaSeleccionadoView(string RutFutbolista)
        {
            using (var db = new Entities())
            {
                var oFutbolista = db.futbolista.Where(f => f.usuario_rut_usuario == RutFutbolista).FirstOrDefault();

                oFutbolista.estado_futb = "0";

                db.Entry(oFutbolista).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return Content("1");
            }
        }
        #endregion

        public ActionResult ObtenerFechaActual()
        {

            string dia = DateTime.Now.ToString("dd");
            string mes = DateTime.Now.ToString("MMMM").ToUpper();
            string anio = DateTime.Now.ToString("yyyy");

            return Content(dia + " DE " + mes + " DE " + anio);
        }

    }
}