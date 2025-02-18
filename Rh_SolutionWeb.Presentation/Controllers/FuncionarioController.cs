using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;

namespace Rh_SolutionWeb.Presentation.Controllers
{
    public class FuncionarioController : Controller
    {
        //atributo
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public IActionResult CadastroFuncionario()
        {
            return View();
        }
        // POST: Funcionario/Cadastro 
        [HttpPost]
        public IActionResult CadastroFuncionario(FuncionarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = new Funcionario
                    {
                        Nome = model.Nome,
                        Email = model.Email,
                        Telefone = model.Telefone,
                        ValorHora = model.ValorHora,
                        Classificacao = model.Classificacao,
                        Cargo = model.Cargo,
                        
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
            return View(model);
        }

        public IActionResult ConsultaFuncionario()
        {
            return View();
        }

        [HttpPost]//Annotation indica que o método será executado no SUBMIT
        public IActionResult ConsultaFuncionario(FuncionarioConsultaViewModel model)
        {
            //verifica se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
              
            }
            return View();
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
