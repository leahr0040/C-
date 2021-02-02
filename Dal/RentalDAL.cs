using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dal
{
    public class RentalDAL
    {
        public static int AddRental(Rental r)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                r.status = true;
                db.Rentals.Add(r);
                db.SaveChanges();
                return db.Rentals.Max(i => i.RentalID);
            }}

            catch (Exception e)
            {
                Trace.TraceInformation("addRentalEror " + e.Message);
                return 0;
            }
        }
        public static bool DeleteRental(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                { 
                    Rental p = db.Rentals.Find(id);
                    p.Property.IsRented = false;
                    p.status = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deleteRentalEror " + e.Message);
                return false;
            }
        }
        public static Rental UpdateRental(Rental rd)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    Rental r = db.Rentals.Find(rd.RentalID);
                    r.PropertyID = rd.PropertyID;
                    r.SubPropertyID = rd.SubPropertyID;
                    r.UserID = rd.UserID;
                    r.RentPayment = rd.RentPayment;
                    r.PaymentTypeID = rd.PaymentTypeID;
                    r.EnteryDate = rd.EnteryDate;
                    r.EndDate = rd.EndDate;
                    db.SaveChanges();
                    return r;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updateRentalEror " + e.Message);
                return null;
            }
        }
        
        public static List<Rental> Search(Nullable<int> propertyID, string owner, string user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {
            try { 
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
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("searchRentalEror " + e.Message);
                return null;
            }
        }
        public static List<getAllRentals_Result> GetAllRentals()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllRentals_Result> pro = (from r in db.getAllRentals() select r).OrderBy(r => r.EndDate).ToList();

                    return pro;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllRentalEror " + e.Message);
                return null;
            }
        }
        public static List<PaymentType> GetAllPaymentTypes()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<PaymentType> ptypes = (from pt in db.PaymentTypes select pt).OrderBy(pt => pt.PaymentTypeName).ToList();
                    return ptypes;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getallPaymentTypesRentalEror " + e.Message);
                return null;
            }
        }
        public static void DeleteAllNotUsedRentals()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    foreach (Rental rental in db.Rentals)
                    {
                        if (rental.status == false)
                            db.Rentals.Remove(rental);
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deleteallNotusedRentalEror " + e.Message);

            }
        }
    }
}
