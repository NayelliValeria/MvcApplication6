using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication6.Models
{
    public partial class persona
    {
        [Key]
        public int idPersona { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido paterno")]
        public string apePaterno { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido Materno")]
        public string apeMaterno { get; set; }
    }
}