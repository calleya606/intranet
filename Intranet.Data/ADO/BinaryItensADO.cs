using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.ADO
{
    public class BinaryItensADO
    {
        public BinaryItensADO()
        {

        }

        public void Salvar(Interface.IEntidade entidade, Entities.User user)
        {
            IntranetEntities db = new IntranetEntities();
            Data.Helpers.SerializationHelper h = new Data.Helpers.SerializationHelper();
            BinaryItens binaryItem = new BinaryItens();
            bool update = false;

            var loadEntity = db.BinaryItens.Where(n => n.Id == entidade.Id);
            if (loadEntity.Count() > 0)
            {
                binaryItem = loadEntity.First();
                update = true;
            }

            byte[] bytes = h.Serialize2Bytes(entidade);
            binaryItem.Binary = bytes;
            binaryItem.Type = entidade.GetType().ToString();
            binaryItem.UsuarioUpdate = user.UserName;
            binaryItem.DataAtualizado = DateTime.Now;

            if (!update)
            {
                binaryItem.DataInserido = DateTime.Now;
                binaryItem.UsuarioInsert = user.UserName;
                db.AddToBinaryItens(binaryItem);
            }

            db.SaveChanges();
        }

        public void SalvarUsuario(Data.Entities.User entidade, Entities.User user)
        {
            IntranetEntities db = new IntranetEntities();
            Data.Helpers.SerializationHelper h = new Data.Helpers.SerializationHelper();
            BinaryItens binaryItem = new BinaryItens();
            bool update = false;

            var usuarioTemp = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.User>().Where(n => n.UserName == entidade.UserName).ToList();
            Int32 idTemp = 0;
            if (usuarioTemp.Count > 0)
            {
                idTemp = usuarioTemp.First().Id;
            }

            var loadEntity = db.BinaryItens.Where(n => n.Id == idTemp);
            if (loadEntity.Count() > 0)
            {
                binaryItem = loadEntity.First();
                update = true;
            }

            byte[] bytes = h.Serialize2Bytes(entidade);
            binaryItem.Binary = bytes;
            binaryItem.Type = entidade.GetType().ToString();
            binaryItem.UsuarioUpdate = user.UserName;
            binaryItem.DataAtualizado = DateTime.Now;

            if (!update)
            {
                binaryItem.DataInserido = DateTime.Now;
                binaryItem.UsuarioInsert = user.UserName;
                db.AddToBinaryItens(binaryItem);
            }

            db.SaveChanges();
        }

        public List<T> CarregarTudo<T>()
        {
            IntranetEntities db = new IntranetEntities();
            Data.Helpers.SerializationHelper h = new Data.Helpers.SerializationHelper();

            List<T> lista = new List<T>();

            string tipo = typeof(T).ToString();

            foreach (var item in db.BinaryItens.Where(n => n.Type == tipo && n.Excluido == false))
            {
                Interface.IEntidade entidade = h.DeserializeFromBytes<T>(item.Binary) as Interface.IEntidade;
                entidade.Id = item.Id;
                lista.Add((T)entidade);
            }

            return lista;
        }

        public T Carregar<T>(int id)
        {
            IntranetEntities db = new IntranetEntities();
            Data.Helpers.SerializationHelper h = new Data.Helpers.SerializationHelper();

            foreach (var item in db.BinaryItens.Where(n => n.Id == id))
            {
                Interface.IEntidade entidade = h.DeserializeFromBytes<T>(item.Binary) as Interface.IEntidade;
                entidade.Id = item.Id;
                return (T)entidade;
            }

            return default(T);
        }

        public bool Delete(int id)
        {
            IntranetEntities db = new IntranetEntities();
            Data.Helpers.SerializationHelper h = new Data.Helpers.SerializationHelper();

            BinaryItens bi = null;

            foreach (var item in db.BinaryItens.Where(n => n.Id == id))
            {
                bi = item;
            }
            try
            {
                if (bi != null)
                {
                    db.DeleteObject(bi);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
