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
        private string _sobrenome;
        private string _cpf;
        #endregion

        public string Nome
        {

            get => _nome;
            set 
            { 
                var regex = new Regex("^[A-Za-zÀ-Üà-ü\\s]{3,20}$");
                if (!regex.IsMatch(value))
                throw new ValidationException ("Nome do funcionário inválido.");
                _nome = value;
            } 
        }

        public string Sobrenome
        {
            get => _sobrenome;
            set
            {
                var regex = new Regex("^[A-Za-zÀ-Üà-ü\\s]{3,150}$");
                if (!regex.IsMatch(value))
                    throw new ValidationException("Sobrenome do funcionário inválido.");
                _sobrenome = value;
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
    }
}
