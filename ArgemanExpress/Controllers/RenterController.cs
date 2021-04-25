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
            bool b = Bl.UserBL.AddUser(rd);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeleteRenter")]
        public IHttpActionResult DeleteRenter(IdDto id)
        {
            bool b = Bl.RenterBL.DeleteRenter(id.id);
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
        public IHttpActionResult Search(UserDTO ud)
        {
            return Ok(Bl.RenterBL.Search(ud.FirstName,ud.LastName,ud.SMS,ud.Email,ud.Phone));
        }
        [Route("GetAllRenters")]
        public IHttpActionResult GetAllRenters()
        {
             return Ok(Bl.RenterBL.GetAllRenters());
            
        }
        //[HttpPost]
        //[Route("GetRenterByID")]
        //public IHttpActionResult GetRenterByID([FromBody]IdDto id)
        //{
        //    return Ok(Bl.RenterBL.GetRenterByID(id.id));
        //}
        //[HttpPost]
        //[Route("getRentalsbyRenterID")]
        //public IHttpActionResult getRentalsbyRenterID([FromBody]IdDto id)//פרטי השכרה לפי איידי
        //{

        //    return Ok(Bl.RenterBL.getRentalsbyRenterID(id.id));
        //}
        [HttpPost]
        [Route("getPropertiesbyRenterID")]
        public IHttpActionResult getPropertiesbyRenterID([FromBody]IdDto id)//דירות ששוכר לפי איידי
        {
            return Ok(Bl.RenterBL.getPropertiesbyRenterID(id.id));
        }
    }
}
