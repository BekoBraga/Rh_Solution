using RhSolution.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class RhFuncionarioConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a Chave do Fuincionário")]
        public string? Chave { get; set; }
        public List<Funcionario>? Funcionarios { get; set; }

        }
}
