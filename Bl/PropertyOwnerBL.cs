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
    public class PropertyOwnerBL
    {
        public static bool AddPropertyOwner(PropertyOwnerDTO pod)
        {

            PropertiesOwner poDal = PropertyOwnerDTO.ToDal(pod);
            int id = PropertyOwnerDAL.AddPropertyOwner(poDal);
            if (id != 0)
            {
                if (pod.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(id, pod.Dock, 2, pod.DocName));
                }
                return true;
            }
            return false;
        }
        public static bool DeletePropertyOwner(int id)
        {
            try
            {
                return PropertyOwnerDAL.DeletePropertyOwner(id);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deletePropertyOwnerEror " + e.Message);
                return false;
            }
        }
        public static bool UpdatePropertyOwner(PropertyOwnerDTO po)
        {
            try
            {
                if (po.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(po.OwnerID, po.Dock, 2, po.DocName));
                    
                }
                return PropertyOwnerDAL.UpdatePropertyOwner(PropertyOwnerDTO.ToDal(po));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("updatePropertyOwnerEror " + e.Message);
                return false;
            }
        }


        public static List<PropertyOwnerDTO> Search(string OwnerFirstName, string OwnerLastName, string Phone, string Email)
        {

            List<PropertiesOwner> po = PropertyOwnerDAL.Search(OwnerFirstName, OwnerLastName, Phone, Email);
            return PropertyOwnerDTO.ConvertListToDTO(po);

        }
        public static List<PropertyOwnerDTO> getAllOwners()
        {
            try
            {
               
                    List<getAllPropertiesOwners_Result> owners = PropertyOwnerDAL.getAllOwners();
                    return PropertyOwnerDTO.ConvertListToDTO(owners);
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getAllOwnersblEror " + e.Message);
                return null;
            }
        }
        //public static PropertyOwnerDTO GetOwnerByID(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        PropertiesOwner owner = db.PropertiesOwners.FirstOrDefault(x=>x.OwnerID==id);
        //        return new PropertyOwnerDTO();
        //    }
        //}

        //public static List<PropertyDTO> getPropertiesbyOwnerID(int id)//דירות שמשכיר לפי איידי
        //{
        //    List<Property> properties = PropertyOwnerDAL.getPropertiesbyOwnerID(id);
        //    return Bl.PropertyBL.ConvertListToDTO(properties);

        //}
        //public static List<RentalDTO> getRentalsbyOwnerID(int id)//פרטי השכרה לפי איידי
        //{
        //    List<Rental> renters = PropertyOwnerDAL.getRentalsbyOwnerID(id);
        //    return Bl.RentalBL.ConvertListToDTO(renters);
        //}



    }
}
