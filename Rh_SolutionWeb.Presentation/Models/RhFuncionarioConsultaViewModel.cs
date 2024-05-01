using System.ComponentModel.DataAnnotations;

namespace Rh_SolutionWeb.Presentation.Models
{
    public class RhFuncionarioConsultaViewModel
    {
        [Required(ErrorMessage ="Por favor, informe o Nome do Fuincionário")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Chave do Fuincionário")]
        public string? Chave { get; set; }
    }
}
