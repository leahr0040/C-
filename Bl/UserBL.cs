using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;
using System.Diagnostics;

namespace Bl
{
    public class UserBL
    {
        public static bool AddUser(UserDTO ud)
        {
            try { 
            Random rand = new Random();//הגרלה לא תקינה
            int i = rand.Next(100000, 999999);
            User u = UserDTO.ToDal(ud);
            if (ud.Email != null)
            {
                    if(ud.UserName==null || ud.UserName == "")
                ud.UserName = ud.Email;
                    if (ud.Password == null || ud.Password == "")
                        ud.Password = ud.Email.Substring(0, 2) + i.ToString();
                
            }//יותר לפי firstname
            else
            {
                int x = rand.Next(1000000, 9999999);

                ud.UserName = x.ToString();
                ud.Password = i.ToString();
            }
           
            // else
            //sms
            int id = UserDAL.AddUser(u);
                if (id != 0)
                {
                    if (ud.Email != null && ud.Email != "")
                        Mailsend.Mailnewuser(ud);

                    if (ud.Dock != null)
                    {
                        DocumentBL.AddUserDocuments(new DocumentDTO(id, ud.Dock, 3, ud.DocName));

                    }
                    return true;
                }
                else
                    return false;
            }
            catch(Exception e)
            {
                Trace.TraceInformation("AddUserblEror " + e.Message);
                return false;
            }
            
        }
        public static bool DeleteUser(int id)
        {
            try
            {
                return UserDAL.DeleteUser(id);

            }
            catch (Exception e)
            {
                Trace.TraceInformation("DeleteUserblEror " + e.Message);
                return false;
            }
        }

        public static List<PropertyDTO> Return_Details_user(string userName, string password)
        {
            try
            {

                int u = UserDAL.Return_Details_user(userName, password);
                if (u != 0)
                    return RenterBL.getPropertiesbyRenterID(u);
                return null;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("detailsblUsers " + e.Message);
                return null;
            }
        }

        public static bool UpdatePassword(UserDTO ud)//שינוי סיסמה 
        {
            try
            {

                Random rand = new Random();//הגרלה לא תקינה
                string userName = "";
                string password = "";
                int i = rand.Next(100000, 999999);
                //User u = UserDTO.ToDal(ud);
                if (ud.Email != null)
                {
                    userName = ud.Email;
                    password = ud.Email.Substring(0, 2) + i.ToString();
                }//יותר לפי firstname
                else
                {
                    userName = ud.FirstName.Substring(1, 4) + ud.LastName.Substring(1, 4);
                    password = ud.UserName.Substring(2, 5) + i.ToString();
                }
                User u = UserDAL.UpdatePassword(ud.UserID, userName, password);
                if (u == null)
                    return false;
                Mailsend.Mailnewuser(new UserDTO(u));
                return true;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updatepasswordUsersEror " + e.Message);
                return false;
            }
        }


        public static bool UpdateUser(UserDTO ud)
        {
            try
            {
                User u = UserDAL.UpdateUser(UserDTO.ToDal(ud));
                if (u == null)
                    return false;
                if (u.Email != ud.Email || u.UserName != ud.UserName || u.Password != ud.Password)
                    Mailsend.Mailnewuser(new UserDTO(u));
                return true;

            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdateUsers " + e.Message);
                return false;
            }
        }
        public static List<UserDTO> GetAllRenters()
        {
            try
            {
                 List<User> renters =UserDAL.GetAllRenters();
                    return UserDTO.ConvertListToDTO(renters);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllblRenrers " + e.Message);
                return null;
            }
        }
        //public static List<PropertyDTO> Return_Details_use(string userNam,string Passwor)
        //{
        //    int u = (from a in db.Users where userNam == a.UserName && Passwor == a.Password select a.UserID).FirstOrDefault();
        //    return RenterBL.getPropertiesbyRenterID(u);
        //}
        public static UserDTO Haveuserforpassword(string userName, string password)
        {
            try
            {
                  getAllUsers_Result u = UserDAL.Haveuserforpassword(userName,password);
                    if (u != null)
                        return new UserDTO(u);
                    return null;
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("haveuserFropasswordUsersblErr " + e.Message);
                return null;
            }
        }
        public static bool MailToAllUser()
        {
            try
            {
                List<UserDTO> u = GetAllRenters();
                foreach (UserDTO user in u)
                {
                    if (user.Email != "" && user.Email != null && user.status == true)
                        Mailsend.Mailnewuser(user);
                }

                return true;

            }
            catch (Exception e)
            {
                Trace.TraceInformation("mailforAllUsers " + e.Message);
                return false;
            }
        }
        public static bool Forgotpassword(string username, string mail)
        {
            //פונקצייה חיפוש עפי מייל קיים
            //Mailsend.Mailforgotpasword()
            try
            {
                getAllUsers_Result u = UserDAL.Forgotpassword(username,mail);


                if (u == null)
                    return false;
                       
                        Mailsend.Mailforgotpasword(u);
                    return true;

            }
            catch (Exception e)
            {
                Trace.TraceInformation("forgotPasswordUsers " + e.Message);
                return false;
            }
        }


    }
}
