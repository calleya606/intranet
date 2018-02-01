using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.ADO
{
    public class HistoryLoginADO
    {
        public void Log(Entities.HistoryLogin historyLogin)
        {
            IntranetEntities db = new IntranetEntities();
            HistoryLogin historyLoginData = new HistoryLogin();

            historyLoginData.AccessDate = historyLogin.AccessDate;
            historyLoginData.Ip = historyLogin.Ip;
            historyLoginData.UserAgent = historyLogin.UserAgent;
            historyLoginData.UserName = historyLogin.UserName;

            db.AddToHistoryLogin(historyLoginData);
            db.SaveChanges();
        }

        public Entities.HistoryLogin GetLastHistoryLogin(Entities.HistoryLogin historyLogin)
        {
            IntranetEntities db = new IntranetEntities();

            var histories = (from n in db.HistoryLogin
                             where n.UserName == historyLogin.UserName
                             orderby n.AccessDate descending
                             select new { n.Id, n.AccessDate, n.UserName }).Take(2).ToList();

            var count = histories.Count;

            if (count.Equals(0) || count.Equals(1))
            {
                return null;
            }
            else
            {
                var htemp = histories.Last();
                historyLogin.AccessDate = htemp.AccessDate;
                historyLogin.UserName = htemp.UserName;
                historyLogin.TotalAccess = db.HistoryLogin.Count(n => n.UserName == historyLogin.UserName);
                return historyLogin;
            }
        }

        public void LogModule(Entities.User user, string url)
        {
            IntranetEntities db = new IntranetEntities();
            HistoryModules historyModules = new HistoryModules();

            historyModules.UserName = user.UserName;
            historyModules.Url = url;
            historyModules.Date = DateTime.Now;

            db.AddToHistoryModules(historyModules);
            db.SaveChanges();
        }

    }
}
