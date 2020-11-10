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
                
                //t.DateForHandling = new DateTime(t.DateForHandling.Year, t.DateForHandling.Month, t.DateForHandling.Day, 0, 0, 0);
                t.status = true;
                db.Tasks.Add(t);
                db.SaveChanges();
                return db.Tasks.Max(i => i.TaskID);
            }
            return 0;
        }
        public static List<Task> Search(Nullable<int> TaskTypeId, Nullable<int> ClassificationID, Nullable<System.DateTime> DateForHandling, Nullable<bool> IsHandled)
        {
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
            }
            return null;
        }
    }
}
