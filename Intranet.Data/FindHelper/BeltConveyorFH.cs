using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.FindHelper
{
    public class BeltConveyorFH
    {
        public Intranet.Data.Entities.User User { get; set; }
        public int PageNumber { get; set; }
        public int TotalNumberRegisterPage { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime LastDate { get; set; }
    }
}
