using System;
using System.ComponentModel.DataAnnotations;

namespace Normas.Web.Client.Models
{
    public class NormaVM
    {
        public int NormaID { get; set; }
        public String Clave { get; set; }
        [Display(Name = "Descripción")]
        public String Descripcion { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Publicación")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPublicacion { get; set; }
        public Boolean Activo { get; set; }
        public int TipoRequisitoId { get; set; }
        public TipoRequisitoVM Requisito { get; set; }
    }
}
