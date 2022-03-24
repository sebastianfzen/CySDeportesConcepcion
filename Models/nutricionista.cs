//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyS___DeportesConcepcioin_v2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class nutricionista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nutricionista()
        {
            this.encuesta = new HashSet<encuesta>();
            this.ev_nutri = new HashSet<ev_nutri>();
            this.suplemento = new HashSet<suplemento>();
        }
    
        public int id_nutri { get; set; }
        public string nom_nutri { get; set; }
        public string appaterno_nutri { get; set; }
        public string apmaterno_nutri { get; set; }
        public string email_nutri { get; set; }
        public int telefono_nutri { get; set; }
        public string estado_nutricionista { get; set; }
        public string usuario_rut_usuario { get; set; }
        public byte[] foto_perfil { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<encuesta> encuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ev_nutri> ev_nutri { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<suplemento> suplemento { get; set; }
    }
}