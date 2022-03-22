import { Prodavnica } from "./Prodavnica.js";

let listaProdavnica = [];

fetch("https://localhost:5001/Prodavnica/PreuzmiProdavnice", {
    method: "GET"
}).then(p => {
    if (p.ok) {
        p.json().then(data => {
            data.reverse().forEach(pr => {
                let novaProdavnica = new Prodavnica(pr.id, pr.naziv, pr.adresa);
                listaProdavnica.push(novaProdavnica);
                novaProdavnica.crtajProdavnicu(document.body);
            });
        })
    }
})