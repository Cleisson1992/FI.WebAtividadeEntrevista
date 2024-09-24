$(document).ready(function () {

    $('#CPFBeneficiario').mask('000.000.000-00', { reverse: true });

    $('#btnSalvarBeneficiario').on('click', function () {
        const cpf = $('#CPFBeneficiario').val();
        const nome = $('#NomeBeneficiario').val();

        if (!isValidCPF(cpf)) {
            alert('CPF inválido.');
            return;
        }

        if (cpfExists(cpf)) {
            alert('Este CPF já foi incluído.');
            return;
        }

        if (adicionarLinhaBeneficiario(cpf, nome)) {
            $('#formBeneficiario')[0].reset();
        } else {
            alert('Erro ao incluir o beneficiário.'); 
        }
    });


    $(document).on('click', '.btnRemover', function () {
        $(this).closest('tr').remove();
    });
});

function adicionarLinhaBeneficiario(cpf, nome) {
    const row = `
            <tr>
                <td class="cpf">${cpf}</td>
                <td class="nome">${nome}</td>
                <td>
                    <button class="btn btn-sm btn-primary btnEditar" data-cpf="${cpf}" data-nome="${nome}">Editar</button>
                    <button class="btn btn-sm btn-primary btnRemover">Remover</button>
                </td>
            </tr>`;
    $('#gridBeneficiarios tbody').append(row);
    const lastRow = $('#gridBeneficiarios tbody tr:last');
    return lastRow.length > 0 && lastRow.find('td').eq(0).text() === cpf && lastRow.find('td').eq(1).text() === nome;
}

function cpfExists(cpf) {
    let exists = false;
    $('#gridBeneficiarios tbody tr').each(function () {
        const existingCpf = $(this).find('td.cpf').text();
        if (existingCpf === cpf) {
            exists = true;
            return false; 
        }
    });
    return exists;
}

function isValidCPF(cpf) {

    cpf = cpf.replace(/\D/g, '');

    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) {
        return false;
    }

    let sum = 0;
    for (let i = 0; i < 9; i++) {
        sum += parseInt(cpf[i]) * (10 - i);
    }
    let firstDigit = (sum * 10) % 11;
    if (firstDigit === 10 || firstDigit === 11) {
        firstDigit = 0;
    }
    if (firstDigit !== parseInt(cpf[9])) {
        return false;
    }

    sum = 0;
    for (let i = 0; i < 10; i++) {
        sum += parseInt(cpf[i]) * (11 - i);
    }
    let secondDigit = (sum * 10) % 11;
    if (secondDigit === 10 || secondDigit === 11) {
        secondDigit = 0;
    }
    if (secondDigit !== parseInt(cpf[10])) {
        return false;
    }

    return true;
}