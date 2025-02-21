using Dapper;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RhSolution.Infra.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _connectionString;

        public FuncionarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "SPI_FUNCIONARIO",
                    new
                    {
                        @NOME = funcionario.Nome,
                        @EMAIL = funcionario.Email,
                        @TELEFONE = funcionario.Telefone,
                        @VALORHORA = funcionario.ValorHora,
                        @CLASSIFICACAO = funcionario.Classificacao,
                        @CARGO = funcionario.Cargo,
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public Funcionario? GetByName(string nome)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return connection.QueryFirstOrDefault<Funcionario>(
                        "SPC_CONSULTAR_FUNCIONARIO",
                        new { Nome = nome },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                // Aqui seria adequado adicionar log da exceção (ex: Serilog, NLog)
                throw new Exception("Erro ao buscar funcionário por nome.", ex);
            }
        }


        public void Update(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    // Chamando a stored procedure para atualizar
                    var result = connection.Execute(
                        "SPU_FUNCIONARIO",  // Nome da stored procedure
                        new
                        {
                            @FUNCIONARIO_ID = funcionario.Id,
                            @NOVO_EMAIL = funcionario.Email,
                            @NOVO_TELEFONE = funcionario.Telefone,
                            @NOVO_VALORHORA = funcionario.ValorHora,
                            @NOVA_CLASSIFICACAO = funcionario.Classificacao,
                            @NOVO_CARGO = funcionario.Cargo,
                        },
                        commandType: CommandType.StoredProcedure
                    );

                    if (result == 0)
                    {
                        Console.WriteLine("Nenhum registro foi atualizado.");
                    }
                    else
                    {
                        Console.WriteLine($"Número de registros atualizados: {result}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao atualizar funcionário: {ex.Message}");
                    throw;
                }
            }
        }

        public void Delete(Funcionario funcionario)
        {
            // Implementar a lógica de exclusão do funcionário conforme necessário
        }

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

        public Funcionario GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Funcionario>(
                    "SELECT * FROM Funcionario WHERE Id = @Id",
                    new { Id = id }
                );
            }
        }

        public List<Funcionario> GetByNome(string nome)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<Funcionario>(
                    "SPC_CONSULTAR_FUNCIONARIO",
                    new { Nome = nome.Trim() },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Log para ver o resultado
                Console.WriteLine($"Resultado encontrado: {result.Count} funcionários");

                return result;
            }
        }

    }
}
