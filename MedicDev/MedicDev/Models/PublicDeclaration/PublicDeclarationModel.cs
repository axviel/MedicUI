using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicDev.Models.PublicDeclaration
{
    public class PublicDeclarationModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DeclarationDate is required")]
        public DateTime DeclarationDate { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "SignatureList is required")]
        public List<PublicDeclarationSignatureModel> SignatureList { get; set; }

    }
}
