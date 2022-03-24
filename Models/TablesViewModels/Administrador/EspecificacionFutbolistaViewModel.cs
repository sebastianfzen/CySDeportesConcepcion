using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.Administrador
{
    public class EspecificacionFutbolistaViewModel
    {
        public FutbolistaViewModel FutbolistaViewModel { get; set; }
        public List<CategoriaViewModel> categoriaViewModels { get; set; }
    }
}