using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Fl.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly BoCliente _clienteService;
        private readonly BoBeneficiario _beneficiarioService;

        public ClienteController()
        {
            _clienteService = new BoCliente();
            _beneficiarioService = new BoBeneficiario();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            if (!ValidarDocumento.ValidarCPF(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Informe um CPF Válido.");
            }

            if (_clienteService.VerificarExistencia(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var cliente = new Cliente();
                cliente.CEP = model.CEP;
                cliente.Cidade = model.Cidade;
                cliente.Email = model.Email;
                cliente.Estado = model.Estado;
                cliente.Logradouro = model.Logradouro;
                cliente.Nacionalidade = model.Nacionalidade;
                cliente.Nome = model.Nome;
                cliente.Sobrenome = model.Sobrenome;
                cliente.Telefone = model.Telefone;
                cliente.CPF = model.CPF;
                cliente.Beneficiarios = new List<Beneficiario>();

                foreach (var beneficiario in model.Beneficiarios)
                {
                    cliente.Beneficiarios.Add(new Beneficiario
                    {
                        Nome = beneficiario.Nome,
                        CPF = beneficiario.CPF
                    });
                }

                model.Id = _clienteService.Incluir(cliente);

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            if (!ValidarDocumento.ValidarCPF(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("CPF inválido.");
            }

            if (_clienteService.VerificarExistencia(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado em outro cliente.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                _clienteService.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF,
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            Cliente cliente = _clienteService.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                };

                foreach (var ben in cliente.Beneficiarios)
                {
                    model.Beneficiarios.Add(new BeneficiarioModel()
                    {
                        Id = ben.Id,
                        CPF = ben.CPF,
                        Nome = ben.Nome,
                        IdCliente = ben.IdCliente
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = _clienteService.Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ExcluirBeneficiario(long id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return Json("Informe o id do beneficiario!");
            }

            _beneficiarioService.Excluir(id);

            return Json("Exclusão realizada com sucesso");
        }
    }
}