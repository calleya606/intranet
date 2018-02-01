using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.Entities
{
    public class Menu
    {
        public Menu()
        {
            this.Menus = new List<Menu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string ControladorNome { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
