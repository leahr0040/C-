using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dto;

namespace ArgemanExpress.Controllers
{
    [RoutePrefix("api/Renter")]
    public class RenterController : ApiController
    {

        [Route("AddRenter")]
        public IHttpActionResult AddRenter([FromBody]UserDTO rd)
        {
            bool b = Bl.RenterBL.AddRenter(rd);
            if (b)
                return Ok();
            return BadRequest();

        }
        [Route("UpdateRenter")]
        public IHttpActionResult UpdateRental([FromBody]UserDTO rd)
        {
            bool b = Bl.RenterBL.UpdateRenter(rd);
            if (b)
                return Ok();
            return BadRequest();
        }
        [Route("Search")]
        public IHttpActionResult Search(string FirstName, string LastName, string SMS, string Email, string Phone, string UserName, string Password)
        {
            return Ok(Bl.RenterBL.Search(FirstName, LastName, SMS, Email, Phone, UserName, Password));
        }
        [Route("getRentalsbyRenterID")]
        public IHttpActionResult getRentalsbyRenterID(int id)//פרטי השכרה לפי איידי
        {

            return Ok(Bl.RenterBL.getRentalsbyRenterID(id));
        }
        [Route("getPropertiesbyRenterID")]
        public IHttpActionResult getPropertiesbyRenterID(int id)//דירות ששוכר לפי איידי
        {
            return Ok(Bl.RenterBL.getPropertiesbyRenterID(id));
        }
    }
}
