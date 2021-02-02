using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Dal;
using System.Diagnostics;

namespace Bl
{
    public class PropertyBL
    {
        public static bool AddProperty(PropertyDTO d)
        {
            d.status = true;
            Property p = PropertyDTO.Todal(d);
            int id = PropertyDAL.AddProperty(p);
            if (id != 0)
            {
                if (d.Dock != null)

                {
                    Document doc = new Document();
                    doc.DocCoding = d.Dock;
                    doc.DocUser = id;
                    doc.type = 1;
                    doc.DocName = d.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                }
                return true;
            }
            return false;
        }

        public static bool DeleteProperty(int id)
        {
            return PropertyDAL.DeleteProperty(id);
        }
        public static bool UpdateProperty(PropertyDTO pd)
        {
            try
            {
                
                if (pd.Dock != null)
                {
                    DocumentBL.AddUserDocuments(new DocumentDTO(pd.PropertyID, pd.Dock, 1, pd.DocName));
                }

                return PropertyDAL.UpdateProperty(PropertyDTO.Todal(pd));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdatePropertyblEror " + e.Message);
                return false;
            }
        }

        public static List<PropertyDTO> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<bool> isRented)
        {

            List<Property> pro = PropertyDAL.Search(cityName, streetName, number, floor, isRented);
            return PropertyDTO.ConvertListToDTO(pro);
        }
        public static List<PropertyDTO> GetAllProperties()
        {
           
            try
            {

                List<getAllProperties_Result> pro = PropertyDAL.GetAllProperties();
                    return PropertyDTO.ConvertListToDTO(pro);
                
            }
            catch (Exception e)

            {
                System.Diagnostics.Trace.TraceInformation("getAllPropertiesEror" + e.Message);
                return new List<PropertyDTO>();

            }
        }
        public static PropertyDTO GetPropertyByID(int id)
        {
            try
            {
                    Property property = PropertyDAL.GetPropertyByID(id);
                    return new PropertyDTO(property);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getPropertyByIDblEror " + e.Message);
                return null;
            }
        }
        //public static RentalDTO GetRentalByPropertyID(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        Rental rental = db.Rentals.FirstOrDefault(r => r.PropertyID == id);
        //        return new RentalDTO(rental);
        //    }
        //}
        //public static RentalDTO GetRentalBySubPropertyID(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        Rental rental = db.Rentals.FirstOrDefault(r => r.SubPropertyID == id);
        //        return new RentalDTO(rental);
        //    }
        //}
        public static bool AddExclusivityPerson(string name)
        {
            ExclusivityPersonDTO epDTO = new ExclusivityPersonDTO();
            epDTO.ExclusivityName = name;
            return PropertyDAL.AddExclusivityPerson(ExclusivityPersonDTO.ToDAL(epDTO));
        }
        public static List<ExclusivityPersonDTO> GetAllExclusivityPoeple()
        {
            try
            {

                List<Exclusivity> ex = PropertyDAL.GetAllExclusivityPoeple();
                List<ExclusivityPersonDTO> exDTOs = new List<ExclusivityPersonDTO>();
                foreach (Exclusivity e in ex)
                {
                    exDTOs.Add(new ExclusivityPersonDTO(e));
                }
                return exDTOs;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllExclusivityPoepleEror " + e.Message);
                return null;
            }
        }
        public static bool AddCity(string name)
        {
            CityDTO cDTO = new CityDTO();
            cDTO.CityName = name;
            return PropertyDAL.AddCity(CityDTO.ToDAL(cDTO));
        }
        public static List<CityDTO> GetAllCities()
        {
            try
            {
                List<getAllCities_Result> cities = PropertyDAL.GetAllCities();
                List<CityDTO> cityDTOs = new List<CityDTO>();
                foreach (getAllCities_Result city in cities)
                {
                    cityDTOs.Add(new CityDTO(city));
                }
                return cityDTOs;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllCitiesblEror " + e.Message);
                return null;
            }
        }
        public static bool AddStreet(StreetDTO sDTO)
        {
            return PropertyDAL.AddStreet(StreetDTO.ToDAL(sDTO));
        }
        public static List<StreetDTO> GetStreetsByCityID(int id)
        {
            try
            {
               
                    List<Street> streets = PropertyDAL.GetStreetsByCityID(id);
                    List<StreetDTO> streetDTOs = new List<StreetDTO>();
                    foreach (Street street in streets)
                    {
                        streetDTOs.Add(new StreetDTO(street));
                    }
                    return streetDTOs;
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetStreetbyCityIDEror " + e.Message);
                return null;
            }
        }

        public static List<StreetDTO> GetAllStreets()
        {
            try
            {

                List<getStreets_Result> streets = PropertyDAL.GetAllStreets();

                List<StreetDTO> streetDTOs = new List<StreetDTO>();
                foreach (getStreets_Result street in streets)
                {
                    streetDTOs.Add(new StreetDTO(street));
                }

                return streetDTOs;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllStreetsEror " + e.Message);
                return null;
            }
        }
        //public static StreetDTO GetStreetByID(int id)
        //{
        //    using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        //    {
        //        Street street= db.Streets.Find(id);
        //        return new StreetDTO(street);
        //    }
        //    return null;
        //}

    }
}
