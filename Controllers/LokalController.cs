using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WEB_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LokalController : ControllerBase
    {
          public VodeniSportoviContext Context { get; set; }
        public LokalController(VodeniSportoviContext context)
        {
            this.Context = context;
        }

        
        [Route("DodajLokal")]
        [HttpPost]
        public async Task<ActionResult> DodatiIznajmljivanje([FromBody] Lokal lokal)
        {
            if(string.IsNullOrWhiteSpace(lokal.Adresa) || lokal.Adresa.Length>100)
            {
                return BadRequest("Losa adresa lokala!");
            }
            if(string.IsNullOrWhiteSpace(lokal.Naziv) || lokal.Adresa.Length>50)
            {
                return BadRequest("Los naziv lokala!");
            }
            try
            {
                Context.Lokal.Add(lokal);
                await Context.SaveChangesAsync();
                return Ok($"Lokal je dodat! ID je: {lokal.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("PreuzmiLokale")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiLokale()
        {
            try
            {
                return Ok(await Context.Lokal.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest("Doslo je do greske: " + e.Message);
            }
        }

        [EnableCors("CORS")]
        [Route("VratiAdrese")]
        [HttpGet]
        public async Task<ActionResult> VratiAdrese()
        {
            try
            {
                return Ok
                (
                    await Context.Lokal
                    .Select(p => p.Adresa)
                    .Distinct()
                    .ToListAsync()
                );
            }
            catch (Exception e)
            {
                return BadRequest("Doslo je do greske: " + e.Message);
            }
        }
        [Route("PromeniAdresuLokala{ime}/{adresa}")]
        [HttpPut]
        public async Task<ActionResult> PromeniBolnicu(string ime, string adresa)
        {
            
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>50)
            {
                return BadRequest("Lose unet naziv lokala!");
            }
            if(string.IsNullOrWhiteSpace(adresa) || adresa.Length>100)
            {
                return BadRequest("Lose uneta adresa lokala!");
            }
            try
             {
                 var lok = Context.Lokal.Where(p=> p.Naziv==ime).FirstOrDefault();
                
                 if(lok!=null) //nasao trazenu bolnicu u bazi, postoji
                 {
                     lok.Adresa=adresa;

                     await Context.SaveChangesAsync();
                     return Ok("Uspesno ste izmenili adresu lokala!");
                 }
                else{
                    return BadRequest("Nije pronadjen lokal!");
                 }
                
                
             }
             catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
            
         }

        [Route("IzbrisiLokal/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPacijenta(int id)
        {
            if(id < 0)
            {
                return BadRequest("Los ID lokala!");
            }
            try{
                var lokal=await Context.Lokal.FindAsync(id);
                int idl=lokal.ID;
                Context.Lokal.Remove(lokal);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno izbrisana lokal sa identifikatorom {idl}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}