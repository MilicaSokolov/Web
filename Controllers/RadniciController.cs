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
    public class RadniciController : ControllerBase
    {

        public RadniciController(VodeniSportoviContext context)
        {
            this.Context = context;

        }
        public VodeniSportoviContext Context { get; set; }


        [Route("DodatiRadnika")]
        [HttpPost]
        public async Task<ActionResult> DodatiRadnika([FromBody] Radnici radnik)
        {
            if(string.IsNullOrWhiteSpace(radnik.Ime) || radnik.Ime.Length>20)
                {
                    return BadRequest("Lose uneto ime radnika!");
                }
                if(string.IsNullOrWhiteSpace(radnik.Prezime) || radnik.Prezime.Length>20)
                {
                    return BadRequest("Lose uneto prezime radnika!");
                }
            
                if (string.IsNullOrWhiteSpace(radnik.Jmbg) || radnik.Jmbg.Length != 13)
                {
                    return BadRequest("Jmbg mora da ima 13 karaktera");
                }
            try
            {  var postojiradnik =Context.Musterija.Where(p=> p.Jmbg==radnik.Jmbg).FirstOrDefault();
                if(postojiradnik!=null)
                {
                     return BadRequest($"Radnik sa jmbg-om {radnik.Jmbg} vec postoji");
                }
                Context.Radnici.Add(radnik);
                await Context.SaveChangesAsync();
                return Ok($"Radnik je dodat! ID je: {radnik.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("PreuzmiRadnika/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiRadnika(string jmbg)
        {

            var radnik = await Context.Radnici.Where(p => p.Jmbg == jmbg).ToListAsync();
            return Ok(radnik);
        }

        [Route("BrojRadnika")]
        [HttpGet]
        public int BrojRadnika()
        {

            var broj = Context.Radnici.Count();   
            return broj;
        }

        [Route("IzmeniRadnika")]
        [HttpPut]
        public async Task<ActionResult> IzmeniRadnika([FromBody] Radnici radnik)
        {
            if (string.IsNullOrWhiteSpace(radnik.Ime) || radnik.Ime.Length > 50)
            {
                return BadRequest("Pogresno ime radnika");
            }
            if (string.IsNullOrWhiteSpace(radnik.Prezime) || radnik.Prezime.Length > 50)
            {
                return BadRequest("Pogresno prezime radnika");
            }
            if (string.IsNullOrWhiteSpace(radnik.Jmbg) || radnik.Jmbg.Length != 13)
            {
                return BadRequest("Jmbg mora da ima 13 karaktera");
            }
            try
            {
                Context.Radnici.Update(radnik);
                await Context.SaveChangesAsync();
                return Ok($"Radnik je uspesno izmenjen {radnik.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("PromenitiImePrezimeRadnika/{jmbg}/{ime}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> PromenitiImePrezimeRadnika(string jmbg, string ime, string prezime)
        {
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>20)
                {
                    return BadRequest("Lose uneto ime radnika!");
                }
                if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>20)
                {
                    return BadRequest("Lose uneto prezime radnika!");
                }
            
                if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
                {
                    return BadRequest("Jmbg mora da ima 13 karaktera");
                }
 
            try
            {
                var radnik=Context.Radnici.Where(p=> p.Jmbg==jmbg).FirstOrDefault();
                if(radnik!=null)
                {
                    radnik.Ime=ime;
                    radnik.Prezime=prezime;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno promenjen radnik sa jmbg-om {jmbg}");
                }
                else return BadRequest("Radnik nije pronadjen");
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

            [Route("IzbrisatiRadnika/{id}")]
            [HttpDelete]
            public async Task<ActionResult> Izbrisi(int id)
            {
                if(id<0)
                {
                    return BadRequest("Pogresan id");
                }
                try
                {
                    var radnik = await Context.Radnici.FindAsync(id);
                    if(radnik!=null)
                    {
                        Context.Radnici.Remove(radnik);
                        await Context.SaveChangesAsync();
                        return Ok("Uspesno obrisan radnik");
                    }
                    else return BadRequest("Radnik sa id-em {id} ne postoji u bazi");
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

        
    }     
}
