using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
   public class DalProperties
    {
        public static bool ReProperties(Property pe)
        {
            using (ArgamanExpressEntities dbb = new ArgamanExpressEntities())
            {
                dbb.Properties.Add(pe);
                dbb.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
