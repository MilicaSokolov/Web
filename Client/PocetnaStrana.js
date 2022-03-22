
import { IznajmljivanjeForma } from "./IznajmljivanjeForma.js";



var listaAdresa =[]; 
var listaTipova =[]; 
let dat = document.querySelector(".main-content");
fetch("https://localhost:5001/Lokal/VratiAdrese")
.then(p=>{
    p.json().then(ad=>{
        ad.forEach(a=> {
            listaAdresa.push(a);
            console.log(a);
        })
        fetch("https://localhost:5001/StvariZaIznajmljivanje/PreuzmiTipove")
        .then(p=>{
            p.json().then(tipovi=>{
                tipovi.forEach(t=> {
                    listaTipova.push(t);
                    console.log(t);
                })
                let iznajmljivanje1 = new IznajmljivanjeForma(listaAdresa, listaTipova);
                iznajmljivanje1.crtajStatistiku(dat);
            })
            
        })
    })
    
})
