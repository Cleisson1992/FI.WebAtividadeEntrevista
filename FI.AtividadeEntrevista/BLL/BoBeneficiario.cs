using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;
using System;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoBeneficiario ben = new DaoBeneficiario();
            ben.Excluir(id);
        }

        /// <summary>
        /// Consultar os beneficiários pelo id do cliente
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DaoCliente cli = new DaoCliente();
            DaoBeneficiario ben = new DaoBeneficiario();

            var cliente = cli.Consultar(id);
            cliente.Beneficiarios = ben.ListarBeneficiarios(id);

            return cliente;
        }

        /// <summary>
        /// Alterar um beneficiário
        /// </summary>
        /// <param name = "beneficiario" > Objeto de beneficiário</param>
        public DML.Beneficiario ConsultarBeneficiario(long id)
        {
            DaoBeneficiario ben = new DaoBeneficiario();

            var benefeciario = ben.ObterBeneficiario(id);

            return benefeciario;
        }

        /// <summary>
        /// Atualiza os dados do beneficiário.
        /// </summary>
        /// <param name="beneficiario">Objeto com os dados do beneficiário a serem atualizados.</param>
        public void Alterar(Beneficiario beneficiario)
        {
            if (beneficiario == null)
            {
                throw new ArgumentNullException(nameof(beneficiario), "O beneficiário não pode ser nulo.");
            }

            DaoBeneficiario dao = new DaoBeneficiario();
            dao.Alterar(beneficiario);
        }
    }
}
