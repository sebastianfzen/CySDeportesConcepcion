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
    
    public partial class futbolista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public futbolista()
        {
            this.ant_familiar = new HashSet<ant_familiar>();
            this.ant_personales = new HashSet<ant_personales>();
            this.comentario_nutri = new HashSet<comentario_nutri>();
            this.ev_nutri = new HashSet<ev_nutri>();
            this.hist_clinico = new HashSet<hist_clinico>();
            this.insumo = new HashSet<insumo>();
            this.respuesta = new HashSet<respuesta>();
            this.suplemento = new HashSet<suplemento>();
        }
    
        public int id_futbolista { get; set; }
        public string nom_futb { get; set; }
        public string appaterno_futb { get; set; }
        public string apmaterno_futb { get; set; }
        public int edad_futb { get; set; }
        public System.DateTime fecha_nacim_futb { get; set; }
        public string nacionalidad_futb { get; set; }
        public string posicion_futb { get; set; }
        public string email_futb { get; set; }
        public string calzado_futb { get; set; }
        public int telefono_futb { get; set; }
        public string club_futb { get; set; }
        public string temporada_futb { get; set; }
        public int anio_futb { get; set; }
        public string estado_futb { get; set; }
        public byte[] foto_perfil { get; set; }
        public string usuario_rut_usuario { get; set; }
        public int categoria_id_categoria { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ant_familiar> ant_familiar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ant_personales> ant_personales { get; set; }
        public virtual categoria categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comentario_nutri> comentario_nutri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ev_nutri> ev_nutri { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hist_clinico> hist_clinico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insumo> insumo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<respuesta> respuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<suplemento> suplemento { get; set; }
    }
}