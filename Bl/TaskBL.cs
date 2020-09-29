using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static List<TaskDTO> Search(Nullable<int> TaskTypeId, Nullable<int> PropertyID, Nullable<int> ClassificationID, Nullable<System.DateTime> ReportDate, System.DateTime DateForHandling, Nullable<bool> IsHandled, Nullable<System.DateTime> HandlingDate)
        {
            List<Dal.Task> tasks = TaskDAL.Search(TaskTypeId, PropertyID, ClassificationID,  ReportDate,  DateForHandling, IsHandled, HandlingDate);
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
        public static string GetTypeName(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                return db.TaskTypes.Find(id).TaskTypeName;
            }
            return null;
        }
        //public static bool? IsTakala(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        return db.TaskTypes.Find(id).TaskTypeName == "תקלה";
        //    }
        //    return null;
        //}
        //public static string GetClassificationName(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        return db.Classifications.Find(id).ClassificationName;
        //    }
        //    return null;
        //}
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
