const botaoTranspor = document.querySelector("#BotaoTransportadoras")
const botaoTransporAdd = document.querySelector("#ButtonAddTrasnpor")

const selectLocal = document.querySelector("#Local_IdLocal")
const idMotorista = document.querySelector("#IdMotorista").value

botaoTranspor.onclick = async () => {
    const modalhtml = "#modalTrasnporId table";
    const modal = document.querySelector(modalhtml)

    modal.innerHTML = `
        <tr>
            <td scope="col">Id</td>
            <td scope="col">Transportadora</td>
            <td scope="col">&ensp;</td>
        </tr>
    `

    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/readlocaismotorista/",
            data: {idMotorista: idMotorista},
            success: function (dados){
                
                $(dados).each(function (i){
                    modal.innerHTML += `
                        <tr id="linha_local${dados[i].idLocal}">
                            <th>${dados[i].idLocal}</th>
                            <th>${dados[i].localRazaoSocial}</th>
                            <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletarTranspor(${dados[i].idLocal})">Excluir</button></th>
                        </tr>
                    `
                })
            }

        })

    })
}


selectLocal.onchange = async() => {
    botaoTransporAdd.disabled = false
}

botaoTransporAdd.onclick = async() => {
    const idLocal = selectLocal.value

    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/createlocaismotorista/",
            data: {idMotorista: idMotorista, idLocal: idLocal},
            success: function (dados){
                botaoTranspor.onclick()
                locais.setChoiceByValue('');
                botaoTransporAdd.disabled = true;
            }

        })

    })

}

function deletarTranspor(idTranspor){
    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/deletelocaismotorista/",
            data: {idMotorista: idMotorista, idLocal: idTranspor},
            success: function (dados){
                botaoTranspor.onclick()
            }

        })

    })
}