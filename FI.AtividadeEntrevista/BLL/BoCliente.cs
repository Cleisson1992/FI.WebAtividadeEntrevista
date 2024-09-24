using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            DaoCliente cli = new DaoCliente();
            DaoBeneficiario ben = new DaoBeneficiario();

            var idCliente = cli.Incluir(cliente);

            foreach (var item in cliente.Beneficiarios)
            {
                item.IdCliente = idCliente;

                if (!ben.VerificarExistencia(item.CPF, idCliente)) ben.Incluir(item);
            }

            return idCliente;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DaoCliente cli = new DaoCliente();
            DaoBeneficiario ben = new DaoBeneficiario();

            foreach (var item in cliente.Beneficiarios)
            {
                item.IdCliente = cliente.Id;

                if (item.Id > 0) ben.Alterar(item);

                else if (!ben.VerificarExistencia(item.CPF, cliente.Id)) ben.Incluir(item);

            }

            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DaoCliente cli = new DaoCliente();
            DaoBeneficiario ben = new DaoBeneficiario();


            var c = cli.Consultar(id);
            c.Beneficiarios = ben.ListarBeneficiarios(id);

            return c;
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoCliente cli = new DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DaoCliente cli = new DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DaoCliente cli = new DaoCliente();
            return cli.VerificarExistencia(CPF);
        }
    }
}
