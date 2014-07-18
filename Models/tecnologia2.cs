using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication6.Models
{
    public partial class tecnologia
    {
        [Key]
        public int idTecnologia { get; set; }
        [DataType(DataType.Text, ErrorMessage = "En este campo sólo se permite texto")]
        [Required(ErrorMessage = "Debe escribir el nombre de la tecnología.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Nombre de la tecnología")]
        public string nombre { get; set; }
    }
}