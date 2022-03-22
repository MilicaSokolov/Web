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
    public class MusterijaController : ControllerBase
    {
        
        public VodeniSportoviContext Context { get; set; }
 
         public MusterijaController(VodeniSportoviContext context)
        {
            this.Context=context;
        }

        [Route("DodatiMusteriju/{Musterija}")]
        [HttpPost]
        public async Task<ActionResult> DodatiMusteriju([FromBody] Musterija musterija)
        {
            if(string.IsNullOrWhiteSpace(musterija.Ime) || musterija.Ime.Length>20)
            {
                return BadRequest("Lose uneto ime musterije!");
            }
            if(string.IsNullOrWhiteSpace(musterija.Prezime) || musterija.Prezime.Length>20)
            {
                return BadRequest("Lose uneto prezime musterije!");
            }
        
            if (string.IsNullOrWhiteSpace(musterija.Jmbg) || musterija.Jmbg.Length != 13)
            {
                return BadRequest("Jmbg mora da ima 13 karaktera");
            }
            try
            {
                var postojimusterija =Context.Musterija.Where(p=> p.Jmbg==musterija.Jmbg).FirstOrDefault();
                if(postojimusterija!=null)
                {
                     return BadRequest($"Musterija sa jmbg-om {musterija.Jmbg} vec postoji");
                }
                Context.Musterija.Add(musterija);
                await Context.SaveChangesAsync();
                return Ok($"Musterija je dodata! ID je: {musterija.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("DodajMusteriju/{ime}/{prezime}/{jmbg}/{brtel}")]
        [HttpPost]
        public async Task<ActionResult> DodajPacijenta(string ime, string prezime, string jmbg, string brtel)
        {
            
  
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>20)
            {
                return BadRequest("Lose uneto ime musterije!");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>20)
            {
                return BadRequest("Lose uneto prezime musterije!");
            }
          
            if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
            {
                return BadRequest("Jmbg mora da ima 13 karaktera");
            }

            try{

                var postojimusterija =Context.Musterija.Where(p=> p.Jmbg==jmbg).FirstOrDefault();
                if(postojimusterija!=null)
                {
                     return BadRequest($"{postojimusterija.ID}");
                }
                var musterija=new Musterija();
                musterija.Ime=ime;
                musterija.Prezime=prezime;
                musterija.Jmbg=jmbg;
                musterija.BrTelefona=brtel;
               
                Context.Musterija.Add(musterija);
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            
        }
        [Route("PreuzmiSveMusterije")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiMusterije()
        {
            return Ok(await Context.Musterija.Select(p=> 
            new {
                p.ID,
                p.Ime,
                p.Prezime,
                p.Jmbg,
                p.BrTelefona
            }).ToListAsync());
        }


        [Route("PreuzmiMusteriju/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiRadnika(string jmbg)
        {

            var musterija = await Context.Musterija.Where(p => p.Jmbg == jmbg).ToListAsync();
            return Ok(musterija);
        }

        [Route("IzmeniMusteriju")]
        [HttpPut]
        public async Task<ActionResult> IzmeniMusteriju([FromBody] Musterija musterija)
        {
            if (string.IsNullOrWhiteSpace(musterija.Ime) || musterija.Ime.Length > 20)
            {
                return BadRequest("Pogresno ime musterije");
            }
            if (string.IsNullOrWhiteSpace(musterija.Prezime) || musterija.Prezime.Length > 20)
            {
                return BadRequest("Pogresno prezime musterije");
            }
            if (string.IsNullOrWhiteSpace(musterija.Jmbg) || musterija.Jmbg.Length != 13)
            {
                return BadRequest("Jmbg mora da ima 13 karaktera");
            }
            try
            {
                Context.Musterija.Update(musterija);
                await Context.SaveChangesAsync();
                return Ok($"Musterija je uspesno izmenjena {musterija.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiImePrezimeMusterije/{jmbg}/{ime}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> PromeniImePrezimeMusterije(string jmbg, string ime, string prezime)
        {
            if (string.IsNullOrWhiteSpace(ime) || ime.Length > 50)
            {
                return BadRequest("Pogresno ime musterije");
            }
            if (string.IsNullOrWhiteSpace(prezime) || prezime.Length > 50)
            {
                return BadRequest("Pogresno prezime musterije");
            }
            if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
            {
                return BadRequest("Jmbg mora da ima 13 karaktera");
            }
 
            try
            {
                var musterija=Context.Musterija.Where(p=> p.Jmbg==jmbg).FirstOrDefault();
                if(musterija!=null)
                {
                    musterija.Ime=ime;
                    musterija.Prezime=prezime;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno promenjena musterija sa jmbg-om {jmbg}");
                }
                else return BadRequest("Musterija nije pronadjen");
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("BrojMusterija")]
        [HttpGet]
        public int BrojIznjamljenih()
        {

            var broj = Context.Musterija.Count();   
            return broj;
        }

        [Route("IzbrisatiMusteriju/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id<0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var musterija = await Context.Musterija.FindAsync(id);
                if(musterija!=null)
                {
                Context.Musterija.Remove(musterija);
                await Context.SaveChangesAsync();
                return Ok("Uspesno obrisana musterija");
                }
                else return BadRequest("Musterija sa id-em {id} ne postoji u bazi");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
