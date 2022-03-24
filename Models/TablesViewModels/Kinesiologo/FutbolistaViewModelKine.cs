using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo
{
    public class FutbolistaViewModelKine
    {
        #region Atributos del Futbolista
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

        #region Atributos del Antecedente Familiar
        public string Cardiopatias { get; set; }
        public string Diabetes { get; set; }
        public string Hipertension { get; set; }
        public string Otros { get; set; }
        #endregion

        #region Atributos del Antecedente Personal
        public string EnferInfancia { get; set; }
        public string Alergias { get; set; }
        public string Operaciones { get; set; }
        public string Traumatismos { get; set; }
        public string AlterVisual { get; set; }
        public string ExamenFisico { get; set; }
        public float Talla { get; set; }
        public float Peso { get; set; }
        public float FC { get; set; }
        public float PA { get; set; }
        public string AparatoLocomotor { get; set; }
        public string AlterracionPost { get; set; }
        public string GenuValgo { get; set; }
        public string PiePlano { get; set; }
        public string Otro { get; set; }
        #endregion

        #region Atributos del Historial Clinico
        public System.DateTime fecha_lesion { get; set; }
        public string lugar_lesion { get; set; }
        public int id_lesion { get; set; }
        public string zona_lesion { get; set; }
        public string relacion_lesion_anterior { get; set; }
        public System.DateTime fecha_baja { get; set; }
        public int dias_baja { get; set; }
        public string examenes { get; set; }
        public string tratamiento { get; set; }
        #endregion

        public string descripcion_insumo { get; set; }
        public string tipo_insumo { get; set; }
        public int cantidad_insumo { get; set; }
        public string comentarioinsumo { get; set; }
        public DateTime fecha_insumo { get; set; }
        public string estado_insumo { get; set; }
        public int futbolista_id_futbolista { get; set; }
        public int kinesiologo_id_kinesiologo { get; set; }
    }
}