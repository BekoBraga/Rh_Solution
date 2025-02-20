using Microsoft.AspNetCore.Mvc;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;


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
                TempData["Mensagem"] = $"Erro: {ex.Message}";
            }
        }
        return View(model);
    }

    public IActionResult ConsultaFuncionario()
    {
        return View(new FuncionarioConsultaViewModel());
    }

    [HttpPost]
    public IActionResult ConsultaFuncionario(FuncionarioConsultaViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                List<Funcionario> funcionarios = new List<Funcionario>();

                if (string.IsNullOrEmpty(model.Nome))
                {
                    // Se o Nome estiver vazio, traz todos os funcionários
                    funcionarios = _funcionarioRepository.GetAll(); // Isso deve retornar uma lista de funcionários
                }
                else
                {
                    // Caso contrário, traz apenas os funcionários com o nome informado
                    var funcionario = _funcionarioRepository.GetByName(model.Nome);
                    if (funcionario != null)
                    {
                        funcionarios.Add(funcionario);
                    }
                }

                // Inicializa a propriedade Resultado no modelo
                model.Resultado = funcionarios;

                if (!model.Resultado.Any())
                {
                    TempData["Mensagem"] = "Nenhum funcionário encontrado.";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = $"Erro: {ex.GetType().Name} - {ex.Message}";
            }
        }
        return View(model);
    }



    public IActionResult EdicaoFuncionario(int id)
    {
        var funcionario = _funcionarioRepository.GetById(id);  // Altere para pegar pelo ID

        if (funcionario == null)
            return NotFound(); // Retorna erro 404 caso o funcionário não exista

        var viewModel = new FuncionarioEdicaoViewModel
        {
            Id = funcionario.Id,  // Passa o ID para a View
            Nome = funcionario.Nome,
            Email = funcionario.Email,
            Telefone = funcionario.Telefone,
            Departamento = funcionario.Departamento,
            Cargo = funcionario.Cargo,
            ValorHora = funcionario.ValorHora,
            Classificacao = funcionario.Classificacao,
            Resultado = new List<Funcionario>() // Inicializa a lista como vazia, se necessário
        };

        return View(viewModel);
    }


    [HttpPost]
    public IActionResult SalvarEdicaoFuncionario(FuncionarioEdicaoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);  // Retorna à view se houver erro de validação
        }

        // Mapeia o ViewModel para o objeto de entidade (Funcionario)
        var funcionario = new Funcionario
        {
            Nome = model.Nome,
            Email = model.Email,
            Telefone = model.Telefone,
            ValorHora = model.ValorHora,
            Classificacao = model.Classificacao,
            Cargo = model.Cargo
        };

        // Chama o método de atualização no repositório
        _funcionarioRepository.Update(funcionario);

        // Redireciona para a página de listagem ou qualquer outra página após a atualização
        return RedirectToAction("Index");  // Ou outra ação, conforme sua lógica
    }

    public IActionResult RelatorioFuncionario()
    {
        return View();
    }
}
