let jogarNovamente = true;
let tentativas = 6;
let listaDinamica = [];
let palavraSecretaCategoria = "TECHNOLOGY WORD";
let palavraSecretaSorteada = "";
let palavras = [];
let jogoAutomatico = true;
let lastToken = "";

createSecretWord();
function createSecretWord(){
    fetch('http://localhost:5238/generateWord')
    .then(response => {
        if (!response.ok) {
            throw new Error('Error getting data from API');
        }

        return response.json();
    })
    .then(data => {
        palavraSecretaSorteada = data.word;
        lastToken = data.token
        console.log(palavraSecretaSorteada);
        assembleWordOnScreen();
    })
    .catch(error => {
        console.error('Erro:', error);
    });
}


function assembleWordOnScreen(){
    const categoria = document.getElementById("categoria");
    categoria.innerHTML = palavraSecretaCategoria;

    const palavraTela = document.getElementById("palavra-secreta");
    palavraTela.innerHTML = "";
    
    for(i = 0; i < palavraSecretaSorteada.length; i++){  
        if(listaDinamica[i] == undefined){
            if (palavraSecretaSorteada[i] == " ") {
                listaDinamica[i] = " ";
                palavraTela.innerHTML = palavraTela.innerHTML + "<div class='letrasEspaco'>" + listaDinamica[i] + "</div>"
            }
            else{
                listaDinamica[i] = "&nbsp;"
                palavraTela.innerHTML = palavraTela.innerHTML + "<div class='letras'>" + listaDinamica[i] + "</div>"
            }     
        }
        else{
            if (palavraSecretaSorteada[i] == " ") {
                listaDinamica[i] = " ";
                palavraTela.innerHTML = palavraTela.innerHTML + "<div class='letrasEspaco'>" + listaDinamica[i] + "</div>"
            }
            else{
                palavraTela.innerHTML = palavraTela.innerHTML + "<div class='letras'>" + listaDinamica[i] + "</div>"
            }    
        }
    }   
}

function checkChosenCharacter(letra){
    document.getElementById("tecla-" + letra).disabled = true;
    if(tentativas > 0)
    {
        changeStyleCharacter("tecla-" + letra, false);
        compareLists(letra);

    }    
}

function changeStyleCharacter(tecla, condicao){
    if(condicao == false)
    {
        document.getElementById(tecla).style.background = "#C71585";
        document.getElementById(tecla).style.color = "#ffffff";
    }
    else{
        document.getElementById(tecla).style.background = "#008000";
        document.getElementById(tecla).style.color = "#ffffff";
    }
}

function compareLists(letra){

    fetch('http://localhost:5238/check/' + letra + "/" + lastToken)
    .then(response => {
        if (!response.ok) {
            throw new Error('Error getting data from API');
        }

        return response.json();
    })
    .then(data => {
        if(data.pos.length == 0){
            tentativas--
            carregaImagemForca();

            if(tentativas == 0){
                abreModal("OPS!", "Not this time... The secret word was <br>" + palavraSecretaSorteada);
                piscarBotaoJogarNovamente(true);
            }

        }
        else{
            changeStyleCharacter("tecla-" + letra, true);
            for(i = 0; i < palavraSecretaSorteada.length; i++){
                if(palavraSecretaSorteada[i] == letra){
                    listaDinamica[i] = letra;
                }
            }

            let vitoria = true;
            for(i = 0; i < palavraSecretaSorteada.length; i++){
                if(palavraSecretaSorteada[i] != listaDinamica[i]){
                    vitoria = false;
                }
            }

            assembleWordOnScreen();
            if(vitoria == true)
            {
                abreModal("Congratulations you won...");
                tentativas = 0;
                piscarBotaoJogarNovamente(true);
            }
        }
    })
    .catch(error => {
        console.error('Erro:', error);
    });   
}

function carregaImagemForca(){
    switch(tentativas){
        case 5:
            document.getElementById("imagem").style.background  = "url('./img/forca01.png')";
            break;
        case 4:
            document.getElementById("imagem").style.background  = "url('./img/forca02.png')";
            break;
        case 3:
            document.getElementById("imagem").style.background  = "url('./img/forca03.png')";
            break;
        case 2:
            document.getElementById("imagem").style.background  = "url('./img/forca04.png')";
            break;
        case 1:
            document.getElementById("imagem").style.background  = "url('./img/forca05.png')";
            break;
        case 0:
            document.getElementById("imagem").style.background  = "url('./img/forca06.png')";
            break;
        default:
            document.getElementById("imagem").style.background  = "url('./img/forca.png')";
            break;
    }
}

function abreModal(titulo, mensagem){
    let modalTitulo = document.getElementById("exampleModalLabel");
    modalTitulo.innerText = titulo;

    let modalBody = document.getElementById("modalBody");
    modalBody.innerHTML = mensagem;

    $("#myModal").modal({
        show: true
    });
}

let bntReiniciar = document.querySelector("#btnReiniciar")
bntReiniciar.addEventListener("click", function(){
    jogarNovamente = false;
    location.reload();
});

function sortear(){
    if(jogoAutomatico == true){
        location.reload();  
    }
    else{
        if(palavras.length > 0){
            listaDinamica=[];
            createSecretWord();
            assembleWordOnScreen();
            resetaTeclas();
            tentativas = 6;
            piscarBotaoJogarNovamente(false);
        }
    }
}

function resetaTeclas(){
    let teclas = document.querySelectorAll(".teclas > button")
    teclas.forEach((x) =>{
        x.style.background = "#FFFFFF";
        x.style.color = "#8B008B";
        x.disabled = false;
    });
}

async function piscarBotaoJogarNovamente(querJogar){
    if(querJogar){
        document.getElementById("jogarNovamente").style.display = "block";
    }
    else{
        document.getElementById("jogarNovamente").style.display = "none";
    }
}
