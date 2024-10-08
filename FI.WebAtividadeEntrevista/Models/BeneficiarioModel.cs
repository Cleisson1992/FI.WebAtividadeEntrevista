﻿using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// CPF 
        /// </summary>
        [Required]
        public string CPF { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Id do cliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}