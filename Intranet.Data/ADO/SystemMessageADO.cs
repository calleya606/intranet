using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.ADO
{
    public class SystemMessageADO
    {
        public string GetMessage()
        {
            IntranetEntities db = new IntranetEntities();

            if (db.SystemMessage.Count(n => n.Active) > 0)
            {
                return db.SystemMessage.First(n => n.Active).Message;
            }

            return string.Empty;
        }
    }
}
