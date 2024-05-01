using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;

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
