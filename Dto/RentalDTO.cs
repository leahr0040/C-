using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dal;

namespace Dto
{
    public class RentalDTO
    {
        public int RentalID { get; set; }
        public int PropertyID { get; set; }
        public Nullable<int> SubPropertyID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<double> RentPayment { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<System.DateTime> EnteryDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> ContactRenew { get; set; }
        public string Dock { get; set; }
        public string DocName { get; set; }
        public Nullable<bool> status { get; set; }
        public RentalDTO()
        {

        }

        public RentalDTO(Rental r)
        {
            RentalID = r.RentalID;
            PropertyID = r.PropertyID;
            SubPropertyID = r.SubPropertyID;
            UserID = r.UserID;
            RentPayment = r.RentPayment;
            PaymentTypeID = r.PaymentTypeID;
            EnteryDate = r.EnteryDate;
            EndDate = r.EndDate;
            ContactRenew = r.ContactRenew;
            status = r.status;
        }
        public RentalDTO(getAllRentals_Result r)
        {
            RentalID = r.RentalID;
            PropertyID = r.PropertyID;
            SubPropertyID = r.SubPropertyID;
            UserID = r.UserID;
            RentPayment = r.RentPayment;
            PaymentTypeID = r.PaymentTypeID;
            EnteryDate = r.EnteryDate;
            EndDate = r.EndDate;
            ContactRenew = r.ContactRenew;
            status = r.status;
        }

        public static Rental ToDal(RentalDTO r)
        {
            return new Rental
            {
                RentalID = r.RentalID,
                PropertyID = r.PropertyID,
                SubPropertyID = r.SubPropertyID,
                UserID = r.UserID,
                RentPayment = r.RentPayment,
                PaymentTypeID = r.PaymentTypeID,
                EnteryDate = r.EnteryDate,
                EndDate = r.EndDate,
                ContactRenew = r.ContactRenew,
                status=r.status
            };
        }
        public static List<RentalDTO> ConvertListToDTO(List<Rental> rentals)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<RentalDTO> redto = new List<RentalDTO>();
                    foreach (Rental r in rentals)
                        redto.Add(new RentalDTO(r));
                    return redto;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("ConvertListToDTORentalEror " + e.Message);
                return null;
            }
        }
        public static List<RentalDTO> ConvertListToDTO(List<getAllRentals_Result> rentals)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<RentalDTO> redto = new List<RentalDTO>();
                    foreach (getAllRentals_Result r in rentals)
                        redto.Add(new RentalDTO(r));
                    return redto;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("ConvertListToDTORentalEror " + e.Message);
                return null;
            }
        }
    }
}
