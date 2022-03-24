using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo
{
    public class InsumoFutbolista
    {
        public string descripcion_insumo { get; set; }
        public string tipo_insumo { get; set; }
        public int cantidad_insumo { get; set; }
        public string comentarioinsumo { get; set; }
        public DateTime fecha_insumo { get; set; }
        public string estado_insumo { get; set; }
        public string RutFutbolista { get; set; }
    }
}