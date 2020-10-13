using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class Mailsend
    {
        static ArgamanExpressEntities db = new ArgamanExpressEntities();
        //public static List<Rental> Return_Details_user(string userNam, string Passwor)
        //{
        //    User u = (from a in db.Users where userNam == a.UserName && Passwor == a.Password select a.UserID).FirstOrDefault();
        //  return getRentalsbyOwnerID(u.UserID);
        //}
        public static List<Rental> getRentalsbyOwnerID(int id)//פרטי השכרה לפי id
        {
            List<Rental> rentals = (from r in db.Rentals where r.UserID == id select r).ToList();
            return rentals;
        }
        public static List<Property> getPropertiesbyOwnerID(int id)//דירות ששוכר לפי איידי
        {
            List<Property> properties=null;
            foreach (Rental r in getRentalsbyOwnerID(id))
                properties.Add((from p in db.Properties where r.PropertyID == p.PropertyID select p).FirstOrDefault());
            return properties;

        }

    }
}
