const tipoSelect = document.getElementById("TipoAtoId");
const fonteSelect = document.getElementById("FonteId");

const regras = {
    "Resolução": ["2"],
    "Portaria": ["3"]
};

tipoSelect.addEventListener("change", () => {
    const tipo = tipoSelect.options[tipoSelect.selectedIndex].text;
    const permitidas = regras[tipo];

    for (const opt of fonteSelect.options) {
        opt.hidden = permitidas ? !permitidas.includes(opt.value) : false;
    }

    const primeiraVisivel = [...fonteSelect.options].find(o => !o.hidden);
    if (primeiraVisivel) {
        fonteSelect.value = primeiraVisivel.value;
    }
});


//// Emenda

const chk = document.getElementById("chkEmenda");
const campos = document.getElementById("camposEmenda");

function toggleEmenda() {
    campos.style.display = chk.checked ? "block" : "none";
}

chk.addEventListener("change", toggleEmenda);

// garante o estado correto ao recarregar a página (ex: erro de validação)
toggleEmenda();
