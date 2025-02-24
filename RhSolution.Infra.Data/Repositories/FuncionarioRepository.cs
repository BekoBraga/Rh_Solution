using Dapper;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RhSolution.Infra.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _connectionString;

        public FuncionarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Implementação do Create (Insert)
        public void Create(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    funcionario.Nome,
                    funcionario.Email,
                    funcionario.Telefone,
                    funcionario.ValorHora,
                    funcionario.Classificacao,
                    funcionario.Cargo
                };
                connection.Execute("SPI_FUNCIONARIO", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Update(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var funcionarioExistente = connection.QuerySingleOrDefault<Funcionario>(
                    "SELECT Id FROM Funcionario WHERE Id = @Id", new { Id = funcionario.Id });

                if (funcionarioExistente == null)
                {
                    throw new InvalidOperationException($"Funcionario com Id {funcionario.Id} não encontrado.");
                }

                var parameters = new
                {
                    funcionario.Id,
                    funcionario.Email,
                    funcionario.Telefone,
                    funcionario.ValorHora,
                    funcionario.Classificacao,
                    funcionario.Cargo
                };

                try
                {
                    connection.Execute("SPU_FUNCIONARIO", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    // Adicione logs aqui para registrar o erro
                    throw new Exception("Erro ao atualizar o funcionário.", ex);
                }
            }
        }


        // Implementação do GetById
        public Funcionario GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Executa a query diretamente no banco, buscando pelo id
                return connection.QuerySingleOrDefault<Funcionario>(
                    "SELECT *  FROM FUNCIONARIO WHERE ID = @Id",
                    new { Id = id });
            }
        }


        // Implementação do GetAll
        public List<Funcionario> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Retorna todos os funcionários
                return connection.Query<Funcionario>(
                    "SELECT * FROM FUNCIONARIO WHERE ATIVO = 1 ORDER BY NOME")
                    .ToList();
            }
        }

        // Implementação do GetByName
        public List<Funcionario> GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<Funcionario>(
                    "SPC_CONSULTAR_FUNCIONARIO",
                    new { Nome = name },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Log para ver o resultado
                Console.WriteLine($"Resultado encontrado: {result.Count} funcionários");

                return result;
            }
        }

        // Implementação do Delete
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "SPD_FUNCIONARIO",
                    new { Id = id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Delete(Funcionario obj)
        {
            throw new NotImplementedException();
        }
    }
}
