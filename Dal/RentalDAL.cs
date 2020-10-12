using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class RentalDAL
    {
        public static bool AddRental(Rental r)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Rentals.Add(r);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static List<Rental> Search(Nullable<int> propertyID,  String user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Rental> rentals;
                rentals = (from r in db.Rentals select r).ToList();
                if (propertyID != null)
                    rentals = (from r in rentals where r.PropertyID == propertyID select r).ToList();
                //if (subPropertyID != null)
                //    rentals = (from r in rentals where r.SubPropertyID != null select r).ToList();
                //if (user != null)
                //    rentals = (from r in rentals where (r.User.FirstName + ' ' + r.User.FirstName).Contains(user.Trim()) select r).ToList();
                //if (rentPayment != null)
                //    rentals = (from r in rentals where r.RentPayment >= rentPayment select r).ToList();
                //if (paymentTypeID != null)
                //    rentals = (from r in rentals where r.PaymentTypeID == paymentTypeID select r).ToList();
                if (enteryDate != null)
                    rentals = (from r in rentals where r.EnteryDate >= enteryDate select r).ToList();
                if (endDate != null)
                    rentals = (from r in rentals where r.EndDate >= endDate select r).ToList();
                //if (contactRenew != null)
                //    rentals = (from r in rentals where r.ContactRenew == contactRenew select r).ToList();
                return rentals;
            }
            return null;
        }
    }
}
