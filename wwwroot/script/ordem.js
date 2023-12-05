/*
----- CHOICES -----
*/
const localContrato = new Choices(document.querySelector(".local-contrato"));
const contrato = new Choices(document.querySelector(".contrato"));

//DESTINO
const destino = new Choices(document.querySelector(".destino"));

//TRANSPOR MOTORISTA
const transpor = new Choices(document.querySelector(".transpor"));
const motorista = new Choices(document.querySelector(".motorista"));

//CONJUNTO PLACAS
const conjunto = new Choices(document.querySelector(".conjunto"));
//
const inputPlacas = [document.querySelector("#PlacaA"), document.querySelector("#PlacaB"), document.querySelector("#PlacaC")]
const inputTipoConjunto = document.querySelectorAll("#TipoConjunto")
const tipoConjunto = ["Caminhao", "Conjunto"]

//
const tipoCommodits = ["Etanol", "Gasolina", "Diesel", "Açucar"]

/*
----- SELECT -----
*/
//LOCAL CONTRATO
const selectLocalContrato = document.querySelector("#Contrato_Locais_IdLocal")
const selectContrato = document.querySelector("#Contrato_IdContrato")

//TRANSPOR MOTORIST
const selectTranspor =  document.querySelector("#Transpor_IdLocal")
const selectMotorista =  document.querySelector("#Motorista_IdMotorista")

//CONJUNTO
const selectConjunto = document.querySelector("#conjuntos_IdConjunto") 



//CHECK MOSTRAR MOTORISTAS
const checkMostrar = document.querySelector("#checkMostrar")



//ONCHANCE SELECT LOCAL CONTRATO
selectLocalContrato.onchange = async () => {
    IdLocal = selectLocalContrato.value

    $(function (){
        $.ajax({
            datatype: "json",
            type: "GET",
            url: "/Contratos/LocaisContratos",
            data: {IdLocal: IdLocal},
            success: function (dados){
                console.log(dados)
                contrato.clearChoices()
                contrato.removeActiveItems()

                let options = []

                $(dados).each(function (i){
                    options.push({
                        value: dados[i].idContrato,
                        label: `${dados[i].idContrato} - ${dados[i].locais.localRazaoSocial} - Volume: ${dados[i].volume/1000} m² - ${tipoCommodits[dados[i].commodity]}`
                    })
                })

                contrato.setChoices(options, 'value', 'label', false)
                contrato.enable()
            }
        })
    })
}



//ONCHANGE TRANSPOR
selectTranspor.onchange = async () => {
    ValueMostrar = checkMostrar.checked
    
    if(ValueMostrar == false){
        MotoristasTranspor()

    }else if ( ValueMostrar == true){
        MotoristaAll()
        
    }

}



//ONCHANGE MOSTRAR
checkMostrar.onchange = async () => {
    ValueMostrar = checkMostrar.checked
    
    if(ValueMostrar == false){
        MotoristasTranspor()

    }else if ( ValueMostrar == true){
        MotoristasAll()
        
    }
}



//ALL MOTORISTAS
function MotoristasAll(){
    $(function (){
        $.ajax({
            datatype: "json",
            type: "GET",
            url: "/motoristas/readmotoristas/",
            data: {},
            success: function (dados){
                motorista.clearChoices()

                let options = []

                $(dados).each(function (i){
                    options.push({
                        value: dados[i].idMotorista,
                        label: `${dados[i].idMotorista} - ${dados[i].nomeMotorista}`
                    })
                })

                motorista.setChoices(options, 'value', 'label', false)
                motorista.enable()
            }
        })
    })
}



//LOCAL MOSTORISTAS
function MotoristasTranspor(){
    IdTranspor = selectTranspor.value

    $(function (){
        $.ajax({
            datatype: "json",
            type: "GET",
            url: "/motoristas/readmotoristaslocal/",
            data: {IdLocal: IdTranspor},
            success: function (dados){
                motorista.clearChoices()

                let options = []

                $(dados).each(function (i){
                    options.push({
                        value: dados[i].idMotorista,
                        label: `${dados[i].idMotorista} - ${dados[i].nomeMotorista}`
                    })
                })

                motorista.setChoices(options, 'value', 'label', false)
                motorista.enable()
            }
        })
    })
}



//ONCHANGE MOTORISTA
selectMotorista.onchange = async () => {
    conjunto.removeActiveItems()

    IdMotorista = selectMotorista.value

    $(function (){
        $.ajax({
            datatype: "json",
            type: "GET",
            url: "/conjuntos/ReadConjuntosMotorista/",
            data: {idMotorista: IdMotorista},
            success: function (dados){
                conjunto.clearChoices()
                conjunto.removeActiveItems()

                let options = []
                let placaA = ""
                let placaB = ""
                let placaC = ""

                $(dados).each(function (i){
                    if (dados[i].placaA != null) {placaA = `${dados[i].placaA.substring(0,3)}-${dados[i].placaA.substring(3)}`} else placaA = ""
                    if (dados[i].placaB != null) {placaB = `${dados[i].placaB.substring(0,3)}-${dados[i].placaB.substring(3)}`} else placaB = ""
                    if (dados[i].placaC != null) {placaC = `${dados[i].placaC.substring(0,3)}-${dados[i].placaC.substring(3)}`} else placaC = ""
                    options.push({
                        value: dados[i].idConjunto,
                        label: `Veiculo: ${dados[i].idConjunto} - ${placaA} ${placaB} ${placaC}`
                    })
                })

                conjunto.setChoices(options, 'value', 'label', false)
                conjunto.enable()
            }
        })
    })
}



//ONCHANGE CONJUNTO
selectConjunto.onchange = async () => {
    IdConjunto = selectConjunto.value

    $(inputPlacas).each( function(i){
        inputPlacas[i].value = "";
    })

    $(function (){
        $.ajax({
            datatype: "json",
            type: "GET",
            url: "/conjuntos/ReadConjunto/",
            data: {idConjunto: IdConjunto},
            success: function(dados){
                inputTipoConjunto[0].value = tipoConjunto[dados.tipoConjunto]
                inputTipoConjunto[1].value = dados.tipoConjunto
                inputPlacas[0].value = dados.placaA ? dados.placaA : ""
                inputPlacas[1].value = dados.placaB ? dados.placaB : ""
                inputPlacas[2].value = dados.placaC ? dados.placaC : ""
            }
        })
    })
}