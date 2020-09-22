﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal
{
    public class PropertyDAL
    {
        public static bool AddProperty(Property pe)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                db.Properties.Add(pe);
                db.SaveChanges();
                return true;
            }
            return false;

        }
        
        public static List<Property> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<double> roomsNum)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Property> pro;
                pro = (from p in db.Properties select p).ToList();
                if (cityName != null)
                    pro = (from p in pro where p.CityName.Contains(cityName) select p).ToList();
                if (streetName != null)
                    pro = (from p in pro where p.StreetName.Contains(streetName) select p).ToList();
                if (number != null)
                    pro = (from p in pro where p.Number.Contains(number) select p).ToList();
                if (floor != null)
                    pro = (from p in pro where p.Floor == floor select p).ToList();

                return pro;
            }
            return null;
        }
    }
    public static List<Property> AdvancedSearch(Nullable<int> propertyID, string owner, string cityName, string streetName, string number, Nullable<int> apartmentNum, Nullable<double> roomsNum, Nullable<double> size, Nullable<int> floor, Nullable<bool> isDivided, Nullable<double> managmentPayment, Nullable<bool> isPaid, Nullable<bool> isExclusivity, string exclusivity, Nullable<bool> isWarranty, Nullable<bool> isRented)
    {
        using (ArgamanExpressEntities db = new ArgamanExpressEntities())
        {
            List<Property> pro;
            pro = (from p in db.Properties select p).ToList();
            if (propertyID != null)
                pro = (from p in pro where p.PropertyID == propertyID select p).ToList();
            if (owner != null)
                pro = (from p in pro where (p.PropertiesOwner.OwnerFirstName + ' ' + p.PropertiesOwner.OwnerFirstName).Contains(owner.Trim()) select p).ToList();
            if (cityName != null)
                pro = (from p in pro where p.CityName.Contains(cityName) select p).ToList();
            if (streetName != null)
                pro = (from p in pro where p.StreetName.Contains(streetName) select p).ToList();
            if (number != null)
                pro = (from p in pro where p.Number.Contains(number) select p).ToList();
            if (size != null)
                pro = (from p in pro where p.Size >= size select p).ToList();
            if (floor != null)
                pro = (from p in pro where p.Floor == floor select p).ToList();
            if (isDivided != null)
                pro = (from p in pro where p.IsDivided == isDivided select p).ToList();
            if (managmentPayment != null)
                pro = (from p in pro where p.ManagmentPayment >= managmentPayment select p).ToList();
            if (isPaid != null)
                pro = (from p in pro where p.IsPaid == isPaid select p).ToList();
            if (isExclusivity != null)
                pro = (from p in pro where p.IsExclusivity == isExclusivity select p).ToList();
            if (exclusivity != null)
                pro = (from p in pro where p.Exclusivity.ExclusivityName.Contains(exclusivity) select p).ToList();
            if (isWarranty != null)
                pro = (from p in pro where p.IsWarranty == isWarranty select p).ToList();
            if (isRented != null)
                pro = (from p in pro where p.IsRented == isRented select p).ToList();
            if (roomsNum != null)
                pro = (from p in pro where p.RoomsNum > roomsNum select p).ToList();
            if (apartmentNum != null)
                pro = (from p in pro where p.ApartmentNum == apartmentNum select p).ToList();

            return pro;
        }
        return null;
    }
}