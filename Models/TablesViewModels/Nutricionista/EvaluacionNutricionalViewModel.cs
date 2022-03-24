using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Nutricionista
{
    public class EvaluacionNutricionalViewModel
    {
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
        public string RutFutbolista { get; set; }
        public int futbolista_id_futbolista { get; set; }
    }
}