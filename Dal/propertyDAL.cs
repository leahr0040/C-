using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal
{
    public class PropertyDAL
    {
        public static int AddProperty(Property pe)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Properties.Add(pe);

                db.SaveChanges();

                return db.Properties.Max(i => i.PropertyID);
            }
            return 0;

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
            public static bool UpdateProperty(Property pd)
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
            return false;
        }
        public static List<Property> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<bool> isRented)
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
            return null;
        }


        public static bool AddCity(City c)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {

                c.CityId = db.Cities.Count() + 1;
                db.Cities.Add(c);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool AddStreet(Street s)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                s.StreetID = db.Streets.Count() + 1;
                db.Streets.Add(s);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool AddExclusivityPerson(Exclusivity e)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Exclusivitys.Add(e);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
    