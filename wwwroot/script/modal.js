
const CodLocal = document.querySelector('#IdLocal').value

const buttons = {
    readResponsaveis: document.querySelector('#ButtonResponsaveis'),
    createResponsavel: document.querySelector('#ButtonCreateResponsavel'),

    readEmails: document.querySelector('#ButtonEmails'),
    createEmail: document.querySelector('#ButtonCreateEmail'),

    readTelefones: document.querySelector('#ButtonTelefones'),
    createTelefone: document.querySelector('#ButtonCreateTelefone'),
}

// ----- // Responsáveis

buttons.readResponsaveis.onclick = async () => {

    const modalhtml = "#ModalResponsaveis table";
    const modal = document.querySelector('#ModalResponsaveis table')

    modal.innerHTML = `
        <tr>
            <td scope="col">Id</td>
            <td scope="col">Nome</td>
            <td scope="col">&ensp;</td>
        </tr>
        `
    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/Responsaveis/",
            data: {CodLocal: CodLocal},
            success: function (dados){
                // console.log(dados)
                $(dados).each(function (i){
                    // console.log(dados[i])
                    modal.innerHTML += `
                    <tr id="linha_responsavel${dados[i].idResponsavel}">
                        <th>${dados[i].idResponsavel}</th>
                        <th>${dados[i].responsavel}</th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('responsavel', ${dados[i].idResponsavel}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `
                })
                
            }

        })

    })

}

buttons.createResponsavel.onclick = async () => {

    const modalhtml = "#ModalResponsaveis table";
    const modal = document.querySelector('#ModalResponsaveis table')
    const Responsavel = document.querySelector('#InputModalResponsaveis').value

    $(function (){
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/locais/createresponsavel/",
            data:{
                Responsavel: Responsavel,
                CodLocal: CodLocal
            },
            success: function (dados){
                // console.log(dados)
                modal.innerHTML += `
                    <tr id="linha_responsavel${dados.idResponsavel}">
                        <th>${dados.idResponsavel}</th>
                        <th>${dados.responsavel}</th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('responsavel', ${dados.idResponsavel}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `;
            }

        })

    })

}

// ----- // Emails

buttons.readEmails.onclick = async () => {
    
    const modalhtml = "#ModalEmails table";
    const modal = document.querySelector('#ModalEmails table')

    modal.innerHTML = `
        <tr>
            <td scope="col">Id</td>
            <td scope="col">Email</td>
            <td scope="col">&ensp;</td>
        </tr>
        `
    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/emails/",
            data: {CodLocal: CodLocal},
            success: function (dados){
                // console.log(dados)
                $(dados).each(function (i){
                    // console.log(dados[i])
                    modal.innerHTML += `
                    <tr id="linha_email${dados[i].idEmail}">
                        <th>${dados[i].idEmail}</th>
                        <th>${dados[i].email}</th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('email', ${dados[i].idEmail}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `
                })
                
            }

        })

    })

}

buttons.createEmail.onclick = async () => {

    const modalhtml = "#ModalEmails table";
    const modal = document.querySelector('#ModalEmails table')
    const Email = document.querySelector('#InputModalEmails').value

    $(function (){
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/locais/createemail/",
            data:{
                Email: Email,
                CodLocal: CodLocal
            },
            success: function (dados){
                // console.log(dados)
                modal.innerHTML += `
                    <tr id="linha_email${dados.idEmail}">
                        <th>${dados.idEmail}</th>
                        <th>${dados.email}</th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('email', ${dados.idEmail}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `
            }

        })

    })

}

// ----- // Telefones

buttons.readTelefones.onclick = async () => {
    
    const modalhtml = "#ModalTelefones table";
    const modal = document.querySelector('#ModalTelefones table')

    modal.innerHTML = `
        <tr>
            <td scope="col">Id</td>
            <td scope="col">Telefone</td>
            <td scope="col">&ensp;</td>
        </tr>
        `
    $(function (){
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/locais/telefones/",
            data: {CodLocal: CodLocal},
            success: function (dados){
                // console.log(dados)
                $(dados).each(function (i){
                    // console.log(dados[i])
                    modal.innerHTML += `
                    <tr id="linha_telefone${dados[i].idTelefone}">
                        <th>${dados[i].idTelefone}</th>
                        <th class="Telefone"><input type="text" class="no-border Telefone" value="${dados[i].telefone}" readonly="readonly"></th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('telefone', ${dados[i].idTelefone}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `
                })
                $('.Telefone').mask('(99) 99999-9999');
            }

        })

    })

}

buttons.createTelefone.onclick = async () => {

    const modalhtml = "#ModalTelefones table";
    const modal = document.querySelector('#ModalTelefones table')
    const Telefone = document.querySelector('#InputModalTelefones').value

    $(function (){
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/locais/createtelefone/",
            data:{
                Telefone: Telefone,
                CodLocal: CodLocal
            },
            success: function (dados){
                // console.log(dados)
                modal.innerHTML += `
                    <tr id="linha_telefone${dados.idTelefone}">
                        <th>${dados.idTelefone}</th>
                        <th class="TELEFONE"><input class= "no-border" value="${dados.telefone}"></th>
                        <th class="text-end"><button class="btn btn-danger btn-sm" type="button" onclick="deletar('telefone', ${dados.idTelefone}, '${modalhtml}')">Excluir</button></th>
                    </tr>
                    `
            }

        })

    })

}

// ----- // Deletar

function deletar(tabela, id, modalhtml){

    const modal = document.querySelector(modalhtml)

    // console.log(tabela, id)
    $.ajax({
        dataType: "json",
        type: "POST",
        url: `/locais/delete${tabela}/?Id${tabela}=${id}`,
        success: function (idReturn){ // retorna o ID aqui do item que foi excluido pls

            // console.log(idResponsavel)
            const linha = modal.querySelector(`#linha_${tabela}${id}`)
            // linha.style.display = 'none'
            linha.remove() // Isso deveria excluir o elemento html, mas se não funcionar, descomenta a linha de cima :)

        }
    })

}