using Dapper;
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
    public class EmpresaRepository : IEmpresaRepository
    {
        //atributo
        private readonly string _connectionString;

        public EmpresaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Empresa obj)
        {

            //conexão com banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("SPI_EMPRESA ", new
                {
                    @CNPJ = obj.Cnpj,
                    @RAZAOSOCIAL = obj.RazaoSocial,
                    @NOMEFANTASIA = obj.NomeFantasia,
                    @LOGRADOURO = obj.Logradouro,
                    @NUMERO = obj.Numero,
                    @BAIRRO = obj.Bairro,
                    @MUNICIPIO = obj.Municipio,
                    @UF = obj.Uf,
                    @CEP = obj.Cep,
                    @NOMECONTATO =  obj.NomeContato,
                    @TELEFONE = obj.Telefone,
                    @EMAIL = obj.Email,
                    @TIPOEMPRESA = obj.TipoEmpresa,
                    @COMPLEMENTO = obj.Complemento,
                    @DEPARTAMENTO = obj.Departamento

                },
                commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Delete(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public List<Empresa> GetAll()
        {
            throw new NotImplementedException();
        }

        public Empresa GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Empresa> GetByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public void Update(Empresa obj)
        {
            throw new NotImplementedException();
        }
    }
}
