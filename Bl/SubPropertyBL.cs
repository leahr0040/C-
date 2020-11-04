using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;

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
                if(spd.Dock!=null)
            {
                Document doc = new Document();
                doc.DocCoding = spd.Dock;
                doc.DocUser = id;
                doc.type = 5;
                doc.DocName = spd.DocName;
                DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                
            }return true;
            }
            return false;
        }
        public static bool DeleteSubProperty(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                SubProperty p =db.SubProperties.Find(id);
                p.status = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateSubProperty(SubPropertyDTO spd)
        {
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
            }
            return false;
        }
        public static List<SubPropertyDTO> ConvertListToDTO(List<SubProperty> subProperties)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubPropertyDTO> spdto = new List<SubPropertyDTO>();
                foreach (SubProperty sp in subProperties)
                    spdto.Add(new SubPropertyDTO(sp));
                return spdto;
            }
            return null;
        }
        public static List<SubPropertyDTO> Search(SubPropertyDTO sd)
        {
            List<SubProperty> subProperties = SubPropertyDAL.Search(sd.PropertyID,sd.num,sd.Size,sd.RoomsNum);
            return ConvertListToDTO(subProperties);
        }
        public static List<SubPropertyDTO> GetAllSubProperties()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> subProperties =(from sp in db.SubProperties where sp.status==true select sp).OrderBy(sp =>sp.IsRented).ToList();
                return ConvertListToDTO(subProperties);
            }
        }
        public static SubPropertyDTO GetSubPropertyByID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
               SubProperty subProperty = db.SubProperties.Find(id);
                return new SubPropertyDTO(subProperty);
            }
        }
        public static List<SubPropertyDTO> GetSubPropertiesOfParentProperty(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> subProperties = (from sp in db.SubProperties where sp.PropertyID == id select sp).ToList();
                return ConvertListToDTO(subProperties);
            }
            return null;
        }
    }
}
