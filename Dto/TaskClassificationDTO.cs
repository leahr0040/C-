using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class TaskClassificationDTO
    {
        
        public int ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public TaskClassificationDTO()
        {
        }
        public TaskClassificationDTO(Classification c)
        {
            ClassificationID =c. ClassificationID;
            ClassificationName =c. ClassificationName;
        }
        public static Classification ToDAL(TaskClassificationDTO tcDTO)
        {
            return new Classification
            {
                ClassificationID = tcDTO.ClassificationID,
                ClassificationName = tcDTO.ClassificationName
            };
        }

    }
}
