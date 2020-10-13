using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class TaskDAL
    {
        public static int AddTask(Task t)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                t.status = true;
                db.Tasks.Add(t);
                db.SaveChanges();
                return (from ta in db.Tasks where ta.PropertyID==t.PropertyID && ta.ReportDate==t.ReportDate && ta.DateForHandling==t.DateForHandling && ta.Description==t.Description select ta.TaskID).FirstOrDefault();
            }
            return 0;
        }
        public static List<Task> Search(Nullable<int> TaskTypeId ,Nullable<int> ClassificationID,System.DateTime DateForHandling,Nullable<bool> IsHandled)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Task> tasks  = (from t in  db.Tasks where t.status == true select t).ToList();
                if (TaskTypeId != null)
                    tasks = (from t in tasks where t.TaskTypeId == TaskTypeId select t).ToList();            
                if (ClassificationID != null)
                    tasks = (from t in tasks where t.ClassificationID == ClassificationID select t).ToList();
                
                if (DateForHandling != null)
                    tasks = (from t in tasks where t.DateForHandling == DateForHandling select t).ToList();
                if (IsHandled != null)
                    tasks = (from t in tasks where t.IsHandled == IsHandled select t).ToList();         
                return tasks;
            }
            return null;
        }
    }
}
