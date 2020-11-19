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
            Random rand = new Random();//הגרלה לא תקינה
            int i = rand.Next(100000, 999999);
            User u = UserDTO.ToDal(ud);
            if (ud.Email != null)
            {
                ud.UserName = ud.Email;
                ud.Password = ud.Email.Substring(0, 2) + i.ToString();
            }//יותר לפי firstname
            else
            {
                ud.UserName = ud.FirstName.Substring(1, 4) + ud.LastName.Substring(1, 4);
                ud.Password = ud.UserName.Substring(1, 3) + i.ToString();
            }
            Mailsend.Mailnewuser(ud);
            // else
            //sms
            int id = UserDAL.AddUser(u);
            if (id != 0 && ud.Dock != null)
            {
                Document doc = new Document();
                doc.DocCoding = ud.Dock;
                doc.DocUser = id;
                doc.type = 7;
                doc.DocName = ud.DocName;
                DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                return true;
            }
            return false;
        }
        public static bool DeleteUser(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                User t = db.Users.Find(id);
                t.status = false;

                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<PropertyDTO> Return_Details_user(string userNam, string Passwor)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                int u = (from a in db.Users where userNam == a.UserName && Passwor == a.Password select a.UserID).FirstOrDefault();
                return RenterBL.getPropertiesbyRenterID(u);
            }
        }

        public static bool UpdatePassword(UserDTO ud)//שינוי סיסמה 
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Random rand = new Random();//הגרלה לא תקינה
                int i = rand.Next(100000, 999999);
                //User u = UserDTO.ToDal(ud);
                if (ud.Email != null)
                {
                    ud.UserName = ud.Email;
                    ud.Password = ud.Email.Substring(0, 2) + i.ToString();
                }//יותר לפי firstname
                else
                {
                    ud.UserName = ud.FirstName.Substring(1, 4) + ud.LastName.Substring(1, 4);
                    ud.Password = ud.UserName.Substring(2, 5) + i.ToString();
                }
                Mailsend.Mailnewuser(ud);
                db.SaveChanges();
                return true;
            }
        }


        public static bool UpdateUser(UserDTO ud)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                bool b = false;
                User u = db.Users.Find(ud.UserID);
                if ((u.Email != ud.Email) || (u.UserName != ud.UserName) || (u.Password != ud.Password))
                    b = true;
                if ((ud.UserName == null) || (ud.Password == null) || (ud.UserName == ""))
                {
                    Random rand = new Random();//הגרלה לא תקינה
                    int i = rand.Next(100000, 999999);
                    if (ud.Email != null)
                    {
                        ud.UserName = ud.Email;
                        ud.Password = ud.Email.Substring(0, 2) + i.ToString();
                    }//יותר לפי firstname
                    else
                    {
                        ud.UserName = ud.FirstName.Substring(1, 4) + ud.LastName.Substring(1, 4);
                        ud.Password = ud.UserName.Substring(1, 3) + i.ToString();
                    }
                }
                u.FirstName = ud.FirstName;
                u.LastName = ud.LastName;
                u.SMS = ud.SMS;
                u.Email = ud.Email;
                u.Phone = ud.Phone;
                u.RoleID = ud.RoleID;
                u.UserName = ud.UserName;
                u.Password = ud.Password;
                if (b == true)//עוד בלי אופציית SMS
                    Mailsend.Mailnewuser(ud);
                db.SaveChanges();
                return true;
                //return false;
            }
        }
        public static List<UserDTO> GetAllRenters()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<User> renters = (from r in db.Users where r.status == true select r).ToList();
                return RenterBL.ConvertListToDTO(renters);

            }
        }
        //public static List<PropertyDTO> Return_Details_use(string userNam,string Passwor)
        //{
        //    int u = (from a in db.Users where userNam == a.UserName && Passwor == a.Password select a.UserID).FirstOrDefault();
        //    return RenterBL.getPropertiesbyRenterID(u);
        //}
        public static UserDTO Haveuserforpassword(string userNam, string Passwor)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {

                    User u = (from a in db.Users where userNam == a.UserName && Passwor == a.Password select a).FirstOrDefault();
                    if (u != null)
                        return new UserDTO(u);

                    return null;
                }
            }


            catch { return null; }
        }
        public static bool MailToAllUser()
        {
            try
            {
                List<UserDTO> u = GetAllRenters();
                int x = u.Count;
                int i = 0;

                while (i < x)
                {
                    if (u[i].Email != string.Empty && u[i].Email != null)
                        Mailsend.Mailnewuser(u[i]);
                    i++;
                }
                return true;

            }
            catch { return false; }
        }
        public static bool Forgotpassword(string username, string mail)
        {
            //פונקצייה חיפוש עפי מייל קיים
            //Mailsend.Mailforgotpasword()
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                User u = (from a in db.Users where username == a.UserName && mail == a.Email select a).FirstOrDefault();


                if (u != null)
                {
                    Random rand = new Random();
                    int i = rand.Next(100000, 999999);
                    u.Password = u.UserName.Substring(0, 2) + i.ToString();
                    db.SaveChanges();
                    Mailsend.Mailforgotpasword(u);
                    return true;
                }
                return false;
            }
        }


    }
}
