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
    public class IznajmljivanjeController : ControllerBase
    {

        public IznajmljivanjeController(VodeniSportoviContext context)
        {
            this.Context = context;

        }
        public VodeniSportoviContext Context { get; set; }


        [Route("DodatiIznajmljivanje/{brosoba}/{datumod}/{datumdo}/{jmbg}/{artikal}/{adr}/{kap}/{pos}")]
        [HttpPost]
        public async Task<ActionResult> DodatiIznajmljivanje(int brosoba, DateTime datumod, DateTime datumdo, string jmbg, string artikal, string adr, int kap, int pos)
        {
           
            try{
                    if (string.IsNullOrWhiteSpace(adr) || adr.Length >100)
                    {
                        return BadRequest("Niste ispravno uneli adresu lokala");
                    }
                    if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
                    {
                        return BadRequest("Jmbg mora da ima 13 karaktera");
                    }
                    if (string.IsNullOrWhiteSpace(artikal) || artikal.Length > 50)
                    {
                        return BadRequest("Lose ste uneli tip artikla");
                    }

                    var izn=new Iznajmljivanje();
                    izn.BrOsoba=brosoba;
                    izn.DatumOd=datumod;
                    izn.DatumDo=datumdo;
                    var radnik = new List<Radnici>();
                    izn.Radnici = radnik;
                    var iznajmljivanjaUTOmPeriodu = Context.Iznajmljivanje.Where(x=>
                        (x.DatumOd<=datumod && datumdo<=x.DatumDo||
                        (datumod<=x.DatumOd && datumdo>=x.DatumOd && datumdo <=x.DatumDo)||
                        (x.DatumOd<=datumod && datumod<=x.DatumDo && x.DatumDo<=datumdo)|| 
                        (datumod<=x.DatumOd && x.DatumDo<=datumdo))
                        && x.Lokal.Adresa==adr).ToList();
                    

                    var m=(Musterija)Context.Musterija.Where(p=>p.Jmbg==jmbg).FirstOrDefault();
                    if(m==null)
                        return BadRequest("Nije pronadjena musterija");
                    izn.Musterija=m;
                    
                    var st=Context.StvariZaIznajmljivanje.Where(st=>st.Tip==artikal && st.Lokal.Adresa==adr && st.MaxBrOsoba>=brosoba).ToList();
                    if(st.Count()==0)
                        return BadRequest("Nije pronadjen artikal");
                    foreach (var i in st)
                    {   
                        var slobodno=iznajmljivanjaUTOmPeriodu.Where(p=>p.Stvar.ID==i.ID).FirstOrDefault();
                        if(slobodno==null){
                            izn.Stvar=i;
                            break;
                        }
                    }
                    if(izn.Stvar==null)
                        return BadRequest("Nema slobodno");
                    var days=(int)(datumdo-datumod).TotalDays;
                    izn.Cena=days*izn.Stvar.Cena*brosoba/2;
                    
                    var lok=(Lokal)Context.Lokal.Where(l=>l.Adresa==adr).FirstOrDefault();
                    if(lok==null)
                        return BadRequest("Nije pronadjen lokal");
                    izn.Lokal=lok;
                    
                    if(kap==1)
                    {
                    var radnici= Context.Radnici.Where(p=>p.Pozicija=="Kapetan").ToList();
                    foreach(var r in radnici){
                        var ima = false;
                            foreach(var iznajmljivanje in iznajmljivanjaUTOmPeriodu){
                                var h = iznajmljivanje.Radnici.FirstOrDefault(x=>x.ID == r.ID);
                                if(h!=null)
                                {
                                    ima =true;
                                    break;
                                }
                            }
                            if(!ima)
                            {
                                radnik.Add(r);
                                break;
                            }
                    }
                        
                    }
                    if(pos==1){
                        var radnici= Context.Radnici.Where(p=>p.Pozicija=="Posada").ToList();
                        foreach(var jedanradnik in radnici){
                        var brojac=0;
                            foreach(var iznjamljivanje in iznajmljivanjaUTOmPeriodu){
                                var h = iznjamljivanje.Radnici.FirstOrDefault(x=>x.ID == jedanradnik.ID);
                                if(h!=null)
                                {
                                    if(brojac<3){
                                        radnik.Add(jedanradnik);
                                        brojac++;
                                        break;
                                    }
                                }
                            }
                            if(brojac==3)
                                break;
                    }
                    }
                    if(radnik.Count()>0)
                        izn.Radnici.AddRange(radnik);
                    Context.Iznajmljivanje.Add(izn);
                    await Context.SaveChangesAsync();
                    return Ok(await Context.Iznajmljivanje.Where(p=>p.ID==izn.ID).Select(p=> 
                    new {
                        cena=p.Cena,
                            naziv=p.Stvar.Naziv
                    }).ToListAsync());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [Route("PreuzmiStvarZaIznajmljivanje/{id}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int id)
        {
            try{
                if (id < 0)
                {
                    return BadRequest("Pogresan id");
                }
                var iznajmljivanje = await Context.Iznajmljivanje.Where(p => p.ID == id).ToListAsync();
                return Ok(iznajmljivanje);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [Route("BrojIznjamljenih/{naziv}/{adr}")]
        [HttpGet]
        public int BrojIznjamljenih(string naziv, string adr)
        {
            try{
                if (string.IsNullOrWhiteSpace(adr) || adr.Length >100)
                    {
                        return -1;
                    }
                var broj = Context.Iznajmljivanje
                .Where(p=>p.Stvar.Tip==naziv && p.Lokal.Adresa==adr).Count();   
                
                return broj;
            }
            catch{
                return -1;
            }
          
        }
        [Route("BrojIznjamljenih")]
        [HttpGet]
        public async Task<ActionResult> BrojIznjamljenih()
        {
            try{
            DateTime date= DateTime.Now;
            var prvidat=new DateTime(date.Year, date.Month, 1);
            var zadnjidat= prvidat.AddMonths(1).AddDays(-1);

            var broj = await Context.Iznajmljivanje.Where(p=>p.DatumOd>=prvidat && p.DatumDo<= zadnjidat).ToListAsync();   
            return Ok(broj.Count());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }


        [Route("PronadjiIznajmljivanja/{adresa}/{datum}")]
        [HttpGet]
        public async Task<ActionResult> PronadjiIznajmljivanja(string adresa, DateTime datum)
        {
            try{
                if (string.IsNullOrWhiteSpace(adresa) || adresa.Length >100)
                {
                    return BadRequest("Niste ispravno uneli adresu lokala");
                }
    
                var t = await Context.Iznajmljivanje.Include(p=>p.Lokal)
                .Include(p=>p.Musterija).Where(x=> x.Lokal.Adresa== adresa && x.DatumOd==datum)
                .Include(p=>p.Stvar).ToListAsync();
                return Ok(
                 t.Select(p =>
                    new
                    {
                        MusterijaIme = p.Musterija.Ime,
                        Prezime = p.Musterija.Prezime,
                        Jmbg = p.Musterija.Jmbg,
                        Adresa = p.Lokal.Adresa,
                        datum1 =p.DatumOd.ToString("dd/MM/yyyy"),
                        cena = p.Cena,
                        plovilo=p.Stvar.Naziv
                    })
                 );
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

            
        }
        public class listica{
            public int ID { get; set; }
            public int Cena { get; set; }
            public string  StvarNaziv { get; set; }  
            public string Adresa { get; set; }
        }
        
        [Route("PreuzmiSlobodnaPlovila/{datumOd}/{datumDo}/{adr}/{tip}")]
        [HttpGet]
        public async Task<List<listica>> PreuzmiSlobodnaPlovila(DateTime datumOd, DateTime datumDo, string adr, string tip)
        { 
            List<listica> retval = new List<listica>();
            try{
                
                if (string.IsNullOrWhiteSpace(adr) || adr.Length >100)
                {
                    return retval;
                }
                if (string.IsNullOrWhiteSpace(tip) || tip.Length >50)
                {
                    return retval;
                }
               
                var iznajmljivanjaUTOmPeriodu = await Context.Iznajmljivanje.Include(p=> p.Stvar).Where(x=>(
                    (x.DatumOd<=datumOd && datumDo<=x.DatumDo)||
                    (datumOd<=x.DatumOd && datumDo>=x.DatumOd && datumDo <=x.DatumDo)||
                    (x.DatumOd<=datumOd && datumOd<=x.DatumDo && x.DatumDo<=datumDo)|| 
                    (datumOd<=x.DatumOd && x.DatumDo<=datumDo))
                    && x.Lokal.Adresa==adr).ToListAsync();
           
                var stvari= await Context.StvariZaIznajmljivanje.Include(p=>p.Lokal).Where(p=>p.Lokal.Adresa==adr && p.Tip==tip).ToListAsync();
                   
                    
                    if(stvari.Count>0){
                        foreach(var s in stvari){
                            var listica = new listica();
                            var slobodno=iznajmljivanjaUTOmPeriodu.Where(p=>p.Stvar.ID==s.ID).FirstOrDefault();
                        if(slobodno==null){
                                listica.Adresa = s.Lokal.Adresa;
                                listica.Cena = s.Cena;
                                listica.StvarNaziv = s.Naziv;
                                listica.ID = s.ID;

                                retval.Add(listica);
                            }
                        
                        }
                    }

                    return retval;
            }
              catch{
                return retval;
            }
                
        }
               
        [Route("PromeniIznajmljivanje/{jmbg}/{adr}/{tip}/{dat}/{datOd}/{datDo}")]
        [HttpPut]
        public async Task<ActionResult> PromeniIznajmljivanje(string jmbg, string adr, string tip, DateTime dat, DateTime datOd, DateTime datDo)
        {
            try{
            
                if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
                {
                    return BadRequest("Jmbg mora da ima 13 karaktera");
                }
                if (string.IsNullOrWhiteSpace(adr) || adr.Length >100)
                {
                    return BadRequest("Niste ispravno uneli adresu lokala");
                }
                if (string.IsNullOrWhiteSpace(tip) || tip.Length >50)
                {
                    return BadRequest("Tip stvari nije ispravan");
                }
               
                var izn=(Iznajmljivanje)Context.Iznajmljivanje.Include(x=>x.Stvar).
                Where(l=>l.Musterija.Jmbg==jmbg && l.Lokal.Adresa==adr && l.Stvar.Tip==tip && l.DatumOd==dat).FirstOrDefault();  
                
                if(izn==null)
                {
                    return BadRequest("Nije pronadjeno iznajmljivanje");
                }
                

                 var iznajmljivanjaUTOmPeriodu = await Context.Iznajmljivanje.Include(p=> p.Stvar).Where(x=>(
                    (x.DatumOd<=datOd && datDo<=x.DatumDo)||
                    (datOd<=x.DatumOd && datDo>=x.DatumOd && datDo <=x.DatumDo)||
                    (x.DatumOd<=datOd && datOd<=x.DatumDo && x.DatumDo<=datDo)|| 
                    (datOd<=x.DatumOd && x.DatumDo<=datDo))
                    && x.Lokal.Adresa==adr && x.ID!=izn.ID && x.Stvar.ID==izn.Stvar.ID).ToListAsync();

            if (iznajmljivanjaUTOmPeriodu.Count >0)
            {
                return BadRequest("Zauzeto u tom periodu"); }
            else
            {

                izn.DatumOd = datOd;
                izn.DatumDo = datDo;
                Context.Iznajmljivanje.Update(izn);
                await Context.SaveChangesAsync();
                return Ok($"Iznajmljivanje je uspesno izmenjeno");
            }
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            
        }

   

        [Route("IzbrisatiIznajmljivanje/{jmbg}/{adr}/{tip}/{dat}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(string jmbg, string adr, string tip, DateTime dat)
        {
            try{
                if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13)
                {
                    return BadRequest("Jmbg mora da ima 13 karaktera");
                }
                if (string.IsNullOrWhiteSpace(adr) || jmbg.Length >100)
                {
                    return BadRequest("Niste ispravno uneli adresu lokala");
                }
                if (string.IsNullOrWhiteSpace(tip) || jmbg.Length >50)
                {
                    return BadRequest("Tip stvari nije ispravan");
                }
            
                
                    var izn=(Iznajmljivanje)Context.Iznajmljivanje.Where(l=>l.Musterija.Jmbg==jmbg && l.Lokal.Adresa==adr && l.Stvar.Tip==tip && l.DatumOd==dat).FirstOrDefault();  
                    if(izn==null)
                        return BadRequest("Nije pronadjeno iznajmljivanje");
                    Context.Iznajmljivanje.Remove(izn);
                    await Context.SaveChangesAsync();
                    return Ok("Uspesno obrisano iznajmljivanje");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
            
    }
}


