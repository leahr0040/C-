using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class DtoProperties
    {

        public int OwnerID { get; set; }
        public int PropertyID { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StreetID { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public float Size { get; set; } //--גודל דירה
        public int Floor { get; set; }
        public bool IsDivided { get; set; }
        public float ManagmentPayment { get; set; } //--דמי ניהול
        public bool IsPaid { get; set; }
        public bool IsRented { get; set; }
        public bool IsExclusivity { get; set; }
        public int ExclusivityID { get; set; }
        public bool IsWarranty { get; set; }// האם באחריות?
        public DtoProperties()
        { }

        public DtoProperties(Property p)
        {
            OwnerID = p.OwnerID;
            PropertyID = p.PropertyID;
            CityID = p.CityID;
            CityName = p.CityName;
            StreetID = p.StreetID;
            Number = p.Number;
            //Size = p.Size;
            //Floor = p.Floor;
            IsDivided = p.IsDivided;
            //ManagmentPayment = p.ManagmentPayment;
            IsPaid = p.IsPaid;
            IsRented = p.IsRented;
            IsExclusivity = p.IsExclusivity;
            //ExclusivityID =p.ExclusivityID;
            IsWarranty = p.IsWarranty;

        }
        public static Property Todal(DtoProperties dd)
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
            };

        }
    }
}
