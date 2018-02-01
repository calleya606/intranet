using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.ADO
{
    public class MenuADO
    {
        private List<Data.Menu> _dbMenus;
        private List<Int32> _menuIds = new List<int>();

        public MenuADO(Entities.User user)
        {
            IntranetEntities db = new IntranetEntities();

            if (user != null && !user.Administrator)
            {
                foreach (var perfilId in user.PerfilIds)
                {
                    var perfil = new Data.ADO.BinaryItensADO().Carregar<Data.Entities.Perfil>(perfilId);
                    _menuIds.AddRange(perfil.PerfilIds);
                }

                _menuIds = _menuIds.Distinct().ToList();
                _dbMenus = (from m in db.Menu
                            from mi in _menuIds
                            where m.Id == mi
                            select m).ToList();
            }
            else
            {
                _dbMenus = (from m in db.Menu
                            select m).ToList();
            }
        }

        public List<Entities.Menu> LoadById(Int32? menuId)
        {
            List<Entities.Menu> menus = new List<Entities.Menu>();

            foreach (var item in _dbMenus.Where(n => n.ParentId == menuId).OrderBy(n => n.MenuOrder))
            {
                menus.Add(new Entities.Menu()
                {
                    Id = item.Id,
                    Href = item.Href,
                    Name = item.Name,
                    ControladorNome = item.ControllerName,
                    Menus = LoadById(item.Id)
                });
            }

            return menus;
        }
    }
}
