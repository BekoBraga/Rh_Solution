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
        private readonly string _connectionString;

        public FuncionarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Funcionario obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("SPI_FUNCIONARIO", new
                {
                    @NOME = obj.Nome,
                    @EMAIL = obj.Email,
                    @TELEFONE = obj.Telefone,
                    @VALORHORA = obj.ValorHora,
                    @CLASSIFICACAO = obj.Classificacao,
                    @CARGO = obj.Cargo,
                },
                commandType: System.Data.CommandType.StoredProcedure);
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
                // Log da exceção (exemplo: Serilog, NLog)
                throw new Exception("Erro ao buscar funcionário por nome.", ex);
            }
        }

        // Método GetByName que retorna uma lista de funcionários ativos
        public List<Funcionario> GetByNameList(string nome)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return connection.Query<Funcionario>(
                        "SPC_CONSULTAR_FUNCIONARIO",
                        new { Nome = nome, Ativo = 1 },
                        commandType: CommandType.StoredProcedure
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log da exceção
                throw new Exception("Erro ao buscar lista de funcionários por nome.", ex);
            }
        }



        public void Update(Funcionario obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("SPU_FUNCIONARIO", new
                {
                    @NOME = obj.Nome,
                    @NOVO_EMAIL = obj.Email,
                    @NOVO_TELEFONE = obj.Telefone,
                    @NOVO_VALORHORA = obj.ValorHora,
                    @NOVA_CLASSIFICACAO = obj.Classificacao,
                    @NOVO_CARGO = obj.Cargo,
                },
                commandType: System.Data.CommandType.StoredProcedure);
            }
        } 

        public void Delete(Funcionario obj)
        {
            // Implementação de deleção
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
                return connection.QuerySingleOrDefault<Funcionario>("SELECT * FROM Funcionario WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Funcionario> GetByNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
