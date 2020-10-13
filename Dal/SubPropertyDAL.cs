using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
     public class SubPropertyDAL
    {
        public static int AddSubProperty(SubProperty sp)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                sp.status = true;
                db.SubProperties.Add(sp);
                db.SaveChanges();
                return (from sup in db.SubProperties where sup.PropertyID==sp.PropertyID && sup.num==sp.num select sup.SubPropertyID).FirstOrDefault();
            }
            return 0;
        }
        public static List<SubProperty> Search(Nullable<int> PropertyID,Nullable<int> num,Nullable<double> Size,Nullable<double> RoomsNum)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> sp = new List<SubProperty>();
                sp = (from sup in db.SubProperties where sup.status == true select sup).ToList();
                if (PropertyID != null)
                    sp = (from s in sp where s.PropertyID == PropertyID select s).ToList();
                if (num != null)
                    sp = (from s in sp where s.num == num select s).ToList();
                if (Size != null)
                    sp = (from s in sp where s.Size >= Size select s).ToList();
                if (RoomsNum != null)
                    sp = (from s in sp where s.RoomsNum >= RoomsNum select s).ToList();
               
                return sp;
            }
        }
    }
}
