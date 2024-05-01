using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class RhFuncionarioCadastroViewModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do Funcionário.")]
        public string? Nome { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o Sobrenome do Funcionário.")]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento do Funcionário.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Cpf do Funcionário.")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Departamento do Funcionário.")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Cargo do Funcionário.")]
        public int IdCargo { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Gênero do Funcionário.")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Salário do Funcionário.")]
        public decimal? Salario { get; set; }
    }
}
