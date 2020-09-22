using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal
{
    public class PropertyOwnerDAL
    {
        public static bool AddPropertyOwner(PropertiesOwner po)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.PropertiesOwners.Add(po);
                db.SaveChanges();
                return true;
            }
            return false;
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
                po = (from p in db.PropertiesOwners select p).ToList();
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
    }
}
