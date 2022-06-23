using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class Extra : Entity
    {
        //Atributo
        public string Nome { get; set; }

        //Referência a Lista de ViaturasEstras
        public List<ViaturaExtra> ViaturasExtras { get; set; }

        //Métodos Construtores
        public Extra() { }

        public Extra(string n)
        {
            Nome = n;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
