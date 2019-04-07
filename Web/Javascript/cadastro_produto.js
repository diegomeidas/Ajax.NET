




function localizaClienteAjax(CPF) {
    var ret;

    $.ajax({
        type: 'GET',
        async: false,
        contentType: 'application/json',
        url: 'controller/cadastro.ashx?metodo=localizacliente&cpf=' + CPF,
        success: (function (obj) {
            ret = TrataRetornoAjax(obj);
            if (ret == true) {
                _idPessoa = obj.IdCliente;
            }
        }),
        error: (function (erro) {
            ExibeMensagemErro('Erro desconhecido');
            ret = false;
        })
    });
    return ret;
}

