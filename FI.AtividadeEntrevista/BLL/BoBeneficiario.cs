using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DAL.Clientes;

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
    }
}
