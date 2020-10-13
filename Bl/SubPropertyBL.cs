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
            return SubPropertyDAL.AddSubProperty(sp);
        }
        public static bool UpdateSubProperty(SubPropertyDTO spd)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                SubProperty sp = db.SubProperties.Find(spd.SubPropertyID);


                sp.num = sp.num;
                sp.IsRented = sp.IsRented;
                sp.Size = sp.Size;
                sp.RoomsNum = sp.RoomsNum;
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
        public static List<SubPropertyDTO> Search(Nullable<int> PropertyID, Nullable<int> num, Nullable<double> Size, Nullable<double> RoomsNum)
        {
            List<SubProperty> subProperties = SubPropertyDAL.Search(PropertyID, num, Size, RoomsNum);
            return ConvertListToDTO(subProperties);
        }
        public static List<SubPropertyDTO> GetAllSubProperties()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<SubProperty> subProperties = db.SubProperties.ToList();
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
