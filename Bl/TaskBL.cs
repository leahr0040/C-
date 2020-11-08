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
                    Document doc = new Document();
                    doc.DocCoding = td.Dock;
                    doc.DocUser = id;
                    doc.type = 6;
                    doc.DocName = td.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));

                }
                return true;
            }
            return false;
        }
        public static bool DeleteTask(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Dal.Task t = db.Tasks.Find(id);
                t.status = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateTask(TaskDTO td)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Dal.Task t = db.Tasks.Find(td.TaskID);


                t.TaskTypeId = td.TaskTypeId;
                t.Description = td.Description;
                t.PropertyID = td.PropertyID;
                t.SubPropertyID = td.SubPropertyID;
                t.ClassificationID = td.ClassificationID;
                t.ClientClassificationID = td.ClientClassificationID;
                t.ReportDate = td.ReportDate;
                t.DateForHandling = td.DateForHandling;
                t.IsHandled = td.IsHandled;
                if (td.IsHandled == true)
                    t.status = false;
                else
                    t.status = true;

                t.HandlingDate = td.HandlingDate;
                t.HandlingWay = td.HandlingWay;
                if (td.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = td.Dock;
                    doc.DocUser = td.TaskID;
                    doc.type = 6;
                    doc.DocName = td.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                    return true;
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool SetClassification(int id, int classificationId)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    Dal.Task task = db.Tasks.Find(id);
                    task.ClassificationID = classificationId;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        static CancellationTokenSource m_ctSource;
        public static void RunPrepareDaily(DateTime date)//מקבלת תאריך מדויק
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
                RunPrepareDaily(date);//קריאה חוזרת לפונקציה...
            }, m_ctSource.Token);
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
            catch
            {
                return false;
            }
        }

        public static void setMonthly(DateTime date)//מקבלת תאריך מדויק
        {
            CancellationTokenSource ctSource;
            ctSource = new CancellationTokenSource();
            var dateNow = DateTime.Now;
            // TimeSpan ts;//אובייקט שמייצג את מרווח הזמן שנותר עד להפעלת התהליך
            if (date <= dateNow)
            {//אם התאריך המבוקש עבר כבר-מקדם אותו למועד הבא
                date = date.AddMonths(1);
                DeleteAllNotUsedTasks();//קריאה לפונקציה המבוקשת

            }//במקרה שלנו- קידום התאריך ביום(יכול להיות גם הוספת דקות/שעות)
             // ts = date - dateNow;
             //שימתין את פרק הזמן שנקבע, ואח"כ יקרא לפונקציה שרצינו שתופעל פעם ב... threadהפעלת ה 
            System.Threading.Tasks.Task.Delay(1000 * 60 * 60 * 24 * 5).ContinueWith((x) =>
            {

                setMonthly(date);//קריאה חוזרת לפונקציה...
            }, ctSource.Token);
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
        public static void DeleteAllNotUsedTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                foreach (Dal.Task task in db.Tasks)
                {
                    if (task.status == false)
                        db.Tasks.Remove(task);
                }
                db.SaveChanges();
            }
        }
        public static void Addtask2()
        {
            ; /*= new TaskDTO();*/
            List<RentalDTO> pro = Bl.RentalBL.GetAllRentals();
            int x = pro.Count;
            int i = 0;
            while (i != x)
            {
                if ((pro[i].EndDate).Value == DateTime.Today.AddMonths(3))
                {
                    AddRenewTask(pro[i].PropertyID, pro[i].SubPropertyID);
                    i++;
                }

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

        public static List<TaskDTO> ConvertListToDTO(List<Dal.Task> tasks)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<TaskDTO> tdto = new List<TaskDTO>();
                foreach (Dal.Task t in tasks)
                    tdto.Add(new TaskDTO(t));
                return tdto;
            }
            return null;
        }

        public static List<TaskDTO> ConvertListToDTO(List<getAllTasks_Result> tasks)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<TaskDTO> tdto = new List<TaskDTO>();
                foreach (getAllTasks_Result t in tasks)
                    tdto.Add(new TaskDTO(t));
                return tdto;
            }
            return null;
        }
        public static List<TaskDTO> Search(Nullable<int> TaskTypeId, Nullable<int> ClassificationID, System.DateTime DateForHandling, Nullable<bool> IsHandled)
        {
            List<Dal.Task> tasks = TaskDAL.Search(TaskTypeId, ClassificationID, DateForHandling, IsHandled);
            return ConvertListToDTO(tasks);
        }
        public static List<TaskDTO> GetAllTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<getAllTasks_Result> tasks = (from t in db.getAllTasks() where t.status == true select t).OrderBy(t => t.DateForHandling).ToList();
                return ConvertListToDTO(tasks);
            }
        }
        public static List<TaskDTO> GetAllarchivesTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Dal.Task> tasks = (from t in db.Tasks where t.status == false select t).OrderBy(t => t.DateForHandling).ToList();
                return ConvertListToDTO(tasks);
            }
        }
        public static List<TaskDTO> GetTimePassedTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Dal.Task> tasks = (from t in db.Tasks where t.status == true && t.IsHandled == false && t.DateForHandling > DateTime.Today select t).ToList();
                return ConvertListToDTO(tasks);
            }
        }
        public static string GetTypeName(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                return db.TaskTypes.Find(id).TaskTypeName;
            }
            return null;
        }

        public static List<TaskClassificationDTO> GetAllClassificationTypes()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Classification> classifications = db.Classifications.ToList();
                List<TaskClassificationDTO> taskClassifications = new List<TaskClassificationDTO>();
                foreach (Classification classif in classifications)
                    taskClassifications.Add(new TaskClassificationDTO(classif));
                return taskClassifications;

            }
            return null;
        }
        public static List<TaskTypeDTO> GetAllTaskTypes()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<TaskType> taskTypes = db.TaskTypes.ToList();
                List<TaskTypeDTO> typesDTO = new List<TaskTypeDTO>();
                foreach (TaskType type in taskTypes)
                    typesDTO.Add(new TaskTypeDTO(type));
                return typesDTO;

            }
            return null;
        }
        public static bool AddTaskType(string name)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                TaskType taskType = new TaskType();
                taskType.TaskTypeName = name;
                db.TaskTypes.Add(taskType);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static TaskDTO ReturnTaskbyid(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Dal.Task task = db.Tasks.Find(id);
                return new TaskDTO(task);
                //bool x = false; int i;
                //List<TaskDTO> s = GetAllTasks();
                //for (i = 0; i < s.Count ||x; i++)
                //{
                //    if (id == s[i].TaskID)
                //    {
                //        i--;
                //        x = true;
                //    }
                //}
                //return s[i];

            }
        }

    }
}
