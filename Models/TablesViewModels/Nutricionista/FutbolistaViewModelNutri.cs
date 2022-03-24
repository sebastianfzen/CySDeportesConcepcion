using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Nutricionista
{
    public class FutbolistaViewModelNutri
    {
        #region Atributos del Futbolista
        public int id_futbolista { get; set; }
        public string RutFutbolista { get; set; }
        public int TipoFutbolista { get; set; }
        public string PassFutbolista { get; set; }
        public string ConfirmarPassFutbolista { get; set; }
        public string NomFutbolista { get; set; }
        public string ApPaternoFutbolista { get; set; }
        public string ApMaternoFutbolista { get; set; }
        public int EdadFutbolista { get; set; }
        public DateTime FechaNacimientoFutbolista { get; set; }
        public string NacionalidadFutbolista { get; set; }
        public string PosicionFutbolista { get; set; }
        public string EmailFutbolista { get; set; }
        public string CalzadoFutbolista { get; set; }
        public string TelefonoFutbolista { get; set; }
        public string ClubFutbolista { get; set; }
        public string TemporadaFutbolista { get; set; }
        public int AnioFutbolista { get; set; }
        public string EstadoFutbolista { get; set; }
        public string FotoFutbolista { get; set; }
        public int CategoriaFutbolista { get; set; }
        #endregion

        #region Atributos de la Evaluacion Nutricional
        public double peso { get; set; }
        public double talla_estatura { get; set; }
        public double porcent_masa_adiposa { get; set; }
        public double kilo_masa_adiposa { get; set; }
        public int estado_uno { get; set; }
        public int estado_uno_dos { get; set; }
        public double porcent_musculo { get; set; }
        public double kilo_musculo { get; set; }
        public int estado_dos { get; set; }
        public int estado_dos_dos { get; set; }
        public double sumatoria_pliegue { get; set; }
        public int estado_tres { get; set; }
        public double imo { get; set; }
        public int estado_cuatro { get; set; }
        #endregion

        public string desc_suplemento { get; set; }
        public string tipo_suplemento { get; set; }
        public int cant_suplemento { get; set; }
        public DateTime fecha_suplem { get; set; }
        public string estd_suplemento { get; set; }
        public string comentariosuplem { get; set; }
    }
}