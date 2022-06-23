using Stand.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Domain.Models
{
    public class ViaturaExtra : Entity
    {
        public int ViaturaId { get; set; } 
        public Viatura Viatura { get; set; }

        public int ExtraId { get; set; }
        public Extra Extra { get; set; }
    }
}
