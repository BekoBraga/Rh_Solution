using RhSolution.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhSolution.Infra.Data.Interfaces
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        List<Funcionario> GetByNome(string nome);
    }
}
