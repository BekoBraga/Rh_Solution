using Dapper;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhSolution.Infra.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo
        private readonly string _connectionString;

        public FuncionarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Funcionario obj)
        {
          
            //conexão com banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("SPI_RH_FUNCIONARIO ", new
                {
                    @NOME = obj.Nome,
                    @SOBRENOME = obj.Sobrenome,
                    @CPF = obj.Cpf,
                    @DT_NASCIMENTO = obj.DataNascimento,
                    @GENERO = obj.Genero,
                    @ID_DEPARTAMENTO = obj.IdDepartamento,
                    @ID_CARGO = obj.IdCargo,
                    @SALARIO_ATUAL = obj.Salario
                },
                commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Update(Funcionario obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Funcionario obj)
        {
            throw new NotImplementedException();
        }

        public List<Funcionario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Funcionario GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Funcionario> GetByNome(string chave)
        {
            List<Funcionario> funcionarios;

            using (var connection = new SqlConnection(_connectionString))
            {
                // Execute a stored procedure to populate the data
                funcionarios = connection.Query<Funcionario>("SPC_FUNCIONARIO", new { @CHAVE = chave }, commandType: CommandType.StoredProcedure).ToList();
            }

            return funcionarios;
        }
    }
}
