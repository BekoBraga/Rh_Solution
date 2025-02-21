using RhSolution.Infra.Data.Entities;
using System.Collections.Generic;

namespace RhSolution.Infra.Data.Interfaces
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        List<Funcionario> GetAll();
        Funcionario GetById(int id);
       
    }
}
