
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
        RecluITEntities1 db = new RecluITEntities1();

        [Key]
        public int idReclutador { get; set; }
        public bool rememberMe { get; set; }

        [Required(ErrorMessage="Debe seleccionar los permisos del reclutador.")]
        public int permisos { get; set; }

        private string Usuario;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres.")]
        [Display(Name = "usuario")]
        public string usuario {
            get { return this.Usuario; }
            set { this.Usuario = (value != null ? value.Trim() : null); }
        }

        private string Password;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "contraseña")]
        public string password {
            get { return this.Password; }
            set { this.Password = (value != null ? value.Trim() : null); } 
        }

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

    }
}