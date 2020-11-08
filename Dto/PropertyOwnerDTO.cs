﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Dto
{
    public class PropertyOwnerDTO
    {
        public int OwnerID { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Dock { get; set; }
        public string DocName { get; set; }
        public bool status { get; set; }
        public PropertyOwnerDTO()
        {

        }
        public PropertyOwnerDTO(PropertiesOwner po)
        {
            OwnerID = po.OwnerID;
            OwnerFirstName = po.OwnerFirstName;
            OwnerLastName = po.OwnerLastName;
            Phone = po.Phone;
            Email = po.Email;
            status = true;
           
        }
        public PropertyOwnerDTO(getAllPropertiesOwners_Result po)
        {
            OwnerID = po.OwnerID;
            OwnerFirstName = po.OwnerFirstName;
            OwnerLastName = po.OwnerLastName;
            Phone = po.Phone;
            Email = po.Email;
            status = true;

        }
        public static PropertiesOwner ToDal(PropertyOwnerDTO po)
        {
            return new PropertiesOwner
            {
                OwnerFirstName = po.OwnerFirstName,
                OwnerLastName = po.OwnerLastName,
                Phone = po.Phone,
                Email = po.Email

            };
        }
    }
}
