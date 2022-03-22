using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Lokal
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [MaxLength(100)]
        public string Adresa { get; set; }

        public List<StvariZaIznajmljivanje> StoStvariZaIznajmljivanjelovi{ get; set; }

        public List<Radnici> Radnici{ get; set; }

         public List<Iznajmljivanje> Iznajmljivanje{ get; set; }
        
    }
}