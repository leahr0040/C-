using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using Dto;

namespace ArgemanExpress.Controllers
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]

   [RoutePrefix("api/Rental")]
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
            return Ok(Bl.RentalBL.Search(propertyID, user, enteryDate, endDate));
        }
        [Route("GetAllRentals")]
        public IHttpActionResult GetAllRentals()
        {//Bl.RentalBL.GetAllRentals()
            return Ok("aaa");
        }
    }
}
