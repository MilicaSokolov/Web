using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;


namespace WEB_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StvariZaIznajmljivanjeController : ControllerBase
    {

        public StvariZaIznajmljivanjeController(VodeniSportoviContext context)
        {
            this.Context = context;

        }
        public VodeniSportoviContext Context { get; set; }


        [Route("DodatiStvarZaIznajmljivanje/{tip}/{cena}/{naziv}/{lok}")]
        [HttpPost]
        public async Task<ActionResult> DodatiStvarZaIznajmljivanje(string tip, int cena, string naziv, string lok, int maxBrOsoba)
        {
            if(string.IsNullOrWhiteSpace(tip) || tip.Length>50)
            {
                return BadRequest("Lose unet tip stvari!");
            }
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Lose unet naziv stvari!");
            }
            if(string.IsNullOrWhiteSpace(lok) || lok.Length>100)
            {
                return BadRequest("Losa adresa lokala!");
            }
            
        
              try{

                    var izn=new StvariZaIznajmljivanje();
                    izn.Tip=tip;
                    izn.Cena=cena;
                    izn.MaxBrOsoba=maxBrOsoba;
                    var postoji=Context.StvariZaIznajmljivanje.Where(s=>s.Naziv==naziv).FirstOrDefault();
                    if(postoji!=null)
                        return BadRequest("Naziv vec postoji");
                    izn.Naziv=naziv;
                    var lokal=(Lokal)Context.Lokal.Where(st=>st.Adresa==lok).FirstOrDefault();
                    izn.Lokal=lokal;
                    if(lokal==null)
                        return BadRequest("Nije pronadjen lokal");
                    Context.StvariZaIznajmljivanje.Add(izn);
                    await Context.SaveChangesAsync();
                    return Ok($"{izn.ID}");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

        }

        [Route("PreuzmiStvarZaIznajmljivanje/{naziv}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(string tip)
        {
            if(string.IsNullOrWhiteSpace(tip) || tip.Length>50)
            {
                return BadRequest("Lose unet tip stvari!");
            }
            var detalj = await Context.StvariZaIznajmljivanje.Where(p => p.Tip == tip).ToListAsync();
            return Ok(detalj);
        }

        [Route("IzbrisatiStvarZaIznajmljivanje/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if (id < 0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var stvar = await Context.StvariZaIznajmljivanje.FindAsync(id);
                int ind = stvar.ID;
                Context.StvariZaIznajmljivanje.Remove(stvar);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisana stvar {ind}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenaStvarZaIznajmljivanje")]
        [HttpPut]
        public async Task<ActionResult> PromenaStvarZaIznajmljivanje([FromBody] StvariZaIznajmljivanje stvar)
        {
            if(string.IsNullOrWhiteSpace(stvar.Tip) || stvar.Tip.Length>50)
            {
                return BadRequest("Lose unet tip stvari!");
            }
            if(string.IsNullOrWhiteSpace(stvar.Naziv) || stvar.Naziv.Length>50)
            {
                return BadRequest("Lose unet naziv stvari!");
            }
            try
            {
                Context.StvariZaIznajmljivanje.Update(stvar);

                await Context.SaveChangesAsync();
                return Ok($"Stvar je uspesno izmenjena {stvar.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("PreuzmiTipove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiTipove()
        {
            try
            {
                return Ok
                (
                    await Context.StvariZaIznajmljivanje
                    .Select(p => p.Tip)
                    .Distinct()
                    .ToListAsync()
                );
            }
            catch (Exception e)
            {
                return BadRequest("Doslo je do greske: " + e.Message);
            }
        }
        
        [Route("BrOsoba/{tip}/{adr}")]
        [HttpGet]
        public int BrOsoba(string tip, string adr)
        {

            var broj = Context.StvariZaIznajmljivanje.Where(p=>p.Tip==tip && p.Lokal.Adresa==adr).Max(w=>w.MaxBrOsoba);
            return broj;
        }

        [Route("BrojStvari/{tip}/{adr}")]
        [HttpGet]
        public async Task<ActionResult> BrojIznjamljenih(string tip, string adr)
        {
            if(string.IsNullOrWhiteSpace(tip) || tip.Length>50)
            {
                return BadRequest("Lose unet tip stvari!");
            }
            if(string.IsNullOrWhiteSpace(adr) || adr.Length>100)
            {
                return BadRequest("Lose unet naziv stvari!");
            }
            try{
            
                var stvari = await Context.StvariZaIznajmljivanje
                .Where(p=>p.Tip==tip && p.Lokal.Adresa==adr)
                .Select(p=>
                                new {
                                    naziv=p.Naziv
                                }).ToListAsync();
                

                return Ok(stvari);
            }
             catch (Exception e)
            {
                return BadRequest("Doslo je do greske: " + e.Message);
            }
        }
  
    }
     
}
