using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Models
{
    [Table("Iznajmljivanje")]
    public class Iznajmljivanje
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression("\\d+")]
        public int BrOsoba { get; set; }


        [Required]
        [RegularExpression("\\d+")]
        public int Cena { get; set; }

        [Required]
        public DateTime DatumOd { get; set; }

        [Required]
        public DateTime DatumDo { get; set; }
 

        [JsonIgnore]    
        public virtual Musterija Musterija { get; set; }
        [JsonIgnore]    
        public virtual StvariZaIznajmljivanje Stvar { get; set; }
        [JsonIgnore]    
        public virtual List<Radnici> Radnici { get; set; }
        [JsonIgnore]    
        public virtual Lokal Lokal { get; set; }
    

    }
}