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
        private object nome;

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
        public List<Funcionario> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Funcionario>(
                    "SPC_CONSULTAR_FUNCIONARIO",
                    new { Nome = (string)null },  // Passa NULL para trazer todos os funcionários
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        public List<Funcionario> GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<Funcionario>(
                    "SPC_CONSULTAR_FUNCIONARIO",
                    new { Nome = name.Trim() },  // Corrigido para usar o parâmetro correto
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Log para verificar o resultado
                Console.WriteLine($"Resultado encontrado: {result.Count} funcionários");

                return result;
            }
        }


        public Funcionario GetById(int id)
        {
            throw new NotImplementedException();
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
    }
}
