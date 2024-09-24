
$(document).ready(function () {

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        const dadosFormulario = coletarDadosFormulario();

        $.ajax({
            url: urlPost,
            method: "POST",
            data: dadosFormulario,
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();
                $("#gridBeneficiarios > tbody").empty();
            }
        });
    })  
})

function obterDadosBeneficiarios() {
    let beneficiarios = [];

    $("#gridBeneficiarios > tbody > tr").each(function () {
        let linhaTabela = $(this);

        let cpf = linhaTabela.find(".cpf").text().replace(/\D+/g, '');
        let nome = linhaTabela.find(".nome").text().trim(); 

        if (cpf) {
            beneficiarios.push({ CPF: cpf, NOME: nome });
        }
    });

    return beneficiarios; 
}

function coletarDadosFormulario() {
    return {
        "NOME": $("#Nome").val(),
        "CEP": $("#CEP").val().replace(/\D+/g, ''), 
        "CPF": $("#CPF").val().replace(/\D+/g, ''), 
        "Email": $("#Email").val(),
        "Sobrenome": $("#Sobrenome").val(),
        "Nacionalidade": $("#Nacionalidade").val(),
        "Estado": $("#Estado").val(),
        "Cidade": $("#Cidade").val(),
        "Logradouro": $("#Logradouro").val(),
        "Telefone": $("#Telefone").val().replace(/\D+/g, ''), 
        "Beneficiarios": obterDadosBeneficiarios() 
    };
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
