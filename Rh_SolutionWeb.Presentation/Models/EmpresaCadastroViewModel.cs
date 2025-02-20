using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class EmpresaCadastroViewModel
    {
        [Required(ErrorMessage = "Por favor, informe o CNPJ.")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o tipo de empresa.")]
        public string? TipoEmpresa { get; set; } // Cliente ou Fornecedor

        [JsonProperty("razao_social")]
        public string? RazaoSocial { get; set; }

        [JsonProperty("nome_fantasia")]
        public string? NomeFantasia { get; set; }
        public string? Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Municipio { get; set; }
        public string? Uf { get; set; }
        public string? Cep { get; set; }
        public string? NomeContato { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Departamento { get; set; }
    }
}
