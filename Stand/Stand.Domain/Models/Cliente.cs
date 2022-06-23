using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CartaConducao { get; set; }
        public string Telemovel { get; set; }

        public List<Venda> Vendas { get; set; }

        public Cliente() { }

        public Cliente(string n)
        {
            Nome = n;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
