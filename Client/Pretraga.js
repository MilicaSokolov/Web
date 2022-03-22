import { IznajmljivanjeForma } from "./IznajmljivanjeForma.js";


var listaAdresa =[]; 


fetch("https://localhost:5001/Lokal/VratiAdrese")
.then(p=>{
    p.json().then(adrese=>{
        adrese.forEach(a=> {
            listaAdresa.push(a);
            console.log(a);
        })
        var lokal1=new IznajmljivanjeForma(listaAdresa,null);
        lokal1.crtaj(document.body);
    })
    
})




