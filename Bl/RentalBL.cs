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
                    DocumentBL.AddUserDocuments(new DocumentDTO(id,rd.Dock,3,rd.DocName));
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
            
                return RentalDAL.DeleteRental(id);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deleteRentalblEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateRental(RentalDTO rd)
        {
            try
            {

                Rental r = RentalDAL.UpdateRental(RentalDTO.ToDal(rd));
                if (r != null)
                {
                    if (r.ContactRenew != rd.ContactRenew)
                    {
                        if (rd.ContactRenew == true && rd.EndDate.Value < DateTime.Today.AddMonths(3))
                            TaskBL.AddRenewTask(rd.PropertyID, rd.SubPropertyID);
                        else
                        {
                            getAllTasks_Result task = TaskDAL.GetTaskForDeleteing(rd.PropertyID, 2, rd.SubPropertyID);
                            if (task != null)
                                TaskBL.DeleteTask(task.TaskID);
                        }
                    }

                    if (rd.Dock != null)
                    {

                        DocumentBL.AddUserDocuments(new DocumentDTO(rd.RentalID, rd.Dock, 3, rd.DocName));
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updateRentalblEror " + e.Message);
                return false;
            }
        }
        
        public static List<RentalDTO> Search(Nullable<int> propertyID, string owner, string user, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate)
        {

            List<Rental> rentals = RentalDAL.Search(propertyID, owner, user, enteryDate, endDate);
            return RentalDTO.ConvertListToDTO(rentals);
        }
        public static List<RentalDTO> GetAllRentals()
        {
            try { 
            
                List<getAllRentals_Result> pro = RentalDAL.GetAllRentals();
                return RentalDTO.ConvertListToDTO(pro);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllRentalEror " + e.Message);
                return null;
            }
        }
        public static List<PaymentTypeDTO> GetAllPaymentTypes()
        {
            try { 
            List<PaymentType> ptypes = RentalDAL.GetAllPaymentTypes();
                List<PaymentTypeDTO> ptdto = new List<PaymentTypeDTO>();
                foreach (PaymentType ptype in ptypes)
                {
                    ptdto.Add(new PaymentTypeDTO(ptype));
                }
                return ptdto;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getallPaymentTypesRentalblEror " + e.Message);
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
               RentalDAL.DeleteAllNotUsedRentals();//קריאה לפונקציה המבוקשת
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