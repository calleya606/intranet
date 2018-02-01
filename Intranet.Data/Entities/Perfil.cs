using Intranet.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.Entities
{
    [Serializable]
    public class Perfil : IEntidade
    {
        public Perfil()
        {
            this.PerfilIds = new List<int>();
        }

        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public List<Int32> PerfilIds { get; set; }
    }
}
