using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Kinesiologo
{
    public class DetallesFutbolistaViewModel
    {
        public FutbolistaViewModelKine FutbolistaViewModelKine { get; set; }
        public List<HistorialClinicoFutbolista> historialClinicoFutbolistas { get; set; }
        public List<InsumoFutbolista> insumoFutbolistas { get; set; }
        public List<EvaluacionNutricionalViewModel> evaluacionNutricionalViewModels { get; set; }
        public List<SuplementoViewModel> suplementoViewModels { get; set; }
    }
}