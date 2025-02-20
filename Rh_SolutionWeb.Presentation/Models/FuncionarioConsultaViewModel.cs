using RhSolution.Infra.Data.Entities;
using System.Collections.Generic;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class FuncionarioConsultaViewModel
    {
        public string? Nome { get; set; }

        // Adicione a propriedade Resultado com o tipo List<Funcionario>
        public List<Funcionario> Resultado { get; set; } = new List<Funcionario>();

    }
}
