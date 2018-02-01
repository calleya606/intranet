using Intranet.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.Entities
{
    [Serializable]
    public class User : IEntidade
    {
        public User()
        {
            this.PerfilIds = new List<Int32>();
            this.Menus = new List<Menu>();
        }

        public Int32 Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Administrator { get; set; }
        public bool Active { get; set; }
        public string UniqueIdentifierLogin { get; set; }
        public Nullable<DateTime> ExpirationDate { get; set; }
        public List<Int32> PerfilIds { get; set; }
        public List<Entities.Menu> Menus { get; set; }
    }
}
