$(function(){
    $('.IE').mask('999.999.999.999');
    $('.CNPJ').mask('99.999.999/9999-99');
    $('.CEP').mask('99999-999');
    $('.CPF').mask('999.999.999-99');
    $('.Telefone').mask('(99) 99999-9999');
    $('.Placa').mask('SSS-00A0');
    $('.Money').mask('#.##0,00', {reverse: true});
});