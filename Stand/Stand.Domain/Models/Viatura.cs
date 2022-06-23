using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
namespace Stand.Domain.Models
{
    public class Viatura : Entity //Posso tirar o Entity para não ter Id?
    {

        //Atributos
        public string Matricula { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public int Cilindrada { get; set; }
        public int Ano { get; set; }
        public float PrecoBasse { get; set; }
        public byte[] Thumb { get; set; }

        //Relação com Tabelas
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }

        //Relação com a tabela associativa
        public List<ViaturaExtra> ViaturasExtras { get; set; }
    
        public Venda Venda { get; set; }


        public Viatura(string matricula)
        {
            Matricula = matricula;
        }

        public Viatura() { }
    }
}
