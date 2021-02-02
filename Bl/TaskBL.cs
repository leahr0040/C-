using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Data;
using System.Data.Entity.Core.Objects;
using Dal;
using Dto;
using System.Data.Entity;
using System.Diagnostics;

namespace Bl
{
    public class TaskBL
    {
        public static bool AddTask(TaskDTO td)
        {

            Dal.Task t = TaskDTO.ToDal(td);
            int id = TaskDAL.AddTask(t);
            if (id != 0)
            {
                if (td.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(id, td.Dock, 6, td.DocName));
                }
                return true;
            }
            return false;
        }
        public static bool DeleteTask(int id)
        {
            try
            {
                return TaskDAL.DeleteTask(id);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("DeleteTaskbl " + e.Message);
                return false;
            }
        }
        public static bool UpdateTask(TaskDTO td)
        {
            try
            {

                if (td.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(td.TaskID, td.Dock, 6, td.DocName));

                }
                return TaskDAL.UpdateTask(TaskDTO.ToDal(td));

            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdateTaskblEror " + e.Message);
                return false;
            }
        }
        public static bool SetClassification(int id, int classificationId)
        {
            try
            {
                return TaskDAL.SetClassification(id, classificationId);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("setclassifTaskEror " + e.Message);
                return false;
            }
        }

        static CancellationTokenSource m_ctSource;
        public static void RunPrepareDaily(DateTime date)//מקבלת תאריך מדויק
        {
            try
            {
                m_ctSource = new CancellationTokenSource();
                var dateNow = DateTime.Now;
                TimeSpan ts;//אובייקט שמייצג את מרווח הזמן שנותר עד להפעלת התהליך
                if (date <= dateNow)       //אם התאריך המבוקש עבר כבר-מקדם אותו למועד הבא
                    date = date.AddDays(1);//במקרה שלנו- קידום התאריך ביום(יכול להיות גם הוספת דקות/שעות)
                ts = date - dateNow;
                //שימתין את פרק הזמן שנקבע, ואח"כ יקרא לפונקציה שרצינו שתופעל פעם ב... threadהפעלת ה 
                System.Threading.Tasks.Task.Delay(ts).ContinueWith((x) =>
                {
                    DailySet();//קריאה לפונקציה המבוקשת
                Addtask2();
                    RunPrepareDaily(date);//קריאה חוזרת לפונקציה...
            }, m_ctSource.Token);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("runPrepareDailyEror " + e.Message);

            }
        }
        public static bool DailySet()
        {
            try
            {

                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    foreach (Dal.Task task in db.Tasks)
                    {
                        if (task.IsHandled != true && task.ClassificationID != null)
                        {
                            if (task.DateForHandling.Date <= DateTime.Now.Date || (task.TaskTypeId != 1 && task.TaskTypeId != 4 && (task.DateForHandling.Date - DateTime.Now.Date).Days <= 30))
                                task.ClassificationID = 1;
                            else if (task.ClassificationID != 1 && ((task.DateForHandling.Date - DateTime.Now.Date).Days <= 7 || (task.TaskTypeId != 1 && task.TaskTypeId != 4 && (task.DateForHandling.Date - DateTime.Now.Date).Days <= 60)))
                                task.ClassificationID = 2;
                        }
                    }
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception e)
            {
                Trace.TraceInformation("DailySetTaskEror " + e.Message);
                return false;
            }
        }

        public static void setMonthly(DateTime date)//מקבלת תאריך מדויק
        {
            try
            {
                CancellationTokenSource ctSource;
                ctSource = new CancellationTokenSource();
                var dateNow = DateTime.Now;
                // TimeSpan ts;//אובייקט שמייצג את מרווח הזמן שנותר עד להפעלת התהליך
                if (date <= dateNow)
                {//אם התאריך המבוקש עבר כבר-מקדם אותו למועד הבא
                    date = date.AddMonths(1);
                    TaskDAL.DeleteAllNotUsedTasks();//קריאה לפונקציה המבוקשת

                }//במקרה שלנו- קידום התאריך ביום(יכול להיות גם הוספת דקות/שעות)
                 // ts = date - dateNow;
                 //שימתין את פרק הזמן שנקבע, ואח"כ יקרא לפונקציה שרצינו שתופעל פעם ב... threadהפעלת ה 
                System.Threading.Tasks.Task.Delay(1000 * 60 * 60 * 24 * 5).ContinueWith((x) =>
                {

                    setMonthly(date);//קריאה חוזרת לפונקציה...
            }, ctSource.Token);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("setMonthlyTaskEror " + e.Message);

            }
        }
        //        static System.Timers.Timer timer;
        //        public static void schedule_Timer(DateTime scheduledTime)
        //        {

        //            DateTime nowTime = DateTime.Now;
        //            //DateTime scheduledTime = DateTime.Now.AddSeconds(15); //new DateTime((nowTime.Year, nowTime.Month, nowTime.Day, 8, 42, 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
        //            if (nowTime > scheduledTime)
        //            {
        //                scheduledTime = scheduledTime.AddMonths(1);
        //            }
        ////double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
        //            timer = new System.Timers.Timer(1000*60*60*24*5);
        //            timer.Elapsed += new ElapsedEventHandler(DeleteAllNotUsedTasks);
        //            timer.Start();
        //        }
        
        public static void Addtask2()
        {
            try
            {
                ; /*= new TaskDTO();*/
                List<RentalDTO> pro = RentalBL.GetAllRentals();
                foreach (RentalDTO rental in pro)
                {
                    if ((rental.EndDate).Value == DateTime.Today.AddMonths(3))
                    {
                        AddRenewTask(rental.PropertyID, rental.SubPropertyID);

                    }
                }
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addTask2Eror " + e.Message);

            }
        }
        public static bool AddRenewTask(int propertyID, int? subpropertyID)
        {
            TaskDTO t = new TaskDTO();
            t.TaskTypeId = 2;
            if (subpropertyID != null)
            {
                SubPropertyDTO sub = Bl.SubPropertyBL.GetSubPropertyByID(subpropertyID.Value);
                t.Description = propertyID + "בנכס " + sub.num + "סיום חוזה לדירה מס:";
            }
            else
                t.Description = propertyID + "סיום חוזה לדירה:";

            t.PropertyID = propertyID;
            t.SubPropertyID = subpropertyID;
            t.ClassificationID = 3;
            t.ClientClassificationID = null;////////
            t.ReportDate = DateTime.Today;
            t.DateForHandling = DateTime.Today.AddMonths(3);//לבדוק למה כתבה 1
            t.IsHandled = false;
            t.HandlingDate = null;
            t.status = true;
            return AddTask(t);

        }

        
        public static List<TaskDTO> Search(Nullable<int> TaskTypeId, Nullable<int> ClassificationID, System.DateTime DateForHandling, Nullable<bool> IsHandled)
        {
            List<Dal.Task> tasks = TaskDAL.Search(TaskTypeId, ClassificationID, DateForHandling, IsHandled);
            return TaskDTO. ConvertListToDTO(tasks);
        }
        public static List<TaskDTO> GetAllTasks()
        {
            try
            {
                
                    List<getAllTasks_Result> tasks = TaskDAL.GetAllTasks();
                    return TaskDTO.ConvertListToDTO(tasks);
               
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllTaskblEror " + e.Message);
                return null;
            }
        }
        public static List<TaskDTO> GetAllarchivesTasks()
        {
            try
            {
               
                    List<getAllTasks_Result> tasks = TaskDAL.GetAllarchivesTasks();
                    return TaskDTO.ConvertListToDTO(tasks);
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getArchiveTaskblEror " + e.Message);
                return null;
            }
        }
        public static List<TaskDTO> GetTimePassedTasks()
        {
            try
            {
                    List<getAllTasks_Result> tasks = TaskDAL.GetTimePassedTasks();
                    return TaskDTO.ConvertListToDTO(tasks);
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getTimePassedTaskblEror " + e.Message);
                return null;
            }
        }
        public static List<TaskDTO> GetNotClassificatedTasks()
        {
            try
            {
               
                    List<getAllTasks_Result> tasks = TaskDAL.GetNotClassificatedTasks();
                    return TaskDTO. ConvertListToDTO(tasks);
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getnotClassifTaskEror " + e.Message);
                return null;
            }
        }
        //public static string GetTypeName(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        return db.TaskTypes.Find(id).TaskTypeName;
        //    }
        //    return null;
        //}

        public static List<TaskClassificationDTO> GetAllClassificationTypes()
        {
            try
            {
               
                    List<Classification> classifications =TaskDAL.GetAllClassificationTypes();
                    List<TaskClassificationDTO> taskClassifications = new List<TaskClassificationDTO>();
                    foreach (Classification classif in classifications)
                        taskClassifications.Add(new TaskClassificationDTO(classif));
                    return taskClassifications;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllClassifTaskEror " + e.Message);
                return null;
            }
        }
        public static List<TaskTypeDTO> GetAllTaskTypes()
        {
            try
            {
                    List<TaskType> taskTypes = TaskDAL.GetAllTaskTypes();
                    List<TaskTypeDTO> typesDTO = new List<TaskTypeDTO>();
                    foreach (TaskType type in taskTypes)
                        typesDTO.Add(new TaskTypeDTO(type));
                    return typesDTO;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllTaskTypesEror " + e.Message);
                return null;
            }
        }
        public static bool AddTaskType(string name)
        {
            try
            {
                return TaskDAL.AddTaskType(name);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addTaskTypeEror " + e.Message);
                return false;
            }
        }
        //public static TaskDTO ReturnTaskbyid(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        Dal.Task task = db.Tasks.Find(id);
        //        return new TaskDTO(task);
        //        //bool x = false; int i;
        //        //List<TaskDTO> s = GetAllTasks();
        //        //for (i = 0; i < s.Count ||x; i++)
        //        //{
        //        //    if (id == s[i].TaskID)
        //        //    {
        //        //        i--;
        //        //        x = true;
        //        //    }
        //        //}
        //        //return s[i];

        //    }
        //}

    }
}
