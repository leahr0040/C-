using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;

namespace Bl
{
    public class UserBL
    {
        public static bool AddUser(UserDTO ud)
        {
            User u = UserDTO.ToDal(ud);
            return UserDAL.AddUser(u);
        }
        public static bool UpdateUser(UserDTO ud)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
               User u = db.Users.Find(ud.UserID);

               
                //u.FirstName = ud.FirstName;
                //u.LastName = ud.LastName;
                u.SMS = ud.SMS;
                u.Email = ud.Email;
                u.Phone = ud.Phone;
                u.RoleID = ud.RoleID;
                u.UserName = ud.UserName;
                u.Password = ud.Password;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
