using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class ExclusivityPersonDTO
    {
       
        public int ExclusivityID { get; set; }
        public string ExclusivityName { get; set; }
        public ExclusivityPersonDTO()
        {
        }
        public ExclusivityPersonDTO(Exclusivity e)
        {
            ExclusivityID =e.ExclusivityID;
            ExclusivityName =e.ExclusivityName;
        }
        public static Exclusivity ToDAL(ExclusivityPersonDTO eDTO)
        {
            return new Exclusivity
            {
                ExclusivityID = eDTO.ExclusivityID,
                ExclusivityName = eDTO.ExclusivityName,
            };
        }

    }
}
