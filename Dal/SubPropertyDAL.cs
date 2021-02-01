using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dal
{
     public class SubPropertyDAL
    {
        public static int AddSubProperty(SubProperty sp)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                sp.status = true;
                db.SubProperties.Add(sp);
                db.SaveChanges();
                return db.SubProperties.Max(i => i.SubPropertyID);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("addSubPropertyEror " + e.Message);
                return 0;
            }
        }
        public static List<SubProperty> Search(Nullable<int> PropertyID, Nullable<int> num, Nullable<double> Size, Nullable<double> RoomsNum)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> sp = new List<SubProperty>();
                sp = (from sup in db.SubProperties where sup.status == true select sup).ToList();
                if (PropertyID != null && PropertyID != 0)
                    sp = (from s in sp where s.PropertyID == PropertyID select s).ToList();
                if (num != null && num != 0)
                    sp = (from s in sp where s.num == num select s).ToList();
                if (Size != null && Size != 0)
                    sp = (from s in sp where s.Size >= Size select s).ToList();
                if (RoomsNum != null && RoomsNum != 0)
                    sp = (from s in sp where s.RoomsNum >= RoomsNum select s).ToList();
                sp = sp.OrderBy(spr => spr.IsRented).ToList();
                return sp;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("searchSubPropertyEror " + e.Message);
                return null;
            }
        }
    }
}
