using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dal;
using Dto;


namespace Bl
{
    public class TaskBL
    {
        public static bool AddTask(TaskDTO td)
        {
            Dal.Task t = TaskDTO.ToDal(td);
            return TaskDAL.AddTask(t);
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
                t.HandlingDate = td.HandlingDate;
                t.HandlingWay = td.HandlingWay;

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
                        if(task.IsHandled!=true && task.ClassificationID!=null)
                        {
                            if (task.DateForHandling.Date <= DateTime.Now.Date   || (task.TaskTypeId!=1 && (task.DateForHandling.Date - DateTime.Now.Date).Days<=30))
                                task.ClassificationID = 1;
                           else if ((task.DateForHandling.Date-DateTime.Now.Date).Days<=7 || (task.TaskTypeId != 1 && (task.DateForHandling.Date - DateTime.Now.Date).Days <= 60))
                                task.ClassificationID = 2;
                            else
                                task.ClassificationID = 3;
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
        public static int? CountDatePassedTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                return db.Tasks.Count(t => (t.IsHandled != true && t.HandlingDate.Value.Date < DateTime.Today));
            }
            return null;
        }
        public static int? CountNotHandledTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                return db.Tasks.Count(t => t.IsHandled != true);
            }
            return null;
        }
        public static int? CountNotclassificatedTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                return db.Tasks.Count(t => t.ClassificationID == null);
            }
            return null;
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
        public static List<TaskDTO> Search(Nullable<int> TaskTypeId, Nullable<int> ClassificationID, System.DateTime DateForHandling, Nullable<bool> IsHandled)
        {
            List<Dal.Task> tasks = TaskDAL.Search(TaskTypeId, ClassificationID, DateForHandling, IsHandled);
            return ConvertListToDTO(tasks);
        }
        public static List<TaskDTO> GetAllTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Dal.Task> tasks = db.Tasks.ToList();
                return ConvertListToDTO(tasks);
            }
        }
        public static List<TaskDTO> GetTimePassedTasks()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Dal.Task> tasks = (from t in db.Tasks where t.IsHandled==false && t.DateForHandling.Date > DateTime.Today select t).ToList();
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
                List<Classification> classifications= db.Classifications.ToList();
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
                List<TaskTypeDTO> typesDTO= new List<TaskTypeDTO>();
                foreach (TaskType type in taskTypes)
                    typesDTO.Add(new TaskTypeDTO(type));
                return typesDTO;

            }
            return null;
        }
        
    }
}
