using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Nutricionista
{
    public class DetallesFutbolistaViewModel
    {
        public FutbolistaViewModelNutri FutbolistaViewModelNutri { get; set; }
        public List<EvaluacionNutricionalViewModel> evaluacionNutricionalViewModels { get; set; }
        public List<SuplementoViewModel> suplementoViewModels { get; set; }
        public List<FutbolistaViewModelNutri> FutbolistaViewModelNutriList { get; set; }
        public List<InsumoFutbolista> insumoFutbolistas { get; set; }
    }
}