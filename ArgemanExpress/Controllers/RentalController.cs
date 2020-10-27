using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using Dto;
using Microsoft.Ajax.Utilities;

namespace ArgemanExpress.Controllers
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]

   [RoutePrefix("api/Rental")]
    public class RentalController : ApiController
    {
        [HttpPost]
        [Route("AddRental")]
        public IHttpActionResult AddRental([FromBody]RentalDTO rd)
        {
            bool b = Bl.RentalBL.AddRental(rd);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("AddRental")]
        public IHttpActionResult AddRental([FromBody]IdDto id)
        {
            bool b = Bl.RentalBL.DeleteRental(id.id);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("UpdateRental")]
        public IHttpActionResult UpdateRental([FromBody]RentalDTO rd)
        {
            bool b = Bl.RentalBL.UpdateRental(rd);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(DtoRent rd)
        {
            return Ok(Bl.RentalBL.Search(rd.PropertyID, rd. user,rd.EnteryDate,rd.EndDate));
        }
        [HttpGet]
        [Route("GetAllRentals")]
        public IHttpActionResult GetAllRentals()
        {
            return Ok(Bl.RentalBL.GetAllRentals());
        }
    }
}
