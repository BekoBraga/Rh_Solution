using RhSolution.Infra.Data.Entities;
using System.Collections.Generic;

namespace RhSolution.Infra.Data.Interfaces
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        // Método para buscar um funcionário pelo nome
        Funcionario? GetByName(string nome); // Retorna um único funcionário ou null caso não exista

        // Método para buscar uma lista de funcionários pelo nome
        List<Funcionario> GetByNome(string nome); // Pode ser por nome completo ou parte do nome
        
    }
}
