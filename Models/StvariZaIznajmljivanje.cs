using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models
{
    [Table("StvariZaIznajmljivanje")]
    public class StvariZaIznajmljivanje
    {
         [Key]
        public int ID { get; set; }


        [MaxLength(50)]
        [RegularExpression("\\w+")] 
        [Required]
        public string Tip { get; set; } 

        
        [MaxLength(50)]
        [Required]
        public string Naziv { get; set; } 



        [Required]
        [RegularExpression("\\d+")] 
        public int Cena { get; set; }   

        [Required]
        [RegularExpression("\\d+")] 
        public int MaxBrOsoba { get; set; }   


        public List<Iznajmljivanje> Iznajmljivanje{get; set;}
        public Lokal Lokal { get; set; }
    }
}