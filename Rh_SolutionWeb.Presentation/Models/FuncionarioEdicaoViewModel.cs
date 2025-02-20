using RhSolution.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class FuncionarioEdicaoViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; internal set; }
        public object? Departamento { get; internal set; }
        public string? Cargo { get; internal set; }
        public decimal? ValorHora { get; internal set; }
        public string? Classificacao { get; internal set; }
        public List<Funcionario>? Resultado { get; set; }
        
    }
}
