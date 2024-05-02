using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System.Runtime.Intrinsics.X86;

namespace Rh_SolutionWeb.Presentation.Controllers
{
    public class RhFuncionarioController : Controller
    {
        //atributo
        private readonly IFuncionarioRepository _funcionarioRepository;

        public RhFuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public IActionResult CadastroFuncionario()
        {
            return View();
        }
        // POST: Funcionario/Cadastro 
        [HttpPost]
        public IActionResult CadastroFuncionario(RhFuncionarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = new Funcionario
                    {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Cpf = model.Cpf,
                        DataNascimento = model.DataNascimento,
                        Genero = model.Genero,
                        IdDepartamento = model.IdDepartamento,
                        IdCargo = model.IdCargo,
                        Salario = (decimal)model.Salario
                    };

                    _funcionarioRepository.Create(funcionario);
                    TempData["Mensagem"] = $"Funcionário {funcionario.Nome} cadastrado com sucesso.";

                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    TempData["Mensagem"] = ex.Message;
                }
            }
                else
                {
                    TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
                }

            return View(model);
        }

        public IActionResult ConsultaFuncionario()
        {
            return View();
        }

        [HttpPost]//Annotation indica que o método será executado no SUBMIT
        public IActionResult ConsultaFuncionario(RhFuncionarioConsultaViewModel model)
        {
            //verifica se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    var chave = model.Chave;

                    //realizando a consulta de funcionários
                    model.Funcionarios = _funcionarioRepository.GetByNome(chave);

                    //verificando se algum evento foi obtido
                    if (model.Funcionarios.Count > 0)
                    {
                        TempData["MensagemSucesso"] = $"{model.Funcionarios.Count} funcionário(s) obtido(s) para a pesquisa";
                     }
                    else
                    {
                        TempData["MensagemAlerta"]= "Nenhum funcionário foi encontrado para a pesquisa realizada.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }
            return View(model);
        }

        public IActionResult EdicaoFuncionario()
        {
            return View();
        }

        public IActionResult RelatorioFuncionario()
        {
            return View();
        }
    }
}
