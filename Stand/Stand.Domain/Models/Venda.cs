using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class Venda : Entity
    {
        public float Preco { get; set; }
        public DateTime data { get; set; }

        public int ViaturaId { get; set; }
        public Viatura Viatura { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Venda() { }

        public Venda(Cliente c, Viatura v, float p)
        {
            Cliente = c;
            Viatura = v;
            Preco = p;
            data = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return "\n -------- Venda " + base.Id + " --------\n\n -> Cliente " + Cliente.Nome + "\n -> Viatura " + Viatura.Id + " - " + Viatura.Marca + " " + Viatura.Modelo + " " + Viatura.Matricula;
        }
    }

}
