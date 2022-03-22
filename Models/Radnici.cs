using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Radnici")]
    public class Radnici
    {
        [Key]
        public int ID { get; set; }

         [MaxLength(20)]
         [RegularExpression("\\w+")] 
         [Required]
        public string Ime { get; set; }

        [Required]
        [RegularExpression("\\w+")] 
        [MaxLength(20)]
        public string Prezime { get; set; }

        [Required]
        [MaxLength(13)]
        public string Jmbg { get; set; }

        [Required]
        public string Pozicija { get; set; }
        
        public virtual List<Iznajmljivanje> Iznajmljivanje{get; set;}
        public virtual Lokal Lokal { get; set; }

    }
}