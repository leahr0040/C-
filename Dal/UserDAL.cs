using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dal
{
    public class UserDAL
    {
        public static int AddUser(User u)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                u.status = true;
                db.Users.Add(u);
                db.SaveChanges();
                return db.Users.Max(i => i.UserID);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("addUserEror " + e.Message);
                return 0;
            }
        }



    }
}

