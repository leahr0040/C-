using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class RenterDAL
    {
        public static List<User> Search(string FirstName, string LastName, string SMS, string Email, string Phone, string UserName, string Password)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<User> users = (from u in db.Users where u.UserRole.RoleName=="שוכר" select u).ToList();
                //if (FirstName != null)
                //    users = (from u in users where u.FirstName.Contains( FirstName) select u).ToList();
                //if (LastName != null)
                //    users = (from u in users where u.LastName.Contains(LastName) select u).ToList();
                if (SMS != null)
                    users = (from u in users where u.SMS.Contains(SMS) select u).ToList();
                if (Email != null)
                    users = (from u in users where u.Email.Contains(Email) select u).ToList();
                if (Phone != null)
                    users = (from u in users where u.Phone.Contains(Phone) select u).ToList();
                if (UserName != null)
                    users = (from u in users where u.UserName.Contains(UserName) select u).ToList();
                if (Password != null)
                    users = (from u in users where u.Password.Contains(Password) select u).ToList();

                return users;
            }
            return null;
        }
        public static List<Rental> getRentalsbyRenterID(int id)//פרטי השכרה לפי איידי
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Rental> rentals = (from r in db.Rentals where r.UserID == id select r).ToList();
                return rentals;
            }
            return null;
        }

        public static List<Property> getPropertiesbyRenterID(int id)//דירות ששוכר לפי איידי
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Property> properties = new List<Property>();
                foreach (Rental r in getRentalsbyRenterID(id))
                    properties.Add((from p in db.Properties where r.PropertyID == p.PropertyID select p).FirstOrDefault());
                return properties;
            }
            return null;
        }
    }
}
