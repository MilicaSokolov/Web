
export class IznajmljivanjeForma{
    constructor(listaAdresa, listaTipova)
    {
        
        this.ukupanIznos = null;
        this.listaProizvoda = listaTipova;
        this.listaAdresa=listaAdresa;
        this.kontejner=null;
    }



    /////////////////       IZNAJMLJIVANJE         ///////////////////
    crtajFormu(host)
    {
        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        this.kontejner=host;
        let zajedno = document.createElement("div");
        zajedno.className="zajedno";
        host.appendChild(zajedno);

        var divSveLevo = document.createElement("div");
        divSveLevo.className="levo";
        zajedno.appendChild(divSveLevo);

        var izn= document.createElement("div");
        izn.className="Razdvoj";
        divSveLevo.appendChild(izn);

        var otkaz= document.createElement("div");
        otkaz.className="Razdvoj";
        divSveLevo.appendChild(otkaz);

        this.crtajOtkaz(otkaz);

        var izmena= document.createElement("div");
        izmena.className="Razdvoj";
        divSveLevo.appendChild(izmena);

        this.crtajIzmneu(izmena);

        this.crtajIznajmljivanje(izn);

        var divDesno = document.createElement("div");
        divDesno.className="divDesno";
        zajedno.appendChild(divDesno);

        this.crtajDesno(divDesno);
    }
   
    crtajIznajmljivanje(host){
        let divIzn = document.createElement("div");
        divIzn.classList.add("novired");
        host.appendChild(divIzn);
        let labI = document.createElement("h4");
        labI.innerHTML="Iznajmi:";
        divIzn.appendChild(labI);

        let div = document.createElement("div");
        div.className="novired";
        let day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumOD";
        let lab= document.createElement("label");
        lab.innerHTML="Od:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumDO";
        lab= document.createElement("label");
        lab.innerHTML="Do:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        let div1 = document.createElement("div");
        div1.className="novired";
        host.appendChild(div1);

        let du = document.createElement("button");
        du.innerHTML="Prikazi";
        du.onclick=(ev)=>this.PrikaziSliku(host);
        du.className="button";
        div1.appendChild(du);

        div1 = document.createElement("div");
        div1.className="novired";
        let lab1 = document.createElement("label");
        lab1.innerHTML="JMBG";
        let tb1= document.createElement("input");
        tb1.className="jmbgMusterije jmbgM"; 
        tb1.maxLength=13;
        tb1.minLength=13;
        host.appendChild(div1);
        div1.appendChild(lab1);
        div1.appendChild(tb1);

        div = document.createElement("div");
        div.className="novired";
        let sel1= document.createElement("select");
        sel1.className="adr";
        l = document.createElement("label");
        l.innerHTML="Adresa lokala"
        
        let op2;
        this.listaAdresa.forEach(p => {
            op2=document.createElement("option");
            op2.innerHTML=p;
            op2.value=p;
            sel1.appendChild(op2);
            
        });

        div.appendChild(l);
        div.appendChild(sel1);
        host.appendChild(div);


        div = document.createElement("div");
        div.className="novired";
        let sel= document.createElement("select");
        sel.className="tip";
        sel.onchange=(ev)=>this.ZabranaCB();
        var l = document.createElement("label");
        l.innerHTML="Tip opreme";

        
        let op1;
        this.listaProizvoda.forEach(p => {
            op1=document.createElement("option");
            op1.innerHTML=p;
            op1.value=p;
            sel.appendChild(op1);
            
        });

        div.appendChild(l);
        div.appendChild(sel);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        lab = document.createElement("label");
        lab.innerHTML="Broj osoba";
        let tb = document.createElement("input");
        tb.className="BrOsoba bros";
        tb.type="number";
        div.appendChild(lab);
        div.appendChild(tb);
        host.appendChild(div);

        let divCB = document.createElement("div");
        divCB.className="novired";
        host.appendChild(divCB);

        div = document.createElement("div");
        let opcija = document.createElement("input");
        opcija.type="checkbox";
        opcija.className= "kapetan";
        

        let labela = document.createElement("label");
        labela.innerHTML="Potreban kapetan";

        div.appendChild(opcija);
        div.appendChild(labela);
        divCB.appendChild(div);

        div = document.createElement("div");
        opcija = document.createElement("input");
        opcija.type="checkbox";
        opcija.className="posada";
        

        labela = document.createElement("label");
        labela.innerHTML="Potrebna posada";

        div.appendChild(opcija);
        div.appendChild(labela);
        divCB.appendChild(div);

        div = document.createElement("div");
        div.className="novired"
        host.appendChild(div);

        let d2 = document.createElement("button");
        d2.innerHTML="Iznajmi";
        d2.className="button";
        d2.onclick=(ev)=>{
            this.Iznajmi();
        }
        d2.className="button";
        div.appendChild(d2);

    }

    crtajIzmneu(host){

        let divIzn = document.createElement("div");
        divIzn.classList.add("novired");
        host.appendChild(divIzn);
        let labI = document.createElement("h4");
        labI.innerHTML="Izmeni rezervaciju:";
        divIzn.appendChild(labI);

        let div1 = document.createElement("div");
        div1.className="novired";
        let lab1 = document.createElement("label");
        lab1.innerHTML="JMBG";
        let tb1= document.createElement("input");
        tb1.className="jmbgIzmena"; 
        tb1.maxLength=13;
        tb1.minLength=13;

        host.appendChild(div1);
        div1.appendChild(lab1);
        div1.appendChild(tb1);

        let div = document.createElement("div");
        div.className="novired";
        let sel1= document.createElement("select");
        sel1.className="adrIzmena";
        let l = document.createElement("label");
        l.innerHTML="Adresa lokala"
        
        let op2;
        this.listaAdresa.forEach(p => {
            op2=document.createElement("option");
            op2.innerHTML=p;
            op2.value=p;
            sel1.appendChild(op2);
            
        });

        div.appendChild(l);
        div.appendChild(sel1);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        let sel= document.createElement("select");
        sel.className="tipIzmena";
        l = document.createElement("label");
        l.innerHTML="Tip opreme"
        
        let op1;
        this.listaProizvoda.forEach(p => {
            op1=document.createElement("option");
            op1.innerHTML=p;
            op1.value=p;
            sel.appendChild(op1);
            
        });

        div.appendChild(l);
        div.appendChild(sel);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        let day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumIzmena";
        let lab= document.createElement("label");
        lab.innerHTML="Datum:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumOdIzmena";
        lab= document.createElement("label");
        lab.innerHTML="Novi datum od:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumDoIzmena";
        lab= document.createElement("label");
        lab.innerHTML="Novi datum do:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        let d1 = document.createElement("button");
        d1.innerHTML="Izmeni";
        d1.className="button";
        host.appendChild(d1);
        d1.onclick=(ev)=>this.IzmenaIzn();
    }   

    IzmenaIzn(){
        let jmbg=this.kontejner.querySelector(".jmbgIzmena").value;   
        console.log(jmbg);
        if(jmbg==""){
            alert("Morate uneti jmbg");
        }

        let adr = this.kontejner.querySelector('.adrIzmena option:checked').value;
        console.log(adr);

        let tip = this.kontejner.querySelector('.tipIzmena option:checked').value;
        console.log(tip);
        
        let dat = this.kontejner.querySelector(".datumIzmena").value;
        console.log(dat);

        let datOd = this.kontejner.querySelector(".datumOdIzmena").value;
        console.log(datOd);
       // debugger;
        let datDo = this.kontejner.querySelector(".datumDoIzmena").value;
        console.log(datDo);
        if(datDo==""){
            alert("Morate uneti datum od kog ste zakazali plovilo ");
        }
        console.log(datDo);

        if(datOd>datDo){
            alert("Lose ste uneli datum")
            return;
        }

        fetch("https://localhost:5001/Iznajmljivanje/PromeniIznajmljivanje/"+jmbg +"/" + adr + "/" + tip +"/"+ dat+"/"+ datOd +"/" + datDo,
        {
            method: "PUT"
        }).then(p =>{
            if (p.ok)
            {
               
                    alert("Uspesno ste izmenili rezervaciju!");
                 
            }
            else
            {
                alert("Rezervacija nije pronadjena!");
            }
        })

    }
    ZabranaCB(){
        
        let tip = this.kontejner.querySelector('.tip option:checked').value;
        console.log(tip);

        let adr = this.kontejner.querySelector(".adr option:checked").value;
        console.log(adr);

        let brOsoba=this.kontejner.querySelector(".bros");
        console.log(brOsoba);

        fetch("https://localhost:5001/StvariZaIznajmljivanje/BrOsoba/" +tip+"/"+adr,
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                console.log("Max broj osoba");
                console.log(data);
                brOsoba.min=1;
                brOsoba.max=data;
                })
            }
        });
        
        const kapetan=this.kontejner.querySelector(".kapetan");
        const posada=this.kontejner.querySelector(".posada");
        //debugger;
        if(tip=="Camac" || tip=="Skuter"){
            kapetan.disabled=true;
            posada.disabled=true;
        }
        else{
            kapetan.disabled=false;
            posada.disabled=false;
        }
    }

    crtajDesno(divDesno){
        this.listaAdresa.forEach(p=>{

            let adr = document.createElement("div");
            adr.className=p;
            divDesno.appendChild(adr);

            let more = document.createElement("div");
            more.className="more";
            let obala=document.createElement("div");
            obala.className="paluba";
            adr.appendChild(more);
            adr.appendChild(obala);

            let lokal=document.createElement("div");
            lokal.className="LokalnaPalubi lokal"+p;
            obala.appendChild(lokal);
            let labela = document.createElement("h5");
            labela.innerHTML=p;
            lokal.appendChild(labela);

            let more1 = document.createElement("div");
            more1.className="more12 more1"+p;
            let more2 = document.createElement("div");
            more2.className="more12 more2"+p;
            let dok = document.createElement("div");
            dok.className="dok dok"+p;
            more.appendChild(more1);
            more.appendChild(dok);
            more.appendChild(more2);
            this.crtajSliku(p, more1, more2, lokal);

        })
    }

    crtajSliku(adr, more1, more2, lokal){
        let brojac=0;
        this.listaProizvoda.forEach(tip=>{
       
            fetch("https://localhost:5001/StvariZaIznajmljivanje/BrojStvari/"+tip+"/"+adr,
            {
                method:"GET"

            }).then(s=>{
                if(s.ok){
                
                    s.json().then(data=>{
                        //console.log(tip);
                        //console.log(data);
                            
                        if(tip==="Brod" || tip=="Kruzer"){
                            data.forEach(p=>{
                            
                                let brodic=document.createElement("div");
                               
                                brodic.id=p.naziv;
                                let labela = document.createElement("label");
                                labela.innerHTML=tip +": "+ p.naziv;
                                brodic.appendChild(labela);
                                if(brojac%2==0)
                                {
                                    brodic.className="brod desno";
                                    more1.appendChild(brodic);
                                }
                                else{
                                    brodic.className="brod levo";
                                    more2.appendChild(brodic);
                                }
                                brojac++;
                            })
                        }
                        else{
                            let labela = document.createElement("label");
                                labela.innerHTML=tip +": "+ data.length;
                                lokal.appendChild(labela);
                        }
                    })
                }
            });
       
    })
    }

    PrikaziSliku(){
        //debugger;
        let oddat = this.kontejner.querySelector(".datumOD").value;
        console.log(oddat);


        let dodat = this.kontejner.querySelector(".datumDO").value;
        console.log(dodat);

        if(oddat=="" || dodat==""){
            alert("Morate uneti datum!");
        }
        if(oddat>dodat){
            alert("Lose ste uneli datum")
            return;
        }

        console.log("adrese");
        console.log(this.listaAdresa);
        console.log("proizvodi");
        console.log(this.listaProizvoda);

        this.listaAdresa.forEach(p=>{
            let m1 = this.kontejner.querySelector(".more1"+p);
            let m2 =  this.kontejner.querySelector(".more2"+p);
            let d =  this.kontejner.querySelector(".dok"+p);
            let l=  this.kontejner.querySelector(".lokal"+p);
            var roditelj=m1.parentElement;
            var roditeljlokal=l.parentElement;
            roditelj.removeChild(m1);
            roditelj.removeChild(m2);
            roditelj.removeChild(d);
            roditeljlokal.removeChild(l);

            let more1 = document.createElement("div");
            more1.className="more12 more1"+p;
            let more2 = document.createElement("div");
            more2.className="more12 more2"+p;
            let dok = document.createElement("div");
            dok.className="dok dok"+p;

            let lokal=document.createElement("div");
            lokal.className="LokalnaPalubi lokal"+p;
            let labela = document.createElement("h5");
            labela.innerHTML=p;
            lokal.appendChild(labela);

            roditelj.appendChild(more1);
            roditelj.appendChild(dok);
            roditelj.appendChild(more2);
            roditeljlokal.appendChild(lokal);

           // debugger;
            let brojac=0;
            this.listaProizvoda.forEach(tip=>{
                fetch("https://localhost:5001/Iznajmljivanje/PreuzmiSlobodnaPlovila/"+oddat+"/"+dodat+"/"+p+"/"+tip,
                {
                    method:"GET"

                }).then(s=>{
                    if(s.ok){
                    
                        s.json().then(data=>{
                            console.log(data);
                            
                            if(tip==="Brod" || tip=="Kruzer"){
                                data.forEach(brod=>{
                                    
                                    console.log(brod.stvarNaziv)
                                    let brodic=document.createElement("div");
                                
                                    brodic.id=brod.stvarNaziv;
                                    let labela = document.createElement("label");
                                    labela.innerHTML=tip +": "+ brod.stvarNaziv;
                                    brodic.appendChild(labela);
                                    if(brojac%2==0)
                                    {
                                        brodic.className="brod desno";
                                        more1.appendChild(brodic);
                                    }
                                    else{
                                        brodic.className="brod levo";
                                        more2.appendChild(brodic);
                                    }
                                    brojac++;
                                })
                            }
                            else{
                                let labela = document.createElement("label");
                                    labela.innerHTML=tip +": "+ data.length;
                                    lokal.appendChild(labela);
                            }   
                            
                        })
                    }
                });
             })

            })  
    }

    Iznajmi(){

        let jmbg= this.kontejner.querySelector(".jmbgM").value;    
        console.log(jmbg);
        if(jmbg==""){
            alert("Morate popuniti jmbg!")
            return;
        }

        let adr =  this.kontejner.querySelector('.adr option:checked').value;
        console.log(adr);

        let tip =  this.kontejner.querySelector('.tip option:checked').value;
        console.log(tip);
        

        let brosoba= this.kontejner.querySelector(".bros").value;   
        console.log(brosoba);

        let oddat =  this.kontejner.querySelector(".datumOD").value;
        console.log(oddat);

        let dodat =  this.kontejner.querySelector(".datumDO").value;
        console.log(dodat);
     
        if(oddat==""){
            alert("Morate odabrati datum!")
            return;
        }

        if(oddat>dodat){
            alert("Lose ste uneli datum")
            return;
        }

        const kapetan= this.kontejner.querySelector(".kapetan:checked");
        console.log(kapetan);

        const posada= this.kontejner.querySelector(".posada:checked");
        console.log(posada);

        let k=1;
        let p=1;
        if(kapetan==null)
            k=0;
        if(posada==null)
            p=0;

            console.log("Kapetan");
            console.log(k);
            console.log("Posada");
            console.log(p);
       
        fetch("https://localhost:5001/Iznajmljivanje/DodatiIznajmljivanje/"+brosoba+"/"+oddat+"/"+dodat+"/"+jmbg+"/"+tip+"/"+adr+"/"+k+"/"+p,
        {
            method:"POST"
        }).then(s=>{
            if (s.status ==200){
                console.log()
                //alert("Uspesno ste iznajmili");
               s.json().then(data=>{
                  //  console.log(data);  
                    alert("Iznajmili ste "+ tip + " "+data[0].naziv+" . Cena iznajmljivanja je "+ data[0].cena);
                    this.PrikaziSliku();       
                })            
            }
            else
            {
                if(s.status==400){
                
                    alert("Rezervacija nije uspesna!")
                   // p.json().then(k => alert(k.poruka))
                }              
            }
        })
        .catch(p => {
            console.log(p);
            alert (p);
        });
    }

    
    crtajOtkaz(host){

        let divIzn = document.createElement("div");
        divIzn.classList.add("novired");
        host.appendChild(divIzn);
        let labI = document.createElement("h4");
        labI.innerHTML="Otkazi:";
        divIzn.appendChild(labI);

        let div1 = document.createElement("div");
        div1.className="novired";
        let lab1 = document.createElement("label");
        lab1.innerHTML="JMBG";
        let tb1= document.createElement("input");
        tb1.minLength=13;
        tb1.maxLength=13;
        tb1.className="jmbgMusterije jmbgOtkaz"; 
        host.appendChild(div1);
        div1.appendChild(lab1);
        div1.appendChild(tb1);

        let div = document.createElement("div");
        div.className="novired";
        let sel1= document.createElement("select");
        sel1.className="adrOtkaz";
        let l = document.createElement("label");
        l.innerHTML="Adresa lokala"
        
        let op2;
        this.listaAdresa.forEach(p => {
            op2=document.createElement("option");
            op2.innerHTML=p;
            op2.value=p;
            sel1.appendChild(op2);
            
        });

        div.appendChild(l);
        div.appendChild(sel1);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        let sel= document.createElement("select");
        sel.className="tipOtkaz";
        l = document.createElement("label");
        l.innerHTML="Tip opreme"
        
        let op1;
        this.listaProizvoda.forEach(p => {
            op1=document.createElement("option");
            op1.innerHTML=p;
            op1.value=p;
            sel.appendChild(op1);
            
        });

        div.appendChild(l);
        div.appendChild(sel);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        let day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datumOtkaz";
        let lab= document.createElement("label");
        lab.innerHTML="Datum:"

        div.appendChild(lab);
        div.appendChild(day);
        host.appendChild(div);

        let d1 = document.createElement("button");
        d1.innerHTML="Otkazi";
        d1.className="button";
        host.appendChild(d1);
        d1.onclick=(ev)=>this.Otkazi(host);
    }   

    Otkazi(host){
        //debugger;
        let jmbg=host.querySelector(".jmbgOtkaz").value;   
        console.log(jmbg);
        if(jmbg==""){
            alert("Morate uneti jmbg");
        }

        let adr = host.querySelector('.adrOtkaz option:checked').value;
        console.log(adr);

        let tip = host.querySelector('.tipOtkaz option:checked').value;
        console.log(tip);
        
        let dat = host.querySelector(".datumOtkaz").value;
        console.log(dat);
        
        if(dat==""){
            alert("Morate uneti datum od kog ste zakazali plovilo ");
        }

        fetch("https://localhost:5001/Iznajmljivanje/IzbrisatiIznajmljivanje/"+jmbg+"/"+adr+"/"+tip+"/"+dat,
        {
            method:"DELETE"
        }).then(s=>{
            if(s.ok)
            {
                
                alert("Uspesno ste otkazali!");
                return;
            }
            else {
                 alert("Doslo je do greske!");
            }
        })


          
    }
    ////////////////////          Registracija          ///////////////////
    crtajRegistraciju(host){

        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        this.kontejner=host;

        let pozicija= document.createElement("div");
        pozicija.className="pozicija";
        host.appendChild(pozicija);

        let RegIzn= document.createElement("div");
        RegIzn.className="Registracija";
        pozicija.appendChild(RegIzn);
        this.crtajReg(RegIzn);

    }

    crtajReg(host){

        var divReg = document.createElement("div");
        divReg.className="novired regCentar";
        host.appendChild(divReg);
        let lab1 = document.createElement("h3");
        lab1.innerHTML="Unesite podatke:";
        divReg.appendChild(lab1);

        var div = document.createElement("div");
        div.className="novired ";
        let lab = document.createElement("label");
        lab.innerHTML="Ime";
        let tb= document.createElement("input");
        tb.className="imeMusterije ime1"; 


        host.appendChild(div);
        div.appendChild(lab);
        div.appendChild(tb);


        div = document.createElement("div");
        div.className="novired";
        lab = document.createElement("label");
        lab.innerHTML="Prezime";
        tb= document.createElement("input");
        tb.className="PrezimeMusterije prez1"; 
        host.appendChild(div);
        div.appendChild(lab);
        div.appendChild(tb);

        div = document.createElement("div");
        div.className="novired";
        lab = document.createElement("label");
        lab.innerHTML="JMBG";
        tb= document.createElement("input");
        tb.className="jmbgMusterije jmbg1"; 
        tb.maxLength=13;
        tb.minLength=13;
        host.appendChild(div);
        div.appendChild(lab);
        div.appendChild(tb);

        div = document.createElement("div");
        div.className="novired";
        lab = document.createElement("label");
        lab.innerHTML="BrTel";
        tb= document.createElement("input");
        tb.className="BrTelMusterije brtel1"; 
        host.appendChild(div);
        div.appendChild(lab);
        div.appendChild(tb);

        let d1 = document.createElement("button");
        d1.innerHTML="Registruj se";
        d1.onclick=(ev)=>this.dodajNovuMusteriju();
        d1.className="button";
        host.appendChild(d1);
    }

    dodajNovuMusteriju(){
        let ime= this.kontejner.querySelector(".ime1").value;    
        console.log(ime);
        if(ime==""){
            alert("Morate uneti ime");
        }
        let prezime= this.kontejner.querySelector(".prez1").value;   
        console.log(prezime);
        if(prezime==""){
            alert("Morate uneti prezime");
        }
        let jmbg= this.kontejner.querySelector(".jmbg1").value;   
        console.log(jmbg);
        if(jmbg==""){
            alert("Morate uneti jmbg");
        }
        let tel= this.kontejner.querySelector(".brtel1").value;   
        console.log(tel);
        if(tel==""){
            alert("Morate uneti tel");
        }

        fetch("https://localhost:5001/Musterija/DodajMusteriju/"+ime+"/"+prezime+"/"+jmbg+"/"+tel,
        {
            method:"POST"
        }).then( s=>
            {
               if (s.status ==200)
               {
                   alert("Uspesno ste se registovali");

               }
               if (s.status ==400)
               {
                    p.json().then(k => alert(k.poruka))
               }
        })
        .catch(p => {
            console.log(p);
            alert ("Greška");
        });

    }


    //////////////        POCETNA         ////////////////////
    crtajStatistiku(host){

        let dat = document.querySelector(".statcardsM");
         fetch("https://localhost:5001/Musterija/BrojMusterija",
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
               s.json().then(data=>{
                console.log("Broj musterija");
                console.log(data);
                let l = document.createElement("h2");
                l.innerHTML=data;
                dat.appendChild(l);
                })
            }
        });

        let datR = document.querySelector(".statcardsR");
        fetch("https://localhost:5001/Radnici/BrojRadnika",
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                console.log("Broj radnika");
                console.log(data);
                let l = document.createElement("h2");
                l.innerHTML=data;
                datR.appendChild(l);
                })
            }
        });

        let datI = document.querySelector(".statcardsI");
        fetch("https://localhost:5001/Iznajmljivanje/BrojIznjamljenih",
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                console.log("Broj iznajmljenih");
                console.log(data);
                let l = document.createElement("h2");
                l.innerHTML=data;
                datI.appendChild(l);
                })
            }
        });

        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        this.kontejner=host;
        let novidiv= document.createElement("div");
        novidiv.className="novidiv";
        host.appendChild(novidiv);
        
        let div = document.createElement("div");
        div.className="divadrStat";
        let sel1= document.createElement("select");
        sel1.className="adrStat";
        let l = document.createElement("label");
        l.innerHTML="Adresa lokala"

        
        let op;
        this.listaAdresa.forEach(p => {
            op=document.createElement("option");
            op.innerHTML=p;
            op.value=p;
            sel1.appendChild(op);
            
        });

        div.appendChild(l);
        div.appendChild(sel1);
        novidiv.appendChild(div);

        let divs = document.createElement("div");
        divs.className="divstubici";
        novidiv.appendChild(divs);

        let d1 = document.createElement("button");
        d1.innerHTML="Prikazi";
        d1.className="dugmence ";
        host.appendChild(d1);
        d1.onclick=(ev)=>this.statistika(divs);
        div.appendChild(d1);

        this.listaProizvoda.forEach(el=>
            {
                console.log(el);
                let divzaj=document.createElement("div");
                divzaj.className=el;
                divs.appendChild(divzaj);
    
                let divstat=document.createElement("div");
                divstat.className="kv";
                divzaj.appendChild(divstat);
    
                let l = document.createElement("h5");
                l.innerHTML=el;
                l.className="labela";
                divzaj.appendChild(l);
    
                this.crtajKvadratice(divstat, el, "Adr1")
            })

      
    }


    statistika(host){
        let adr =  this.kontejner.querySelector('.adrStat option:checked').value;
        //console.log(adr);
        

        this.listaProizvoda.forEach(el=>{
            var form= this.kontejner.querySelector("."+el);
            var roditelj=form.parentElement;
            //debugger;
            roditelj.removeChild(form);

             console.log(el);
                let divzaj=document.createElement("div");
                divzaj.className=el;
                host.appendChild(divzaj);
    
                let divstat=document.createElement("div");
                divstat.className="kv";
                divzaj.appendChild(divstat);
    
                let l = document.createElement("h5");
                l.innerHTML=el;
                l.className="labela";
                divzaj.appendChild(l);
    

            this.crtajKvadratice(divstat,el, adr)
        });
       

    }

    crtajKvadratice(host, tip, adr){
        fetch("https://localhost:5001/Iznajmljivanje/BrojIznjamljenih/"+tip+"/"+adr,
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                    console.log(tip);
                       /*
                        let stub=document.createElement("div");
                        stub.className="stubic";
                        stub.style.height=data/100+"%";
                        host.appendChild(stub);*/
                    for(let i=0; i<data; i++)
                    {
                        let kvadratic=document.createElement("div");

                       
                        
                            kvadratic.className="kvadratic";
                            let l = document.createElement("label");
                            l.className="labela";
                            kvadratic.appendChild(l);
                        
                        host.appendChild(kvadratic);
                    }
                    
                })
            }
            else{
                p.json().then(k => alert(k.poruka))
            }
        });
    }

    //////////////         PRETRAGA                ////////////////
    crtaj(host)
    {
        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        this.kontejner=host;

        var divoba = document.createElement("div");
        divoba.className="PretragaOba";
        host.appendChild(divoba);


        var divGore = document.createElement("div");
        divGore.className="pretragaGore";
        divoba.appendChild(divGore);

        this.crtajGore(divGore);

        var divDole = document.createElement("div");
        divDole.className="pretragaDole";
        this.contDole=divGore;

        divoba.appendChild(divDole);

        this.crtajDole(divDole);
    }

    crtajGore(host){
    const gore = document.createElement("div");
        gore.className="KontPretragaGore";
        host.appendChild(gore);
        

        var div = document.createElement("div");
        div.className="novired";
        let sel= document.createElement("select");
        var l = document.createElement("label");
        l.innerHTML="Adresa lokala:"

        //console.log(this.listaAdresa);

        let op1;
        this.listaAdresa.forEach(p => {
            op1=document.createElement("option");
            op1.innerHTML=p;
            op1.value=p;
            sel.appendChild(op1);
            
        });

        div.appendChild(l);
        div.appendChild(sel);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="novired";
        let day= document.createElement("INPUT");
        day.setAttribute("type", "date");
        day.className="datum";
        day.id="datum";
        l= document.createElement("label");
        l.innerHTML="Datum:"

        div.appendChild(l);
        div.appendChild(day);
        host.appendChild(div);

        div = document.createElement("div");
        div.className="Centar";
        let d1 = document.createElement("button");
        d1.innerHTML="Pronadji";
        d1.onclick=(ev)=>this.pronadjiAdresuDatum();
        d1.className = "dugmence";
        div.appendChild(d1);
        host.appendChild(div);
      
    }

    crtajDole(host){

        let table = document.createElement('table');
        let thead = document.createElement('thead');
        let tbody = document.createElement('tbody');
        tbody.className = "Tabela";
        table.className = "Tabela1";
        table.id = "nadji";
        
        table.appendChild(thead);
        table.appendChild(tbody);
        host.appendChild(table);
        
        let row_1 = document.createElement('tr');

        let heading_1 = document.createElement('th');
        heading_1.className = "tabelaKolone";
        heading_1.innerHTML = "Ime";
        let heading_2 = document.createElement('th');
        heading_2.className = "tabelaKolone";
        heading_2.innerHTML = "Prezime";
        let heading_3 = document.createElement('th');
        heading_3.className = "tabelaKolone";
        heading_3.innerHTML = "jmbg";
        let heading_4 = document.createElement('th');
        heading_4.className = "tabelaKolone";
        heading_4.innerHTML = "Datum iznajmljivanja";
        let heading_5 = document.createElement('th');
        heading_5.className = "tabelaKolone";
        heading_5.innerHTML = "Cena";
        let heading_6 = document.createElement('th');
        heading_6.className = "tabelaKolone";
        heading_6.innerHTML = "Adresa lokala";
        let heading_7 = document.createElement('th');
        heading_7.className = "tabelaKolone";
        heading_7.innerHTML = "Naziv plovila";

        row_1.appendChild(heading_1);
        row_1.appendChild(heading_2);
        row_1.appendChild(heading_3);
        row_1.appendChild(heading_4);
        row_1.appendChild(heading_5);
        row_1.appendChild(heading_6);
        row_1.appendChild(heading_7);
        thead.appendChild(row_1);

    }

    PrikaziTabelu(adr, datum){
        console.log(adr, datum);
         var teloTabele = this.obrisiPrethodniSadrzaj();
        fetch("https://localhost:5001/Iznajmljivanje/PronadjiIznajmljivanja/"+adr+"/"+datum,
        {
            method:"GET"

        }).then(s=>{
            if(s.ok){
               
                s.json().then(data=>{
                    data.forEach(a=>{
                        console.log(a);
                        this.crtajRed(teloTabele,a)
                    })
                    
                })
            }
        });
       
    }

    pronadjiAdresuDatum(){
        let option = this.contDole.querySelector("select");
        var lokal=option.options[option.selectedIndex].value;
        console.log(lokal);
        let datepicked = document.querySelector("#datum").value;
        console.log(datepicked);
        this.PrikaziTabelu(lokal, datepicked);
    }

   

    crtajRed(host,s){
        var tr = document.createElement("tr");
        host.appendChild(tr);

        var el = document.createElement("td");
        el.innerHTML=s.musterijaIme;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.prezime;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.jmbg;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.datum1;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.cena;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.adresa;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=s.plovilo;
        tr.appendChild(el);

    }

    obrisiPrethodniSadrzaj()
    {
        var teloTabele =  document.querySelector(".Tabela");
        var roditelj = teloTabele.parentNode;
        roditelj.removeChild(teloTabele);

        teloTabele =  document.createElement("tbody");
        teloTabele.className="Tabela";
        roditelj.appendChild(teloTabele);
        return teloTabele;
    }
}
    
