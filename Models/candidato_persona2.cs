using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication6.Models
{

    public partial class candidato_persona
    {
        private candidato cd;

        private void getCandidato()
        {
            cd = new candidato();
            cd.idCandidato = this.idCandidato;
            cd.idPersona = this.idPersona;
            cd.idReclutador = this.idReclutador;
        }

        public void setTecnologia( ) 
        {
            getCandidato();

        }
    }
}