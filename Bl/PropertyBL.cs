using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Dal;
namespace Bl
{
    public class PropertyBL
    {
        public static bool AddProperty(PropertyDTO d)
        {
            Property p = PropertyDTO.Todal(d);
            int id = PropertyDAL.AddProperty(p);
            if (id != 0 && d.Dock!=null)
            {
                Document doc = new Document();
                doc.DocCoding = d.Dock;
                doc.DocUser = id;
                doc.type = 1;
                doc.DocName = d.DocName;
                DocumentBL.AddUserDocuments(new DocumentDTO(doc));
                return true;
            }
            return false;
        }
        public static bool AddExclusivityPerson(ExclusivityPersonDTO epDTO)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Exclusivitys.Add(ExclusivityPersonDTO.ToDAL(epDTO));
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool DeleteProperty(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property p = db.Properties.Find(id);
                p.status = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
            public static bool UpdateProperty(PropertyDTO pd)
        {
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
            return false;
        }
        public static List<PropertyDTO> ConvertListToDTO(List<Property> pro)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<PropertyDTO> prodto = new List<PropertyDTO>();
                foreach (Property p in pro)
                    prodto.Add(new PropertyDTO(p));
                return prodto;
            }
            return null;
        }
        public static List<PropertyDTO> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<bool> isRented)
        {

            List<Property> pro = PropertyDAL.Search(cityName, streetName, number, floor, isRented);
            return ConvertListToDTO(pro);
        }
        public static List<PropertyDTO> GetAllProperties()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Property> pro =(from p in  db.Properties where p.status==true select p).OrderBy(p=>p.City.CityName).OrderBy(p =>p.Street.StreetName).ToList();
                return ConvertListToDTO(pro);
            }
        }
        public static PropertyDTO GetPropertyByID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property property = db.Properties.Find(id);
                return new PropertyDTO(property);
            }
        }
        public static RentalDTO GetRentalByPropertyID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Rental rental = db.Rentals.FirstOrDefault(r => r.PropertyID == id);
                return new RentalDTO(rental);
            }
        }
        public static RentalDTO GetRentalBySubPropertyID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Rental rental = db.Rentals.FirstOrDefault(r => r.SubPropertyID == id);
                return new RentalDTO(rental);
            }
        }
        public static bool AddExclusivityPerson(string name)
        {
            ExclusivityPersonDTO epDTO = new ExclusivityPersonDTO();
            epDTO.ExclusivityName = name;
            return PropertyDAL.AddExclusivityPerson(ExclusivityPersonDTO.ToDAL(epDTO));
        }
        public static List<ExclusivityPersonDTO> GetAllExclusivityPoeple()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Exclusivity> ex = db.Exclusivitys.ToList();
                List<ExclusivityPersonDTO> exDTOs = new List<ExclusivityPersonDTO>();
                foreach (Exclusivity e in ex)
                {
                    exDTOs.Add(new ExclusivityPersonDTO(e));
                }
                return exDTOs;
            }
            return null;
        }
        public static bool AddCity(string name)
        {
            CityDTO cDTO = new CityDTO();
            cDTO.CityName = name;
            return PropertyDAL.AddCity(CityDTO.ToDAL(cDTO));
        }
        public static List<CityDTO> GetAllCities()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<City> cities = db.Cities.OrderBy(i=>i.CityName).ToList();
                List<CityDTO> cityDTOs = new List<CityDTO>();
                foreach (City city in cities)
                {
                    cityDTOs.Add(new CityDTO(city));
                }
                return cityDTOs;
            }
            return null;
        }
        public static bool AddStreet(StreetDTO sDTO)
        {
            return PropertyDAL.AddStreet(StreetDTO.ToDAL(sDTO));
        }
        public static List<StreetDTO> GetStreetsByCityID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                City city = db.Cities.Find(id);
                List<StreetDTO> streetDTOs = new List<StreetDTO>();
                foreach (Street street in city.Streets)
                {
                    streetDTOs.Add(new StreetDTO(street));
                }
                return streetDTOs;
            }
            return null;
        }
       
            public static List<StreetDTO> GetAllStreets()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Street> streets = db.Streets.OrderBy(i=>i.StreetName).ToList();
                List<StreetDTO> streetDTOs = new List<StreetDTO>();
                foreach (Street street in streets)
                {
                    streetDTOs.Add(new StreetDTO(street));
                }
              
                return streetDTOs;
            }
                return null;
        }
        public static StreetDTO GetStreetByID(int id)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Street street= db.Streets.Find(id);
                return new StreetDTO(street);
            }
            return null;
        }
        //public static List<PropertyDTO> AdvancedSearch(Nullable<int> propertyID, string owner, string cityName, string streetName, string number, Nullable<int> apartmentNum, Nullable<double> roomsNum, Nullable<double> size, Nullable<int> floor, Nullable<bool> isDivided, Nullable<double> managmentPayment, Nullable<bool> isPaid, Nullable<bool> isExclusivity, string exclusivity, Nullable<bool> isWarranty, Nullable<bool> isRented)
        //{
        //    List<Property> pro = PropertyDAL.AdvancedSearch(propertyID, owner, cityName, streetName, number, apartmentNum, roomsNum, size, floor, isDivided, managmentPayment, isPaid, isExclusivity, exclusivity, isWarranty, isRented);
        //    List<PropertyDTO> prodto = new List<PropertyDTO>();
        //    foreach (Property p in pro)
        //        prodto.Add(new PropertyDTO(p));
        //    return prodto;

        //}

    }
}
