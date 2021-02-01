using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Dal;
using Dto;
using System.Diagnostics;

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
                if (rd.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = rd.Dock;
                    doc.DocUser = id;
                    doc.type = 3;
                    doc.DocName = rd.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                }
                if (rd.ContactRenew == true && (rd.EndDate).Value < DateTime.Today.AddMonths(3))
                    Bl.TaskBL.AddRenewTask(rd.PropertyID, rd.SubPropertyID);
                return true;
            }
            return false;

        }
        public static bool DeleteRental(int id)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                
                Rental p = db.Rentals.Find(id);
                p.Property.IsRented = false;
                p.status = false;
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("deleteRentalEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateRental(RentalDTO rd)
        {
            try { 
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
                if(r.ContactRenew != rd.ContactRenew)
                {
                    if (rd.ContactRenew == true && rd.EndDate.Value < DateTime.Today.AddMonths(3))
                        Bl.TaskBL.AddRenewTask(rd.PropertyID, rd.SubPropertyID);
                    else
                    {
                       getAllTasks_Result task= db.getAllTasks().Where(t => t.PropertyID == rd.PropertyID && t.TaskTypeId == 2 && t.status == true && t.SubPropertyID == rd.SubPropertyID).FirstOrDefault();
                        if (task != null)
                            Bl.TaskBL.DeleteTask(task.TaskID);
                    }
                }
                r.ContactRenew = rd.ContactRenew;
                
                if (rd.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = rd.Dock;
                    doc.DocUser = rd.RentalID;
                    doc.type = 3;
                    doc.DocName = rd.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                }
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("updateRentalEror " + e.Message);
                return false;
            }
        }
        public static List<RentalDTO> ConvertListToDTO(List<Rental> rentals)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<RentalDTO> redto = new List<RentalDTO>();
                foreach (Rental r in rentals)
                    redto.Add(new RentalDTO(r));
                return redto;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("ConvertListToDTORentalEror " + e.Message);
                return null;
            }
        }
        public static List<RentalDTO> ConvertListToDTO(List<getAllRentals_Result> rentals)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<RentalDTO> redto = new List<RentalDTO>();
                foreach (getAllRentals_Result r in rentals)
                    redto.Add(new RentalDTO(r));
                return redto;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("ConvertListToDTORentalEror " + e.Message);
                return null;
            }
        }
        public static List<RentalDTO> Search(Nullable<int> propertyID, string owner, string user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {

            List<Rental> rentals = RentalDAL.Search(propertyID, owner, user, enteryDate, endDate);
            return ConvertListToDTO(rentals);
        }
        public static List<RentalDTO> GetAllRentals()
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<getAllRentals_Result> pro = (from r in db.getAllRentals()select r).OrderBy(r => r.EndDate).ToList();

                return ConvertListToDTO(pro);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getAllRentalEror " + e.Message);
                return null;
            }
        }
        public static List<PaymentTypeDTO> GetAllPaymentTypes()
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PaymentType> ptypes = (from pt in db.PaymentTypes select pt).OrderBy(pt => pt.PaymentTypeName).ToList();
                List<PaymentTypeDTO> ptdto = new List<PaymentTypeDTO>();
                foreach (PaymentType ptype in ptypes)
                {
                    ptdto.Add(new PaymentTypeDTO(ptype));
                }
                return ptdto;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getallPaymentTypesRentalEror " + e.Message);
                return null;
            }
        }
        static CancellationTokenSource ctSource;
        public static void setYearly(DateTime date)//מקבלת תאריך מדויק
        {
            try {
            ctSource = new CancellationTokenSource();
            var dateNow = DateTime.Now;
            // TimeSpan ts;//אובייקט שמייצג את מרווח הזמן שנותר עד להפעלת התהליך
            if (date <= dateNow)
            {//אם התאריך המבוקש עבר כבר-מקדם אותו למועד הבא
                date = date.AddYears(1);//במקרה שלנו- קידום התאריך ביום(יכול להיות גם הוספת דקות/שעות)
                DeleteAllNotUsedRentals();//קריאה לפונקציה המבוקשת
            }                                                   //שימתין את פרק הזמן שנקבע, ואח"כ יקרא לפונקציה שרצינו שתופעל פעם ב... threadהפעלת ה 
            System.Threading.Tasks.Task.Delay(1000 * 60 * 60 * 24 * 20).ContinueWith((x) =>
            {

                setYearly(date);//קריאה חוזרת לפונקציה...
            }, ctSource.Token);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("setYearlyEror " + e.Message);
                
            }
        }
        //    static System.Timers.Timer timer;
        public static void DeleteAllNotUsedRentals()
        {
            try { 
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
        //    public static void schedule_Timer(DateTime scheduledTime)
        //    {

        //        DateTime nowTime = DateTime.Now;
        //        //DateTime scheduledTime = DateTime.Now.AddSeconds(15); //new DateTime((nowTime.Year, nowTime.Month, nowTime.Day, 8, 42, 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
        //        if (nowTime > scheduledTime)
        //        {
        //            scheduledTime = scheduledTime.AddMonths(1);
        //        }
        //        double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
        //        timer = new System.Timers.Timer(tickTime);
        //        timer.Elapsed += new ElapsedEventHandler(DeleteAllNotUsedRentals);
        //        timer.Start();
        //    }
    }

}