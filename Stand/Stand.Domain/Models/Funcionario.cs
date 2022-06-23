using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Stand.Domain.Models
{
    public class Funcionario : Entity
    {
        public string Email { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);

                password = BitConverter.ToString(hashData).Replace("-", "").ToUpper();
            }
        }

        public string Nome { get; set; }
        public bool isAdmin { get; set; }

        public List<Venda> Vendas { get; set; }

        public Funcionario(string nome)
        {
            this.Nome = nome;
        }

        public Funcionario()
        {
        }
    }
}
