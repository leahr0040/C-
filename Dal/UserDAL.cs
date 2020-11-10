using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserDAL
    {
        public static int AddUser(User u)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                u.status = true;
                db.Users.Add(u);
                db.SaveChanges();
                return db.Users.Max(i => i.UserID);
            }
            return 0;
        }



    }
}

