﻿using System;
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
            return PropertyDAL.AddProperty(p);
        }

        public static bool UpdateProperty(PropertyDTO pd)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                Property p = db.Properties.Find(pd.PropertyID);
               
                p.OwnerID = pd.OwnerID;
                p.CityID = pd.CityID;
                p.CityName = pd.CityName;
                p.StreetID = pd.StreetID;
                p.StreetName = pd.StreetName;
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
                //p.RoomsNum = pd.RoomsNum;
                //p.ApartmentNum = pd.ApartmentNum;

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
                foreach (Property p in db.Properties)
                    prodto.Add(new PropertyDTO(p));
                return prodto;
            }
            return null;
        }
        public static List<PropertyDTO> Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<double> roomsNum, Nullable<bool> isRented)
        {

            List<Property> pro = PropertyDAL.Search(cityName, streetName, number, floor, roomsNum,isRented);
            return ConvertListToDTO(pro);
        }
        public static List<PropertyDTO> GetAllProperties()
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                List<Property> pro = db.Properties.ToList();
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
