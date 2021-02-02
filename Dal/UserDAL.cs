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
        public static bool DeleteUser(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    User t = db.Users.Find(id);
                    t.status = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("DeleteUserEror " + e.Message);
                return false;
            }
        }

        public static int Return_Details_user(string userName, string password)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    int u = (from a in db.Users where userName == a.UserName && password == a.Password select a.UserID).FirstOrDefault();
                    return u;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("detailsUsers " + e.Message);
                return 0;
            }
        }

        public static User UpdatePassword(int UserID, string userName, string password)//שינוי סיסמה 
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    User u = db.Users.Find(UserID);
                    u.UserName = userName;
                    u.Password = password;
                    db.SaveChanges();
                    return u;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updatepasswordUsersEror " + e.Message);
                return null;
            }
        }
        public static User UpdateUser(User ud)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    User u = db.Users.Find(ud.UserID);
                    if ((u.Email != ud.Email && ud.Email.Trim()!="" && ud.Email != null) ||
                        (u.UserName != ud.UserName && ud.UserName.Trim() != "" && ud.UserName != null) ||
                        (u.Password != ud.Password && ud.Password.Trim() != "" && ud.Password != null))
                    {
                        u.Email = ud.Email;
                        u.UserName = ud.UserName;
                        u.Password = ud.Password;
                    }
                    u.FirstName = ud.FirstName;
                    u.LastName = ud.LastName;
                    u.SMS = ud.SMS;
                    u.Phone = ud.Phone;
                    u.RoleID = ud.RoleID;
                    db.SaveChanges();
                    return u;
                    
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdateUsers " + e.Message);
                return null;
            }
        }

        public static List<User> GetAllRenters()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<User> renters = (from r in db.Users where r.status == true select r).ToList();
                    return renters;

                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllRenrersErr " + e.Message);
                return null;
            }
        }
        public static getAllUsers_Result Haveuserforpassword(string userName, string password)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {

                    getAllUsers_Result u = (from a in db.getAllUsers() where userName == a.UserName && password == a.Password select a).FirstOrDefault();
                    return u;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("haveuserFropasswordUsers " + e.Message);
                return null;
            }
        }
        
        public static getAllUsers_Result Forgotpassword(string username, string mail)
        {
            //פונקצייה חיפוש עפי מייל קיים
            //Mailsend.Mailforgotpasword()
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    getAllUsers_Result u = (from a in db.getAllUsers() where username == a.UserName && mail == a.Email select a).FirstOrDefault();
                    if (u != null)
                    {
                        Random rand = new Random();
                        int i = rand.Next(100000, 999999);
                        u.Password = u.UserName.Substring(0, 2) + i.ToString();
                        db.SaveChanges();
                        return u;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("forgotPasswordUsers " + e.Message);
                return null;
            }
        }
    }
}

