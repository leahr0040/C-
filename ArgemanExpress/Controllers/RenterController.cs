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

    [RoutePrefix("api/Renter")]
    public class RenterController : ApiController
    {
        [HttpPost]
        [Route("AddRenter")]
        public IHttpActionResult AddRenter([FromBody]UserDTO rd)
        {
            bool b = Bl.RenterBL.AddRenter(rd);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeleteRenter")]
        public IHttpActionResult DeleteRenter(int id)
        {
            bool b = Bl.RenterBL.DeleteRenter(id);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("UpdateRenter")]
        public IHttpActionResult UpdateRental([FromBody]UserDTO rd)
        {
            bool b = Bl.RenterBL.UpdateRenter(rd);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(string FirstName, string LastName, string SMS, string Email, string Phone)
        {
            return Ok(Bl.RenterBL.Search(FirstName, LastName, SMS, Email, Phone));
        }
        [Route("GetAllRenters")]
        public IHttpActionResult GetAllRenters()
        {
             return  Ok(Bl.RenterBL.GetAllRenters());
            
        }
        [HttpPost]
        [Route("GetRenterByID")]
        public IHttpActionResult GetRenterByID([FromBody]int id)
        {
            return Ok(Bl.RenterBL.GetRenterByID(id));
        }
        [HttpPost]
        [Route("getRentalsbyRenterID")]
        public IHttpActionResult getRentalsbyRenterID([FromBody]int id)//פרטי השכרה לפי איידי
        {

            return Ok(Bl.RenterBL.getRentalsbyRenterID(id));
        }
        [HttpPost]
        [Route("getPropertiesbyRenterID")]
        public IHttpActionResult getPropertiesbyRenterID([FromBody]int id)//דירות ששוכר לפי איידי
        {
            return Ok(Bl.RenterBL.getPropertiesbyRenterID(id));
        }
    }
}
