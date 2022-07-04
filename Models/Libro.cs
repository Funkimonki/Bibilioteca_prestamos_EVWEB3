//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bibilioteca_prestamos_EVWEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Libro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Libro()
        {
            this.Prestamo = new HashSet<Prestamo>();
            this.Ejemplar = new HashSet<Ejemplar>();
        }
    
        public int id_Libro { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> id_Editorial { get; set; }
        public Nullable<int> id_Autor { get; set; }
        public string archivo { get; set; }
    
        public virtual Autor Autor { get; set; }
        public virtual Editorial Editorial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ejemplar> Ejemplar { get; set; }
    }
}