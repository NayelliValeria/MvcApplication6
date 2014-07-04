using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication6.Models
{
    public partial class candidato
    {
        RecluITEntities db = new RecluITEntities();

        public void setIds()
        {
            try
            {
                this.idCandidato = db.candidato.Max(b => b.idCandidato) > 0 ? db.candidato.Max(b => b.idCandidato) + 1 : 1;
                this.idPersona = db.persona.Max(b => b.idPersona) > 0 ? db.persona.Max(b => b.idPersona) + 1 : 1;
                //this.idCandidato = db.candidato.Max(b => b.idCandidato) + 1;
                //this.idPersona = db.persona.Max(b => b.idPersona) + 1;
            }
            catch (InvalidOperationException)
            {
                this.idCandidato = 1;
                this.idPersona = this.persona.idPersona = 1;
            }
        }
        
    }
}