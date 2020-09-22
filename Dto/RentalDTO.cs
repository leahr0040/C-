using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ContactRenew = r.ContactRenew
            };
        }
    }
}
