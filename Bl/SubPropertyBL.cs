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
                sp.SubPropertyID = sp.SubPropertyID;
                sp.PropertyID = sp.PropertyID;
                sp.num = sp.num;
                sp.IsRented = sp.IsRented;
                sp.Size = sp.Size;
                sp.RoomsNum = sp.RoomsNum;
                return true;
            }
            return false;
        }
            public static List<SubPropertyDTO> Search(Nullable<int> PropertyID, Nullable<int> num, Nullable<double> Size, Nullable<double> RoomsNum, Nullable<bool> IsRented)
        {
            List<SubProperty> subProperties = SubPropertyDAL.Search(PropertyID, num, Size, RoomsNum, IsRented);
            List<SubPropertyDTO> spdto = new List<SubPropertyDTO>();
            foreach (SubProperty sp in subProperties)
                spdto.Add(new SubPropertyDTO(sp));
            return spdto;
        }
    }
}
