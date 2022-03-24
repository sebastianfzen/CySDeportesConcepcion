using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo
{
    public class HistorialClinicoFutbolista
    {
        public DateTime fecha_lesion { get; set; }
        public string lugar_lesion { get; set; }
        public int id_lesion { get; set; }
        public string nomb_lesion { get; set; }
        public string zona_lesion { get; set; }
        public string relacion_lesion_anterior { get; set; }
        public DateTime fecha_baja { get; set; }
        public int dias_baja { get; set; }
        public string examenes { get; set; }
        public string tratamiento { get; set; }
        public string RutFutbolista { get; set; }
    }
}