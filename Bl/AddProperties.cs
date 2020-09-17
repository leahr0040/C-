using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Dal;
namespace Bl
{
  public class BLAddProperties
    {
        public static bool Properties(DtoProperties d)
        {
            Property p = DtoProperties.Todal(d);
            return DalProperties.ReProperties(p);
        }
    }
}
