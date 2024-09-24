using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal bool Incluir(Beneficiario model)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", model.IdCliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", model.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", model.Nome));

            DataSet ds = base.Consultar("FI_SP_IncluirBeneficiario", parametros);
            long ret = 0;

            if(ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret > 0;
        }

        internal bool VerificarExistencia(string cpf, long id)
        {
            long idRet = 0;

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cpf));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarioId", parametros);

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out idRet);

            return id == idRet;
        }

        internal List<Beneficiario> ListarBeneficiarios(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();

            parameters.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarioCliente", parameters);

            List<Beneficiario> ben = Converter(ds);

            return ben;
        }


        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> listBen = new List<Beneficiario>();

            if(ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario ben = new Beneficiario();

                    ben.Id = row.Field<long>("Id");
                    ben.CPF = row.Field<string>("CPF");
                    ben.Nome = row.Field<string>("Nome");
                    ben.IdCliente = row.Field<long>("IdCliente");

                    listBen.Add(ben);
                }
            }

            return listBen;
        }

        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        internal void Alterar(Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));            
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));            

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }

    }
}
