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
                //pro.OwnerFirstName = po.OwnerFirstName;
                //pro.OwnerLastName = po.OwnerLastName;
                pro.Phone = po.Phone;
                pro.Email = po.Email;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static List<PropertyOwnerDTO> ConvertListToDTO(List<PropertiesOwner> po)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertyOwnerDTO> podto = new List<PropertyOwnerDTO>();
                foreach (PropertiesOwner p in po)
                    podto.Add(new PropertyOwnerDTO(p));
                return podto;
            }
            return null;
        }
        public static List<PropertyOwnerDTO> Search(string OwnerFirstName, string OwnerLastName, string Phone, string Email)
        {
            
                List<PropertiesOwner> po=PropertyOwnerDAL.Search(OwnerFirstName,OwnerLastName,Phone,Email);       
                return ConvertListToDTO(po);
           
        }
        public static List<PropertyOwnerDTO> getAllOwners()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertiesOwner> owners = db.PropertiesOwners.ToList();
               return ConvertListToDTO(owners);
            }
        }
        public static PropertyOwnerDTO GetOwnerByID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                PropertiesOwner owner = db.PropertiesOwners.FirstOrDefault(x=>x.OwnerID==id);
                return new PropertyOwnerDTO(owner);
            }
        }

        public static List<PropertyDTO> getPropertiesbyOwnerID(int id)//דירות שמשכיר לפי איידי
        {
            List<Property> properties = PropertyOwnerDAL.getPropertiesbyOwnerID(id);
            return Bl.PropertyBL.ConvertListToDTO(properties);

        }
        public static List<RentalDTO> getRentalsbyOwnerID(int id)//פרטי השכרה לפי איידי
        {
            List<Rental> renters = PropertyOwnerDAL.getRentalsbyOwnerID(id);
            return Bl.RentalBL.ConvertListToDTO(renters);
        }


    }
}
