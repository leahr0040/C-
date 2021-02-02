using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dal
{
    public class TaskDAL
    {
        public static int AddTask(Task t)
        {
            try {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                
                //t.DateForHandling = new DateTime(t.DateForHandling.Year, t.DateForHandling.Month, t.DateForHandling.Day, 0, 0, 0);
                t.status = true;
                db.Tasks.Add(t);
                db.SaveChanges();
                return db.Tasks.Max(i => i.TaskID);
            } }
            catch (Exception e)
            {
                Trace.TraceInformation("addTaskEror " + e.Message);
                return 0;
            }
        }
        public static bool DeleteTask(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    Task t = db.Tasks.Find(id);
                    t.status = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("DeleteTask " + e.Message);
                return false;
            }
        }
        public static bool UpdateTask(Task td)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                   Task t = db.Tasks.Find(td.TaskID);
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
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdateTaskEror " + e.Message);
                return false;
            }
        }
        public static List<Task> Search(Nullable<int> TaskTypeId, Nullable<int> ClassificationID, Nullable<System.DateTime> DateForHandling, Nullable<bool> IsHandled)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Task> tasks = (from t in db.Tasks where t.status == true select t).ToList();
                if (TaskTypeId != 0&& TaskTypeId!=null)
                    tasks = (from t in tasks where t.TaskTypeId == TaskTypeId select t).ToList();
                if (ClassificationID != 0 && ClassificationID != null)
                    tasks = (from t in tasks where t.ClassificationID == ClassificationID select t).ToList();
                if (DateForHandling != null && DateForHandling != new DateTime())
                    tasks = (from t in tasks where t.DateForHandling.Date == DateForHandling.Value.Date select t).ToList();
                if (IsHandled != null)
                    tasks = (from t in tasks where t.IsHandled == IsHandled select t).ToList();
                tasks = tasks.OrderBy(t => t.ClassificationID).OrderBy(t => t.DateForHandling).ToList();
                return tasks;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("searchTaskEror " + e.Message);
                return null;
            }
        }
        public static getAllTasks_Result GetTaskForDeleteing(int PropertyID ,int TaskTypeId , Nullable<int> SubPropertyID )
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    getAllTasks_Result task = db.getAllTasks().Where(t => t.PropertyID == PropertyID && t.TaskTypeId == TaskTypeId && t.status == true && t.SubPropertyID == SubPropertyID).FirstOrDefault();
                    return task;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetTaskForDeleteingEror " + e.Message);
                return null;
            }
            
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
            catch (Exception e)
            {
                Trace.TraceInformation("setclassifTaskEror " + e.Message);
                return false;
            }
        }
        public static void DeleteAllNotUsedTasks()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    foreach (Task task in db.Tasks)
                    {
                        if (task.status == false)
                            db.Tasks.Remove(task);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("DeleteAllNotUsedTaskEror " + e.Message);

            }
        }
        public static List<getAllTasks_Result> GetAllTasks()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllTasks_Result> tasks = (from t in db.getAllTasks() select t).OrderBy(t => t.ClassificationID).OrderBy(t => t.DateForHandling).ToList();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllTaskEror " + e.Message);
                return null;
            }
        }
        public static List<getAllTasks_Result> GetAllarchivesTasks()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllTasks_Result> tasks = (from t in db.getAllTasks() where t.status == false select t).OrderBy(t => t.DateForHandling).ToList();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getArchiveTaskEror " + e.Message);
                return null;
            }
        }
        public static List<getAllTasks_Result> GetTimePassedTasks()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllTasks_Result> tasks = (from t in db.getAllTasks() where t.status == true && t.IsHandled == false && t.DateForHandling < DateTime.Today select t).ToList();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getTimePassedTaskEror " + e.Message);
                return null;
            }
        }
        public static List<getAllTasks_Result> GetNotClassificatedTasks()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllTasks_Result> tasks = (from t in db.getAllTasks() where (t.status == true && t.ClassificationID == 0 || t.ClassificationID == null) select t).ToList();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getnotClassifTaskEror " + e.Message);
                return null;
            }
        }
        public static List<Classification> GetAllClassificationTypes()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<Classification> classifications = db.Classifications.ToList();
                    return classifications;

                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllClassifTaskEror " + e.Message);
                return null;
            }
        }
        public static List<TaskType> GetAllTaskTypes()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<TaskType> taskTypes = db.TaskTypes.ToList();
                    return taskTypes;

                }
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
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    TaskType taskType = new TaskType();
                    taskType.TaskTypeName = name;
                    db.TaskTypes.Add(taskType);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addTaskTypeEror " + e.Message);
                return false;
            }
        }
    }
}
