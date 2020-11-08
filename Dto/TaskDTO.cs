using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public  class TaskDTO
    {
        public int TaskID { get; set; }
        public int TaskTypeId { get; set; }
        public string Description { get; set; }
        public Nullable<int> PropertyID { get; set; }
        public Nullable<int> SubPropertyID { get; set; }
        public Nullable<int> ClassificationID { get; set; }
        public Nullable<int> ClientClassificationID { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public System.DateTime DateForHandling { get; set; }
        public Nullable<bool> IsHandled { get; set; }
        public Nullable<System.DateTime> HandlingDate { get; set; }
        public string HandlingWay { get; set; }

        public string Dock { get; set; }
        public string DocName { get; set; }
        public bool status { get; set; }
        public TaskDTO()
        {

        }
        public TaskDTO(Dal.Task t)
        {
            this.TaskID = t.TaskID;
            this.TaskTypeId = t.TaskTypeId;
            this.Description = t.Description;
            this.PropertyID = t.PropertyID;
            this.SubPropertyID = t.SubPropertyID;
            this.ClassificationID = t.ClassificationID;
            this.ClientClassificationID = t.ClientClassificationID;
            this.ReportDate = t.ReportDate;
            this.DateForHandling = t.DateForHandling;
            this.IsHandled = t.IsHandled;
            this.HandlingDate = t.HandlingDate;
            this.HandlingWay = t.HandlingWay;
            status = true;
        }
        public TaskDTO(getAllTasks_Result t)
        {
            this.TaskID = t.TaskID;
            this.TaskTypeId = t.TaskTypeId;
            this.Description = t.Description;
            this.PropertyID = t.PropertyID;
            this.SubPropertyID = t.SubPropertyID;
            this.ClassificationID = t.ClassificationID;
            this.ClientClassificationID = t.ClientClassificationID;
            this.ReportDate = t.ReportDate;
            this.DateForHandling = t.DateForHandling;
            this.IsHandled = t.IsHandled;
            this.HandlingDate = t.HandlingDate;
            this.HandlingWay = t.HandlingWay;
            status = true;
        }
        public static Dal.Task ToDal(TaskDTO t)
        {
            return new Dal.Task
            {
                TaskID = t.TaskID,
                TaskTypeId = t.TaskTypeId,
                Description = t.Description,
                PropertyID = t.PropertyID,
                SubPropertyID = t.SubPropertyID,
                ClassificationID = t.ClassificationID,
                ClientClassificationID = t.ClientClassificationID,
                ReportDate = t.ReportDate,
                DateForHandling = t.DateForHandling,
                IsHandled = t.IsHandled,
                HandlingDate = t.HandlingDate,
                HandlingWay = t.HandlingWay
            };
        }
    }
}
