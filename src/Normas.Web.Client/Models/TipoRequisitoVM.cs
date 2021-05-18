using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Normas.Web.Client.Models
{
    public class TipoRequisitoVM
    {
        public  int TipoRequisitoId { get; set; }
        [Display (Name = "Tipo de Requisito")]
        public string Descripcion { get; set; }
    }
}
