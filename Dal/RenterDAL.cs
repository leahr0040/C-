using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class RenterDAL
    {
        public static List<User> Search(string FirstName, string LastName, string SMS, string Email, string Phone)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<User> users = (from u in db.Users where u.UserID==3 && u.status == true select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                if (FirstName != null)
                    users = (from u in users where u.FirstName.Contains(FirstName) select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                if (LastName != null)
                    users = (from u in users where u.LastName.Contains(LastName) select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                if (SMS != null)
                    users = (from u in users where u.SMS.Contains(SMS) select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                if (Email != null)
                    users = (from u in users where u.Email.Contains(Email) select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();
                if (Phone != null)
                    users = (from u in users where u.Phone.Contains(Phone) select u).OrderBy(r => r.FirstName).OrderBy(r => r.LastName).ToList();

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
