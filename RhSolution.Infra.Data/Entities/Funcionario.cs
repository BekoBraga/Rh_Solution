using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RhSolution.Infra.Data.Entities
{
    public class Funcionario
    {
        #region Atributos
        private string _nome;
        private string _cpf;
        #endregion

        public string Nome
        {

            get => _nome;
            set 
            { 
                var regex = new Regex("^[A-Za-zÀ-Üà-ü\\s]{3,150}$");
                if (!regex.IsMatch(value))
                throw new ValidationException ("Nome do funcionário inválido.");
                _nome = value;
            } 
        }

        public string Cpf
        {
            get => _cpf;
            set
            {
                var regex = new Regex("^[0-9]{11}$");
                if (!regex.IsMatch(value))
                    throw new ValidationException
                   ("CPF inválido (Informe 11 digitos numéricos).");
                _cpf = value;
            }

        }
        public DateTime DataNascimento { get; set; }
        public string? Genero { get; set; }
        public int IdDepartamento { get; set; }
        public int IdCargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataContratacao { get; set; }
        public string? Chave { get; set; }
        public string? Email { get; set; }
    }
}
