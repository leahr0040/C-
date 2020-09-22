using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;

namespace Bl
{
    public class PropertyOwnerBL
    {
        public static bool AddPropertyOwner(PropertyOwnerDTO pod)
        {

            PropertiesOwner poDal = PropertyOwnerDTO.ToDal(pod);
            return PropertyOwnerDAL.AddPropertyOwner(poDal);
        }
        public static bool UpdatePropertyOwner(PropertyOwnerDTO po)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                PropertiesOwner pro = db.PropertiesOwners.Find(po.OwnerID);
                pro.OwnerFirstName = po.OwnerFirstName;
                pro.OwnerLastName = po.OwnerLastName;
                pro.Phone = po.Phone;
                pro.Email = po.Email;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static List<PropertyOwnerDTO> Search(string OwnerFirstName, string OwnerLastName, string Phone, string Email)
        {
            
                List<PropertiesOwner> po=PropertyOwnerDAL.Search(OwnerFirstName,OwnerLastName,Phone,Email);
               
                List<PropertyOwnerDTO> podto = new List<PropertyOwnerDTO>();
                foreach (PropertiesOwner p in po)
                    podto.Add(new PropertyOwnerDTO(p));
                return podto;
           
        }
    }
}
