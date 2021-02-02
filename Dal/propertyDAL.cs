using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Dal
{
    public class PropertyDAL
    {

        public static int AddProperty(Property pe)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    db.Properties.Add(pe);

                    db.SaveChanges();

                    return db.Properties.Max(i => i.PropertyID);
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addPropertyEror " + e.Message);
                return 0;
            }

        }
        public static bool DeleteProperty(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    Property p = db.Properties.Find(id);
                    p.status = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("deletePropertyEror " + e.Message);
                return false;
            }
        }
        public static bool UpdateProperty(Property pd)
        {
            try
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
        public static List<Property> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<bool> isRented)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<Property> pro;
                    pro = (from p in db.Properties where p.status == true select p).ToList();
                    if (cityName != null)
                        pro = (from p in pro where p.City.CityName.Contains(cityName) select p).ToList();
                    if (streetName != null)
                        pro = (from p in pro where p.Street.StreetName.Contains(streetName) select p).ToList();
                    if (number != null)
                        pro = (from p in pro where p.Number == number select p).ToList();
                    if (floor != null)
                        pro = (from p in pro where p.Floor == floor select p).ToList();
                    if (isRented != null)
                        pro = (from p in pro where p.IsRented == isRented select p).ToList();
                    pro = pro.OrderBy(p => p.City.CityName).OrderBy(p => p.Street.StreetName).ToList();
                    return pro;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("searchPropertyEror " + e.Message);
                return null;
            }
        }
        public static List<getAllProperties_Result> GetAllProperties()
        {
           
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    

                    List<getAllProperties_Result> pro = (from p in db.getAllProperties() select p).
                       OrderBy(p => p.CityID).OrderBy(p => p.StreetID).ToList();
                    return pro;
                }

            }
            catch (Exception e)

            {
                System.Diagnostics.Trace.TraceInformation("getAllPropertiesEror" + e.Message);
                return new List<PropertyDTO>();

            }
        }
        public static Property GetPropertyByID(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    Property property = db.Properties.Find(id);
                    return property;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("getPropertyByIDEror " + e.Message);
                return null;
            }
        }

        public static bool AddCity(City c)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {

                    c.CityId = db.Cities.Count() + 1;
                    db.Cities.Add(c);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addCityEror " + e.Message);
                return false;
            }
        }
        public static bool AddStreet(Street s)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    s.StreetID = db.Streets.Count() + 1;
                    db.Streets.Add(s);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("addStreetEror " + e.Message);
                return false;
            }
        }
        public static List<Street> GetStreetsByCityID(int id)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    City city = db.Cities.Find(id);
                    return city.Streets.ToList();
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetStreetbyCityIDEror " + e.Message);
                return null;
            }
        }

        public static List<getStreets_Result> GetAllStreets()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    db.Database.CommandTimeout = 300;
                    List<getStreets_Result> streets = db.getStreets().ToList();
                    return streets;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllStreetsEror " + e.Message);
                return null;
            }
        }
        public static List<getAllCities_Result> GetAllCities()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<getAllCities_Result> cities = db.getAllCities().OrderBy(i => i.CityName).ToList();
                    return cities;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllCitiesEror " + e.Message);
                return null;
            }
        }
        public static bool AddExclusivityPerson(Exclusivity e)
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    db.Exclusivitys.Add(e);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("AddExclusivityPersonEror " + ex.Message);
                return false;
            }
        }
        public static List<Exclusivity> GetAllExclusivityPoeple()
        {
            try
            {
                using (ArgamanExpressEntities db = new ArgamanExpressEntities())
                {
                    List<Exclusivity> ex = db.Exclusivitys.ToList();
                    return ex;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("GetAllExclusivityPoepleEror " + e.Message);
                return null;
            }
        }
    }
}
