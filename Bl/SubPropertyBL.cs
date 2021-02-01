using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;
using System.Diagnostics;

namespace Bl
{
    
    public class SubPropertyBL
    {
        public static bool AddSubProperty(SubPropertyDTO spd)
        {
            SubProperty sp = SubPropertyDTO.ToDal(spd);
            int id= SubPropertyDAL.AddSubProperty(sp);
            if (id != 0)
            {
                if (spd.Dock != null)

                {
                    Document doc = new Document();
                    doc.DocCoding = spd.Dock;
                    doc.DocUser = id;
                    doc.type = 5;
                    doc.DocName = spd.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));

                }
                return true;
            }
            return false;
        }
        public static bool DeleteSubProperty(int id)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                SubProperty p =db.SubProperties.Find(id);
                p.status = false;
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("deleteSubPropertyEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateSubProperty(SubPropertyDTO spd)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                SubProperty sp = db.SubProperties.Find(spd.SubPropertyID);


                sp.num = spd.num;
                sp.IsRented = spd.IsRented;
                sp.Size = spd.Size;
                sp.RoomsNum = spd.RoomsNum;
                if (spd.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = spd.Dock;
                    doc.DocUser = spd.SubPropertyID;
                    doc.type = 5;
                    doc.DocName = spd.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
               
                }
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("updateSubPropertyEror " + e.Message);
                return false;
            }
        }
        public static List<SubPropertyDTO> ConvertListToDTO(List<SubProperty> subProperties)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubPropertyDTO> spdto = new List<SubPropertyDTO>();
                foreach (SubProperty sp in subProperties)
                    spdto.Add(new SubPropertyDTO(sp));
                return spdto;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("convertListToDTOSubPropertyEror " + e.Message);
                return null;
            }
        }


        public static List<SubPropertyDTO> Search(SubPropertyDTO sd)
        {
            List<SubProperty> subProperties = SubPropertyDAL.Search(sd.PropertyID,sd.num,sd.Size,sd.RoomsNum);
            return ConvertListToDTO(subProperties);
        }
        public static List<SubPropertyDTO> GetAllSubProperties()
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> subProperties =(from sp in db.SubProperties  select sp).OrderBy(sp =>sp.IsRented).ToList();
                return ConvertListToDTO(subProperties);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getAllSubPropertyEror " + e.Message);
                return null;
            }
        }
        public static SubPropertyDTO GetSubPropertyByID(int id)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                SubProperty subProperty = db.SubProperties.Find(id);
                return new SubPropertyDTO(subProperty);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getSubPropertyByIDEror " + e.Message);
                return null;
            }
        }
        //public static List<SubPropertyDTO> GetSubPropertiesOfParentProperty(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        List<SubProperty> subProperties = (from sp in db.SubProperties where sp.PropertyID == id select sp).ToList();
        //        return ConvertListToDTO(subProperties);
        //    }
        //    return null;
        //}
    }
}
