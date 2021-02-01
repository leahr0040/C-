using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Dal
{
    public class PropertyOwnerDAL
    {
        public static int AddPropertyOwner(PropertiesOwner po)
        {
            try {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                po.status = true;
                db.PropertiesOwners.Add(po);
                db.SaveChanges();
                return db.PropertiesOwners.Max(i => i.OwnerID);
                //return (from p in db.PropertiesOwners where p.OwnerFirstName==po.OwnerFirstName && p.OwnerLastName==po.OwnerLastName && p.Phone==po.Phone && p.Email==po.Email select p.OwnerID).FirstOrDefault();
            }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addPropertyOwnerEror " + e.Message);
                return 0;
            }
        }


        public static List<PropertiesOwner> Search(string OwnerFirstName, string OwnerLastName, string Phone, string Email)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertiesOwner> po;
                po = (from p in db.PropertiesOwners where p.status == true select p).ToList();
                if (OwnerFirstName != null)
                    po = (from p in po where p.OwnerFirstName != null && p.OwnerFirstName.Contains(OwnerFirstName) select p).ToList();
                if (OwnerLastName != null)
                    po = (from p in po where p.OwnerLastName != null && p.OwnerLastName.Contains(OwnerLastName) select p).ToList();
                if (Phone != null)
                    po = (from p in po where p.Phone != null && p.Phone.Contains(Phone) select p).ToList();
                if (Email != null)
                    po = (from p in po where p.Email != null && p.Email.Contains(Email) select p).ToList();
                po = po.OrderBy(o => o.OwnerFirstName).OrderBy(o => o.OwnerLastName).ToList();
                return po;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("searchPropertyOwnerEror " + e.Message);
                return null;
            }
        }
        //public static List<Property> getPropertiesbyOwnerID(int id)//דירות שמשכיר לפי איידי
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        List<Property> properties = (from r in db.Properties where r.OwnerID == id select r).ToList();
        //        return properties;
        //    }
        //    return null;
        //}

        //public static List<Rental> getRentalsbyOwnerID(int id)//פרטי השכרה לפי איידי
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        List<Rental> rentals = new List<Rental>();
        //        foreach (Property p in getPropertiesbyOwnerID(id))
        //            rentals.Add((from r in db.Rentals where r.PropertyID == p.PropertyID select r).FirstOrDefault());
        //        return rentals;
        //    }
        //    return null;
        //}
    }
}
