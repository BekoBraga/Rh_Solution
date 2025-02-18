using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhSolution.Infra.Data.Entities
{
    public class Empresa
    {
        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Municipio { get; set; }
        public string? Uf { get; set; }
        public string? Cep { get; set; }
        public string? NomeContato { get; set;}
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? TipoEmpresa { get; set; }
        public string? Complemento { get; set; }
    }
}
