using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.CTecnico
{
    public class CTecnicoViewModel
    {
        public int IdCTecnico { get; set; }
        public string NomCTecnico { get; set; }
        public string ApPaternoCTecnico { get; set; }
        public string ApMaternoCTecnico { get; set; }
        public int TelefonoCTecnico { get; set; }
        public string EmailCTecnico { get; set; }
        public string PassCTecnico { get; set; }
        public string ConfirmarPassCTecnico { get; set; }
    }
}