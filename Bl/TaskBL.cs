using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;


namespace Bl
{
    class TaskBL
    {
        public static bool Task(TaskDTO td)
        {
            Dal.Task t = TaskDTO.ToDal(td);
            return TaskDAL.AddTask(t);
        }
        public static bool UpdateTask(TaskDTO td)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Dal.Task t = db.Tasks.Find(td.TaskID);

                t.TaskID = td.TaskID;
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
        public static List<TaskDTO> Search(Nullable<int> TaskTypeId, Nullable<int> PropertyID, Nullable<int> ClassificationID, Nullable<System.DateTime> ReportDate, System.DateTime DateForHandling, Nullable<bool> IsHandled, Nullable<System.DateTime> HandlingDate)
        {
            List<Dal.Task> tasks = TaskDAL.Search(TaskTypeId, PropertyID, ClassificationID,  ReportDate,  DateForHandling, IsHandled, HandlingDate);
            List<TaskDTO> tdto = new List<TaskDTO>();
            foreach (Dal.Task t in tasks)
                tdto.Add(new TaskDTO(t));
            return tdto;
        }
    }
}
