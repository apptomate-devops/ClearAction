using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClearAction.Modules.Scheduler.Entity;
namespace ClearAction.Modules.Scheduler.Components
{
    public class DbController
    {
        #region "Email Scheduler"

        public EmailScheduler AddEmail(EmailScheduler emailSchedular)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var odex = ctx.GetRepository<EmailScheduler>();
                    odex.Insert(emailSchedular);


                }
            }
            catch (Exception ex)
            {

                // throw;
            }

            return emailSchedular;
        }

        public List<EmailScheduler> GetUnSendEmail()
        {
            IQueryable<EmailScheduler> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<EmailScheduler>();
                t = rep.Find("Where IsSent=@0", false).AsQueryable();
            }
            return t.OrderBy(x => x.EmailSchedulerId).ToList();
        }
        public void UpdateEmailSchedular(EmailScheduler oemail)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<EmailScheduler>();
                rep.Update(oemail);
            }
        }

        #endregion
    }
}