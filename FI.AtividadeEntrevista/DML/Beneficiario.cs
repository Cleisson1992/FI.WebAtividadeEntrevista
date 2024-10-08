﻿namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// CPF 
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Id do cliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}
