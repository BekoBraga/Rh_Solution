using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rh_SolutionWeb.Presentation.Models;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;

public class EmpresaController : Controller
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly HttpClient _httpClient;

    public EmpresaController(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _httpClient = new HttpClient();
    }

    public IActionResult CadastroEmpresa()
    {
        return View(new EmpresaCadastroViewModel());
    }

    [HttpPost]
    public IActionResult CadastroEmpresa(EmpresaCadastroViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            var empresa = new Empresa
            {
                Cnpj = model.Cnpj,
                TipoEmpresa = model.TipoEmpresa,
                RazaoSocial = model.RazaoSocial,
                NomeFantasia = model.NomeFantasia,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Numero = model.Numero,
                Bairro = model.Bairro,
                Municipio = model.Municipio,
                Uf = model.Uf,
                Cep = model.Cep,
                NomeContato = model.NomeContato,
                Telefone = model.Telefone,
                Email = model.Email,
                Departamento = model.Departamento
            };

            _empresaRepository.Create(empresa);
            TempData["Mensagem"] = $"Empresa {empresa.NomeFantasia} cadastrada com sucesso.";
            ModelState.Clear();
        }
        catch (Exception ex)
        {
            TempData["Mensagem"] = ex.Message;
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> BuscarEmpresaPorCnpj(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj)) return BadRequest("CNPJ inválido.");

        try
        {
            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cnpj/v1/{cnpj}");
            if (!response.IsSuccessStatusCode) return NotFound("Empresa não encontrada.");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var empresaData = JsonConvert.DeserializeObject<EmpresaCadastroViewModel>(jsonResponse);
            return Json(empresaData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar dados da empresa: {ex.Message}");
        }
    }
}
