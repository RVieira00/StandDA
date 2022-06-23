using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class Tipo : Entity
    {
        //Atributos
        public string Nome { get; set; }

        //Referência a Lista de Viaturas
        public List<Viatura> Viaturas { get; set; }

        //Métodos Construtores
        public Tipo() { }

        public Tipo(string n)
        {
            Nome = n;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
