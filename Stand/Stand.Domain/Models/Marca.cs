using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class Marca : Entity
    {
        //Atributos
        public string Nome { get; set; }

        //Referência a Lista de Viaturas (Ligação 1- N)
        public List<Viatura> Viaturas { get; set; }

        //Métodos construtores
        public Marca() {}

        public Marca(string n)
        {
            Nome = n;
        }

        public override string ToString()
        {
            return Nome;
        }

    }
}
