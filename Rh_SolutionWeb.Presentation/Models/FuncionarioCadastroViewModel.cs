using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class FuncionarioCadastroViewModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do Funcionário.")]
        public string? Nome { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o Email do Funcionário.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Contato do Funcionário.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Valor da Hora do Funcionário.")]
        public decimal? ValorHora { get; set; }

        [Required(ErrorMessage = "Por favor, informe se é da MMO ou Parceiro.")]
        public string? Classificacao { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Cargo do Funcionário.")]
        public string? Cargo { get; set; }
    }
}
