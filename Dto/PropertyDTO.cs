using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
        public Nullable<bool> IsRented { get; set; }
        public Nullable<double> RoomsNum { get; set; }
        public Nullable<int> ApartmentNum { get; set; }
        public string Dock { get; set; }
        public string DocName { get; set; }
        public Nullable<bool> status { get; set; }
        public PropertyDTO()
        {

        }

        public PropertyDTO(Property p)
        {
            this.PropertyID = p.PropertyID;
            this.OwnerID = p.OwnerID;
            this.CityID = p.CityID;
            this.StreetID = p.StreetID;
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
            status = p.status;
        }

        public PropertyDTO(getAllProperties_Result p)
        {
            this.PropertyID = p.PropertyID;
            this.OwnerID = p.OwnerID;
            this.CityID = p.CityID;
            this.StreetID = p.StreetID;
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
            status = p.status;
        }
        public static Property Todal(PropertyDTO dd)
        {
            return new Property
            {
                OwnerID = dd.OwnerID,
                PropertyID = dd.PropertyID,
                CityID = dd.CityID,
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
                ApartmentNum = dd.ApartmentNum,
                 status = dd.status

        };

        }
        public static List<PropertyDTO> ConvertListToDTO(List<Property> pro)
        {
            try
            {
                    List<PropertyDTO> prodto = new List<PropertyDTO>();
                    foreach (Property p in pro)
                        prodto.Add(new PropertyDTO(p));
                    return prodto;
            }
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
                
                    List<PropertyDTO> prodto = new List<PropertyDTO>();
                    foreach (getAllProperties_Result p in pro)
                        prodto.Add(new PropertyDTO(p));
                    return prodto;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceInformation("convertListToDtoProperty" + e.Message + " 109");

                return null;
            }

        }
    }
}
