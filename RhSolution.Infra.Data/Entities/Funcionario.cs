using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RhSolution.Infra.Data.Entities
{
    public class Funcionario
    {
        #region Atributos
        private string _nome;
        private string _email;
        
        #endregion

        public string Nome
        {

            get => _nome;
            set 
            { 
                var regex = new Regex("^[A-Za-zÀ-Üà-ü\\s]{3,100}$");
                if (!regex.IsMatch(value))
                throw new ValidationException ("Nome do funcionário inválido.");
                _nome = value;
            } 
        }

        public string Email
        {
            get => _email;
            set
            {
                try
                {
                    var addr = new MailAddress(value);
                    _email = addr.Address;
                }
                catch
                {
                    throw new ValidationException("Email do funcionário inválido.");
                }
            }
        }

        public string? Telefone { get; set; }
        public decimal? ValorHora { get; set; }
        public string? Classificacao { get; set; }
        public string? Cargo { get; set; }
        public object? Departamento { get; set; }
        public int Id { get; set; }
    }
}
