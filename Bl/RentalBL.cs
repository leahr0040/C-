using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;

namespace Bl
{
    public class RentalBL
    {
        public static bool AddRental(RentalDTO rd)
        {
            Rental r = RentalDTO.ToDal(rd);
            int id = RentalDAL.AddRental(r);
            if (id != 0)
            {
                Document doc = new Document();
                doc.DocCoding = rd.Dock;
                doc.DocUser = id;
                doc.type = 3;
                doc.DocName = rd.DocName;
                DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                if (rd.ContactRenew == true && (rd.EndDate).Value < DateTime.Today.AddMonths(3))
                {
                    Bl.TaskBL.AddRenewTask(rd.PropertyID, rd.SubPropertyID);                
                }
                return true;
            }
            return false;

        }
        public static bool DeleteRental(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Rental p = db.Rentals.Find(id);
                p.status = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateRental(RentalDTO rd)
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
                r.ContactRenew = rd.ContactRenew;
                return true;
            }
            return false;
        }
        public static List<RentalDTO> ConvertListToDTO(List<Rental> rentals)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<RentalDTO> redto = new List<RentalDTO>();
                foreach (Rental r in rentals)
                    redto.Add(new RentalDTO(r));
                return redto;
            }
            return null;
        }
        public static List<RentalDTO> Search(Nullable<int> propertyID, String user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {

            List<Rental> rentals = RentalDAL.Search(propertyID, user, enteryDate, endDate);
            return ConvertListToDTO(rentals);
        }
        public static List<RentalDTO> GetAllRentals()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Rental> pro = (from r in db.Rentals where r.status == true select r).ToList();
                return ConvertListToDTO(pro);
            }
        }
        
    }

}