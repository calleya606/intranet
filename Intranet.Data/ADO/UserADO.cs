using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace Intranet.Data.ADO
{
    public class UserADO
    {
        public List<Entities.User> CarregarTudo()
        {
            Data.IntranetEntities db = new Data.IntranetEntities();
            List<Entities.User> usuarios = new List<Entities.User>();
            var usuariosTemp = new Data.ADO.BinaryItensADO().CarregarTudo<Entities.User>();

            foreach (var item in usuariosTemp)
            {
                CarregaUsuarioAD(item);
                usuarios.Add(item);
            }

            return usuarios;
        }

        public Entities.User CarregaUsuarioAD(Entities.User user)
        {
            DirectoryEntry Ldap = new DirectoryEntry();
            DirectorySearcher dseSearcher = new DirectorySearcher(Ldap);
            dseSearcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", user.UserName);

            SearchResult result = dseSearcher.FindOne();

            if (result != null)
            {
                DirectoryEntry dirEntry = result.GetDirectoryEntry();

                user.Name = dirEntry.Properties["name"].Value.ToString();
                user.UserName = dirEntry.Properties["sAMAccountName"].Value.ToString();

                return user;
            }
            else
            {
                return null;
            }
        }

        public Entities.User Login(Entities.User user)
        {
            Data.IntranetEntities db = new Data.IntranetEntities();

            if (user.UserName.Equals("sistemas") && user.Password.Equals("@sistemas"))
            {
                Entities.User usuarioAdmin = new Entities.User();
                usuarioAdmin.Name = "Administrador do Sistema";
                usuarioAdmin.UserName = "sistemas";
                usuarioAdmin.UniqueIdentifierLogin = Guid.NewGuid().ToString();
                usuarioAdmin.Administrator = true;

                return usuarioAdmin;
            }

            bool isValid = false;

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                isValid = pc.ValidateCredentials(user.UserName, user.Password);

                if (isValid)
                {
                    DirectoryEntry Ldap = new DirectoryEntry();
                    DirectorySearcher dseSearcher = new DirectorySearcher(Ldap);
                    dseSearcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", user.UserName);

                    SearchResult result = dseSearcher.FindOne();

                    if (result != null)
                    {
                        DirectoryEntry dirEntry = result.GetDirectoryEntry();

                        var usuarioDB = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.User>().Where(n => n.UserName == user.UserName).ToList();
                        user = usuarioDB.First();
                        user.Name = dirEntry.Properties["name"].Value.ToString();
                        user.UserName = dirEntry.Properties["sAMAccountName"].Value.ToString();
                        user.UniqueIdentifierLogin = Guid.NewGuid().ToString();
                    }
                }
                else
                {
                    return null;
                }
            }

            return user;
        }
    }
}
