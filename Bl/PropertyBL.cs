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
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property p = db.Properties.Find(id);
                p.status = false;
                db.SaveChanges();
                return true;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("deletePropertyEror " + e.Message);
                return false;
            }
        }
            public static bool UpdateProperty(PropertyDTO pd)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property p = db.Properties.Find(pd.PropertyID);
               
                p.OwnerID = pd.OwnerID;
                p.CityID = pd.CityID;
                p.StreetID = pd.StreetID;
                p.Number = pd.Number;
                p.Size = pd.Size;
                p.Floor = pd.Floor;
                p.IsDivided = pd.IsDivided;
                p.ManagmentPayment = pd.ManagmentPayment;
                p.IsPaid = pd.IsPaid;
                p.IsExclusivity = pd.IsExclusivity;
                p.ExclusivityID = pd.ExclusivityID;
                p.IsWarranty = pd.IsWarranty;
                p.IsRented = pd.IsRented;
                p.RoomsNum = pd.RoomsNum;
                p.ApartmentNum = pd.ApartmentNum;
                if (pd.Dock != null)
                {
                    Document doc = new Document();
                    doc.DocCoding = pd.Dock;
                    doc.DocUser = pd.PropertyID;
                    doc.type = 1;
                    doc.DocName = pd.DocName;
                    DocumentBL.AddUserDocuments(new DocumentDTO(doc));

                }
                db.SaveChanges();
                return true;
            }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("UpdatePropertyEror " + e.Message);
                return false;
            }
        }
        public static List<PropertyDTO> ConvertListToDTO(List<Property> pro)
        {
            try {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertyDTO> prodto = new List<PropertyDTO>();
                foreach (Property p in pro)
                    prodto.Add(new PropertyDTO(p));
                return prodto;
            } }
            catch (Exception e)
            {
                Trace.TraceInformation("PropertyConvertListToDTOEror " + e.Message);
                return null;
            }
        }
        public static List<PropertyDTO> ConvertListToDTO(List<getAllProperties_Result> pro)
        {
            System.Diagnostics.Trace.TraceInformation("ConvertListToDTO");
            try
            {
                 using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertyDTO> prodto = new List<PropertyDTO>();
                foreach (getAllProperties_Result p in pro)
                    prodto.Add(new PropertyDTO(p));
                return prodto;
            }
            return null;
            }
            catch(Exception e)
            {
                System.Diagnostics.Trace.TraceInformation("convertListToDtoProperty"+ e.Message +" 109");

                return null;
            }
           
        }
        public static List<PropertyDTO> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<bool> isRented)
        {

            List<Property> pro = PropertyDAL.Search(cityName, streetName, number, floor, isRented);
            return ConvertListToDTO(pro);
        }
        public static List<PropertyDTO> GetAllProperties()
        {
            System.Diagnostics.Trace.TraceInformation("come in GetAllProperties BLL");
            try
            {  
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                System.Diagnostics.Trace.TraceInformation("117");

                List<getAllProperties_Result> pro =(from p in  db.getAllProperties() select p).
                   OrderBy(p => p.CityID).OrderBy(p => p.StreetID).ToList();
                return ConvertListToDTO(pro);
            }

            }catch(Exception e)

            {
                System.Diagnostics.Trace.TraceInformation("getAllPropertiesEror"+e.Message);
                return new List<PropertyDTO>();

            }

        }
        public static PropertyDTO GetPropertyByID(int id)
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property property = db.Properties.Find(id);
                return new PropertyDTO(property);
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("getPropertyByIDEror " + e.Message);
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
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Exclusivity> ex = db.Exclusivitys.ToList();
                List<ExclusivityPersonDTO> exDTOs = new List<ExclusivityPersonDTO>();
                foreach (Exclusivity e in ex)
                {
                    exDTOs.Add(new ExclusivityPersonDTO(e));
                }
                return exDTOs;
            }}
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
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<getAllCities_Result> cities = db.getAllCities().OrderBy(i=>i.CityName).ToList();
                List<CityDTO> cityDTOs = new List<CityDTO>();
                foreach (getAllCities_Result city in cities)
                {
                    cityDTOs.Add(new CityDTO(city));
                }
                return cityDTOs;
            }}
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllCitiesEror " + e.Message);
                return null;
            }
        }
        public static bool AddStreet(StreetDTO sDTO)
        {
            return PropertyDAL.AddStreet(StreetDTO.ToDAL(sDTO));
        }
        public static List<StreetDTO> GetStreetsByCityID(int id)
        {
            try {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                City city = db.Cities.Find(id);
                List<StreetDTO> streetDTOs = new List<StreetDTO>();
                foreach (Street street in city.Streets)
                {
                    streetDTOs.Add(new StreetDTO(street));
                }
                return streetDTOs;
            } }
            catch (Exception e)
            {
                Trace.TraceInformation("GetStreetbyCityIDEror " + e.Message);
                return null;
            }
        }
       
            public static List<StreetDTO> GetAllStreets()
        {
            try { 
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Database.CommandTimeout=300;
                List< getStreets_Result> streets = db.getStreets().ToList();
                
                List<StreetDTO> streetDTOs = new List<StreetDTO>();
                foreach (getStreets_Result street in streets)
                {
                    streetDTOs.Add(new StreetDTO(street));
                }
              
                return streetDTOs;
            }}
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
