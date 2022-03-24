using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo
{
    public class SuplementoViewModel
    {
        public string desc_suplemento { get; set; }
        public string tipo_suplemento { get; set; }
        public int cant_suplemento { get; set; }
        public DateTime fecha_suplem { get; set; }
        public string estd_suplemento { get; set; }
        public string comentariosuplem { get; set; }
        public string RutFutbolista { get; set; }
    }
}