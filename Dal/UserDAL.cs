using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserDAL
    {
        public static bool AddUser(User u)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Users.Add(u);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        
    }
}
