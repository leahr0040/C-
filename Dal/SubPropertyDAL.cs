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
        public static bool DeleteSubProperty(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    SubProperty p = db.SubProperties.Find(id);
                    p.status = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deleteSubPropertyEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateSubProperty(SubProperty spd)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    SubProperty sp = db.SubProperties.Find(spd.SubPropertyID);
                    sp.num = spd.num;
                    sp.IsRented = spd.IsRented;
                    sp.Size = spd.Size;
                    sp.RoomsNum = spd.RoomsNum;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updateSubPropertyEror " + e.Message);
                return false;
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
        public static List<SubProperty> GetAllSubProperties()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<SubProperty> subProperties = (from sp in db.SubProperties select sp).OrderBy(sp => sp.IsRented).ToList();
                    return subProperties;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllSubPropertyEror " + e.Message);
                return null;
            }
        }
        public static SubProperty GetSubPropertyByID(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    SubProperty subProperty = db.SubProperties.Find(id);
                    return subProperty;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getSubPropertyByIDEror " + e.Message);
                return null;
            }
        }
    }
}
