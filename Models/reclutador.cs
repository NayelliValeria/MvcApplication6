//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class reclutador
    {
        public reclutador()
        {
            this.candidato = new HashSet<candidato>();
        }
    
        
        public int idPersona { get; set; }
    
        public virtual ICollection<candidato> candidato { get; set; }
        public virtual persona persona { get; set; }
    }
}
