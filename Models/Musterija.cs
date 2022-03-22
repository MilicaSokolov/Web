using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Musterija")]
    public class Musterija
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


        [RegularExpression("\\d+")] 
        public string BrTelefona { get; set; }

        public virtual List<Iznajmljivanje> Iznajmljivanje{get; set;}
    }
}