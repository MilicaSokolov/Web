using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class VodeniSportoviContext:DbContext
    {       
        public VodeniSportoviContext(DbContextOptions<VodeniSportoviContext> options)
        :base(options)
        {

        }

        public DbSet<Iznajmljivanje> Iznajmljivanje { get; set; }
        public DbSet<Musterija> Musterija{get; set;}
        public DbSet<Radnici> Radnici { get; set; }
        public DbSet<Lokal> Lokal { get; set; }
        public DbSet<StvariZaIznajmljivanje> StvariZaIznajmljivanje { get; set; }


        
    }
}