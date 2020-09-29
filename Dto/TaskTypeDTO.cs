using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class TaskTypeDTO
    {
        public int TaskTypeId { get; set; }
        public string TaskTypeName { get; set; }
        public TaskTypeDTO()
        {
        }
        public TaskTypeDTO(TaskType tt)
        {
            TaskTypeId =tt. TaskTypeId;
            TaskTypeName =tt. TaskTypeName;
        }
        public static TaskType ToDAL(TaskType ttDTO)
        {
            return new TaskType
            {
                TaskTypeId = ttDTO.TaskTypeId,
                TaskTypeName = ttDTO.TaskTypeName
            };
        }

    }
}
