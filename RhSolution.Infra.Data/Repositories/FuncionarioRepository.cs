﻿using Dapper;
using RhSolution.Infra.Data.Entities;
using RhSolution.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
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
                connection.Execute("SPI_FUNCIONARIO ", new
                {
                    @NOME           = obj.Nome,
                    @EMAIL          = obj.Email,
                    @TELEFONE       = obj.Telefone,
                    @VALORHORA      = obj.ValorHora,
                    @MMOPARCEIRO    = obj.Classificacao,
                    @CARGO          = obj.Cargo,
                    
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

        public List<Funcionario> GetByNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
