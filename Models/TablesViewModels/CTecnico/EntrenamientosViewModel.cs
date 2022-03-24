using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels.CTecnico
{
    public class EntrenamientosViewModel
    {
        #region Atributos Entrenamiento
        public int IdEntrenamiento { get; set; }
        public string tipo_entr { get; set; }
        public DateTime fecha_entr { get; set; }
        public int volumen_entr { get; set; }
        public string objetivo_entr { get; set; }
        public int categoria_id_categoria { get; set; }
        public int cuerpo_tec_id_cu_tec { get; set; }
        public int tipo_entrenam_id_tipo_entr { get; set; }
        #endregion

        #region Atributos Cuerpo Tecnico
        public string nom_ct { get; set; }
        public string appaterno_ct { get; set; }
        public string apmaterno_ct { get; set; }
        public string email_ct { get; set; }
        public int telefono_ct { get; set; } 
        #endregion
    }
}