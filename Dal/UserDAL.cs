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
                return (from us in db.Users where us.FirstName==u.FirstName && us.LastName==us.LastName && us.Email==u.Email && us.SMS==u.SMS && us.UserName==us.UserName select us.UserID).FirstOrDefault();
            }
            return 0;
        }
        //public static bool Searchforuser(string UserName, string Password)
        //{ }
             
        }
        
    }

