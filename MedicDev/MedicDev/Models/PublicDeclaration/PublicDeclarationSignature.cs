using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicDev.Models.PublicDeclaration
{
    public class PublicDeclarationSignature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PersonName { get; set; }

        public int PublicDeclarationId { get; set; }
        [ForeignKey("PublicDeclarationId")]
        public PublicDeclaration PublicDeclaration { get; set; }
    }
}
