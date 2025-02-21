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

        public List<Funcionario> GetByNameList(string? nome)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var funcionarios = connection.Query<Funcionario>(
                        "SPC_CONSULTAR_FUNCIONARIO",
                        new { Nome = nome },
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    return funcionarios ?? new List<Funcionario>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar lista de funcionários por nome.", ex);
            }
        }

        public void Update(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Execute(
                    "SPU_FUNCIONARIO",
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
                    // Log ou mensagem de erro, pois nada foi atualizado
                    Console.WriteLine("Nenhum registro foi atualizado.");
                }
                else
                {
                    // Caso contrário, a operação foi bem-sucedida
                    Console.WriteLine($"Número de registros atualizados: {result}");
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
                return connection.Query<Funcionario>("SELECT * FROM Funcionario").ToList();
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
            // Esta implementação ainda não foi definida, se necessário, implemente-a
            throw new NotImplementedException();
        }
    }
}
