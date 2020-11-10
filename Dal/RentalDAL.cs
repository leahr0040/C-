using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class RentalDAL
    {
        public static int AddRental(Rental r)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                r.status = true;
                db.Rentals.Add(r);
                db.SaveChanges();
                return db.Rentals.Max(i => i.RentalID);
            }
            return 0;
        }
        public static List<Rental> Search(Nullable<int> propertyID, string owner, string user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Rental> rentals;
                rentals = (from r in db.Rentals where r.status == true select r).ToList();
                if (propertyID != null && propertyID != 0)
                    rentals = (from r in rentals where r.PropertyID == propertyID select r).ToList();
                if (owner != null)
                {
                    List<int> owners = (from own in db.PropertiesOwners where (own.OwnerFirstName + " " + own.OwnerLastName).Contains(owner.Trim()) select own.OwnerID).ToList();
                    rentals = (from r in rentals where owners.Contains(r.Property.OwnerID) select r).ToList();
                }
                if (user != null)
                    rentals = (from r in rentals where (r.User.FirstName + ' ' + r.User.FirstName).Contains(user.Trim()) select r).ToList();
                if (enteryDate != null && enteryDate != new DateTime())
                    rentals = (from r in rentals where r.EnteryDate >= enteryDate select r).ToList();
                if (endDate != null && endDate != new DateTime())
                    rentals = (from r in rentals where r.EndDate <= endDate select r).ToList();
                rentals = rentals.OrderBy(r => r.EndDate).ToList();
                return rentals;
            }
            return null;
        }
    }
}
