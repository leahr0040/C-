using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dto;

namespace ArgemanExpress.Controllers
{   [RoutePrefix("api/Rental")]
    public class RentalController : ApiController
    {
        [Route("AddRental")]
        public IHttpActionResult AddRental([FromBody]RentalDTO rd)
        {
            bool b = Bl.RentalBL.AddRental(rd);
            if (b)
                return Ok();
            return BadRequest();

        }
        [Route("UpdateRental")]
        public IHttpActionResult UpdateRental([FromBody]RentalDTO rd)
        {
            bool b = Bl.RentalBL.UpdateRental(rd);
            if (b)
                return Ok();
            return BadRequest();
        }
        [Route("Search")]
        public IHttpActionResult Search(Nullable<int> propertyID, Nullable<bool> subPropertyID, String user, Nullable<double> rentPayment, Nullable<int> paymentTypeID, Nullable<DateTime> enteryDate, Nullable<DateTime> endDate, Nullable<bool> contactRenew)
        {
            return Ok(Bl.RentalBL.Search(propertyID, subPropertyID, user, rentPayment, paymentTypeID, enteryDate, endDate, contactRenew));
        }
       
    }
}
