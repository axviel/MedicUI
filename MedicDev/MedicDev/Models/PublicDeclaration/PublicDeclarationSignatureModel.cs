using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicDev.Models.PublicDeclaration
{
    public class PublicDeclarationSignatureModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "PersonName is required")]
        public string PersonName { get; set; }
    }
}
