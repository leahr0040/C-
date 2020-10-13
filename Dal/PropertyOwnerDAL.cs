using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal
{
    public class PropertyOwnerDAL
    {
        public static int AddPropertyOwner(PropertiesOwner po)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                po.status = true;
                db.PropertiesOwners.Add(po);
                db.SaveChanges(); 
                return (from p in db.PropertiesOwners where p.OwnerFirstName==po.OwnerFirstName && p.OwnerLastName==po.OwnerLastName && p.Phone==po.Phone && p.Email==po.Email select p.OwnerID).FirstOrDefault();
            }
            return 0;
        }

        
        public static bool DeletePropertyOwner(PropertiesOwner po)//צריך לבדוק כי לא אמורים למחוק לגמרי אלא להעביר לארכיון
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.PropertiesOwners.Remove(po);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static List<PropertiesOwner> Search(string OwnerFirstName, string OwnerLastName, string Phone, string Email)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertiesOwner> po;
                po = (from p in db.PropertiesOwners where p.status == true select p).ToList();
                if (OwnerFirstName != null)
                    po = (from p in po where p.OwnerFirstName.Contains(OwnerFirstName) select p).ToList();
                if (OwnerLastName != null)
                    po = (from p in po where p.OwnerLastName.Contains(OwnerLastName) select p).ToList();
                if (Phone != null)
                    po = (from p in po where p.Phone.Contains(Phone) select p).ToList();
                if (Email != null)
                    po = (from p in po where p.Email.Contains(Email) select p).ToList();
                return po;
            }
            return null;
        }
        public static List<Property> getPropertiesbyOwnerID(int id)//דירות שמשכיר לפי איידי
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Property> properties = (from r in db.Properties where r.OwnerID == id select r).ToList();
                return properties;
            }
            return null;
        }

        public static List<Rental> getRentalsbyOwnerID(int id)//פרטי השכרה לפי איידי
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Rental> rentals = new List<Rental>();
                foreach (Property p in getPropertiesbyOwnerID(id))
                    rentals.Add((from r in db.Rentals where r.PropertyID == p.PropertyID select r).FirstOrDefault());
                return rentals;
            }
            return null;
        }
    }
}
