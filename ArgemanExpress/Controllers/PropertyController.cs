using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dto;
namespace ArgemanExpress.Controllers
{
    [RoutePrefix("api/Property")]//לכאן להוסיף את הניתוב לריאקט

    public class PropertyController : ApiController
    {
        [Route("AddProperty")]// לבדוק איך קוראים בר
        public IHttpActionResult AddProperties([FromBody]PropertyDTO dt)
        {
            bool b = Bl.PropertyBL.AddProperty(dt);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [Route("UpdateProperty")]
        public IHttpActionResult UpdateProperty([FromBody]PropertyDTO pd)
        {
            if (Bl.PropertyBL.UpdateProperty(pd))
                return Ok();
            return BadRequest();
        }
        [Route("Search")]
        public IHttpActionResult Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<double> roomsNum, Nullable<bool> isRented)
        {
            return Ok(Bl.PropertyBL.Search(cityName, streetName, number, floor, roomsNum, isRented));
        }
        //[Route("AdvancedSearch")]
        //public IHttpActionResult AdvancedSearch(Nullable<int> propertyID, string owner, string cityName, string streetName, string number, Nullable<int> apartmentNum, Nullable<double> roomsNum, Nullable<double> size, Nullable<int> floor, Nullable<bool> isDivided, Nullable<double> managmentPayment, Nullable<bool> isPaid, Nullable<bool> isExclusivity, string exclusivity, Nullable<bool> isWarranty, Nullable<bool> isRented)
        //{
        //    return Ok(Bl.PropertyBL.AdvancedSearch(propertyID, owner, cityName, streetName, number, apartmentNum, roomsNum, size, floor, isDivided, managmentPayment, isPaid, isExclusivity, exclusivity, isWarranty, isRented));
        //}
    }
}
