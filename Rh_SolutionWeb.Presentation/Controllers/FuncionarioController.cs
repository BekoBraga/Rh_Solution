
using Microsoft.AspNetCore.Mvc;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System.Diagnostics;

namespace Rh_SolutionWeb.Presentation.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public IActionResult CadastroFuncionario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroFuncionario(FuncionarioCadastroViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

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
                TempData["Mensagem"] = $"Erro: {ex.Message}";
            }
            return View(model);
        }

        public IActionResult ConsultaFuncionario()
        {
            var model = new FuncionarioConsultaViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ConsultaFuncionario(FuncionarioConsultaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                model.Resultado = string.IsNullOrEmpty(model.Nome)
                    ? _funcionarioRepository.GetAll()  // Retorna todos os funcionários se o nome estiver vazio
                    : _funcionarioRepository.GetByName(model.Nome);  // Filtra pelo nome se preenchido

                if (!model.Resultado.Any())
                    TempData["Mensagem"] = "Nenhum funcionário encontrado.";
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = $"Erro: {ex.GetType().Name} - {ex.Message}";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EdicaoFuncionario(int id)
        {
            var funcionario = _funcionarioRepository.GetById(id);

            if (funcionario == null)
            {
                return RedirectToAction("ConsultaFuncionario");
            }

            var viewModel = new FuncionarioEdicaoViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
                ValorHora = funcionario.ValorHora,
                Classificacao = funcionario.Classificacao
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult SalvarEdicaoFuncionario(FuncionarioEdicaoViewModel model)
        {
            Debug.WriteLine("Chegou no método SalvarEdicaoFuncionario");

            if (!ModelState.IsValid) return View(model);

            try
            {
                var funcionario = new Funcionario
                {
                    Id = (int)model.Id,
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    ValorHora = model.ValorHora,
                    Classificacao = model.Classificacao,
                    Cargo = model.Cargo
                };

                _funcionarioRepository.Update(funcionario);
                TempData["Mensagem"] = "Funcionário atualizado com sucesso!";
                return RedirectToAction("ConsultaFuncionario");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Mensagem"] = ex.Message; // Exibe a mensagem específica do repositório
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = $"Erro ao atualizar: {ex.Message}";
            }

            return View(model);
        }

    }
}