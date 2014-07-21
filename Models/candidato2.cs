using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication6.Models
{
    public partial class candidato
    {
        RecluITEntities1 db = new RecluITEntities1();

        [Key]
        public int idCandidato { get; set; }

        private string curp;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "Este {0} no es valido")]
        [RegularExpression("[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[0-9]{2}$", ErrorMessage = "Este no es un {0} valido")]
        [Display(Name = "CURP")]
        public string CURP {
            get { return this.curp; }
            set { this.curp = value!=null?value.Trim():null; }
        }

        private string rfc;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(15, ErrorMessage = "Este {0} no es valido")]
        [RegularExpression("[A-Z]{4}[0-9]{6}[A-Z0-9]{3}$")]
        [Display(Name = "RFC")]
        public string RFC {
            get { return this.rfc; }
            set { this.rfc = value != null ? value.Trim() : null; }
        }

        private string Email;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "Este {0} no es valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string email {
            get { return this.Email; }
            set { this.Email = value != null ? value.Trim() : null; }
        }

        private string Telefono;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "Este {0} no es valido")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string telefono {
            get { return this.Telefono; }
            set { this.Telefono = value != null ? value.Trim() : null; }
        }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Palabras clave")]
        public string palabrasClave { get; set; }

        public void setIds()
        {
            try
            {
                this.idCandidato = db.candidato.Max(b => b.idCandidato) > 0 ? db.candidato.Max(b => b.idCandidato) + 1 : 1;
                //this.idCandidato = db.candidato.Max(b => b.idCandidato) + 1;
                //this.idPersona = db.persona.Max(b => b.idPersona) + 1;
            }
            catch (InvalidOperationException)
            {
                this.idCandidato = 1;
            }
            try 
            {
                this.idPersona = this.persona.idPersona = db.persona.Max(b => b.idPersona) > 0 ? db.persona.Max(b => b.idPersona) + 1 : 1;
            }
            catch (InvalidOperationException)
            {
                this.idPersona = this.persona.idPersona = 1;
            }
        }
        
    }
}