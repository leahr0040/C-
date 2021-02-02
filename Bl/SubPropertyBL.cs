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
                    DocumentBL.AddUserDocuments(new DocumentDTO(id,spd.Dock,5,spd.DocName));

                }
                return true;
            }
            return false;
        }
        public static bool DeleteSubProperty(int id)
        {
            try {
                return SubPropertyDAL.DeleteSubProperty(id);
                    }
            catch (Exception e)
            {
                Trace.TraceInformation("deleteSubPropertyEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateSubProperty(SubPropertyDTO spd)
        {
            try { 
           
               
                if (spd.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(spd.SubPropertyID,spd.Dock, 5 ,spd.DocName));
               
                }
               
                return SubPropertyDAL.UpdateSubProperty(SubPropertyDTO.ToDal(spd));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updateSubPropertyEror " + e.Message);
                return false;
            }
        }
       


        public static List<SubPropertyDTO> Search(SubPropertyDTO sd)
        {
            List<SubProperty> subProperties = SubPropertyDAL.Search(sd.PropertyID,sd.num,sd.Size,sd.RoomsNum);
            return SubPropertyDTO. ConvertListToDTO(subProperties);
        }
        public static List<SubPropertyDTO> GetAllSubProperties()
        {
            try { 
             List<SubProperty> subProperties =SubPropertyDAL.GetAllSubProperties();
                return SubPropertyDTO.ConvertListToDTO(subProperties);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllSubPropertyEror " + e.Message);
                return null;
            }
        }
        public static SubPropertyDTO GetSubPropertyByID(int id)
        {
            try { 
            
                SubProperty subProperty = SubPropertyDAL.GetSubPropertyByID(id);
                return new SubPropertyDTO(subProperty);
            }
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
