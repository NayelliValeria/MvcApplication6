
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace MvcApplication6.Models
{
    public partial class reclutador:DbContext 
    {
        RecluITEntities db = new RecluITEntities();

        public bool rememberMe { get; set; }
        public IList<candidato_persona> candidatosRegistrados { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "Este nombre de {0} no es vlaido")]
        [Display(Name = "usuario")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "El campo {0} no es vlaido")]
        [DataType(DataType.Password)]
        [Display(Name = "contraseña")]
        public string password { get; set; }

        //<summary>Buscar al usuario en la base de datos</summary>
        //<param name = "user"> nombre de usuario </param>
        //<param name = "pass"> contraseña del usuario </param>
        //<returns> true si es correcto, false en caso contrario </returns>
        public reclutador validar(string user, string pass)
        {
            try
            {
                reclutador rec = db.reclutador
                .Where(b => b.usuario == user)
                .Where(b => b.password == pass)
                .Include("persona").Single();
                return rec;
            }
            catch (Exception) { return null;  }
        }
        
        
        /*
        public bool validar(string user, string pass)
        {
            RecluITEntities db = new RecluITEntities();
            ObjectResult<iniciarSesion_Result> rs = db.iniciarSesion(user, pass);
            int i = 1;
            if (rs != null)
            {
                foreach (iniciarSesion_Result result in rs)
                {
                    if (i == 1)
                    {
                        this.idReclutador = result.idReclutador;
                        this.usuario = result.usuario;
                        this.password = result.password;
                        this.permisos = result.permisos;
                        this.idPersona = result.idPersona;
                    }
                    else
                        return false;
                    i++;
                }
                return true;
            }
            else return false;
        }*/
    }
}