using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class PropertyDTO
    {

        public int PropertyID { get; set; }
        public int OwnerID { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StreetID { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public Nullable<double> Size { get; set; }//--גודל דירה
        public Nullable<int> Floor { get; set; }
        public bool IsDivided { get; set; }
        public Nullable<double> ManagmentPayment { get; set; }//--דמי ניהול
        public bool IsPaid { get; set; }
        public bool IsExclusivity { get; set; }
        public Nullable<int> ExclusivityID { get; set; }
        public bool IsWarranty { get; set; }// האם באחריות?
        public bool IsRented { get; set; }
        public Nullable<double> RoomsNum { get; set; }
        public Nullable<int> ApartmentNum { get; set; }

        public PropertyDTO()
        {

        }

        public PropertyDTO(Property p)
        {
            this.PropertyID = p.PropertyID;
            this.OwnerID = p.OwnerID;
            this.CityID = p.CityID;
            this.CityName = p.CityName;
            this.StreetID = p.StreetID;
            this.StreetName = p.StreetName;
            this.Number = p.Number;
            this.Size = p.Size;
            this.Floor = p.Floor;
            this.IsDivided = p.IsDivided;
            this.ManagmentPayment = p.ManagmentPayment;
            this.IsPaid = p.IsPaid;
            this.IsExclusivity = p.IsExclusivity;
            this.ExclusivityID = p.ExclusivityID;
            this.IsWarranty = p.IsWarranty;
            this.IsRented = p.IsRented;
            this.RoomsNum = p.RoomsNum;
            this.ApartmentNum = p.ApartmentNum;
        }
        public static Property Todal(PropertyDTO dd)
        {
            return new Property
            {
                OwnerID = dd.OwnerID,
                PropertyID = dd.PropertyID,
                CityID = dd.CityID,
                CityName = dd.CityName,
                StreetID = dd.StreetID,
                Number = dd.Number,
                Size = dd.Size,
                Floor = dd.Floor,
                IsDivided = dd.IsDivided,
                ManagmentPayment = dd.ManagmentPayment,
                IsPaid = dd.IsPaid,
                IsRented = dd.IsRented,
                IsExclusivity = dd.IsExclusivity,
                ExclusivityID = dd.ExclusivityID,
                IsWarranty = dd.IsWarranty,
                RoomsNum = dd.RoomsNum,
                ApartmentNum = dd.ApartmentNum
        };

        }
        //public int OwnerID { get; set; }
        //public int PropertyID { get; set; }
        //public int CityID { get; set; }
        //public string CityName { get; set; }
        //public int StreetID { get; set; }
        //public string StreetName { get; set; }
        //public string Number { get; set; }
        //public float Size { get; set; }
        //public int Floor { get; set; }
        //public bool IsDivided { get; set; }
        //public float ManagmentPayment { get; set; }
        //public bool IsPaid { get; set; }
        //public bool IsRented { get; set; }
        //public bool IsExclusivity { get; set; }
        //public int ExclusivityID { get; set; }
        //public bool IsWarranty { get; set; }
        //public PropertyDTO()
        //{ }

        //public PropertyDTO(Property p)
        //{
        //    OwnerID = p.OwnerID;
        //    PropertyID = p.PropertyID;
        //    CityID = p.CityID;
        //    CityName = p.CityName;
        //    StreetID = p.StreetID;
        //    Number = p.Number;
        //    Size = p.Size;
        //    Floor = p.Floor;
        //    IsDivided = p.IsDivided;
        //    ManagmentPayment = p.ManagmentPayment;
        //    IsPaid = p.IsPaid;
        //    IsRented = p.IsRented;
        //    IsExclusivity = p.IsExclusivity;
        //    ExclusivityID = p.ExclusivityID;
        //    IsWarranty = p.IsWarranty;

        //}
        
    }
}
