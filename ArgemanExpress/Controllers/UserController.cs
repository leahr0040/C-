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



    [RoutePrefix("api/User")]
    
    

    public class UserController : ApiController
    {

        [HttpPost]
        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody] UserDTO ud)
        {
            bool b = Bl.UserBL.AddUser(ud);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser([FromBody]int id)
        {
            bool b = Bl.UserBL.DeleteUser(id);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateRental([FromBody] UserDTO ud)
        {
            bool b = Bl.UserBL.UpdateUser(ud);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("returnuser")]
        public IHttpActionResult ee([FromBody]string username, [FromBody]string password)
        {
            return Ok(Bl.UserBL.Return_Details_user(username, password));

        }
        [HttpPost]
        [Route("returnuserproperty")]
        public IHttpActionResult returnuserproperty([FromBody]string username, [FromBody]string password)
        {
            return Ok(Bl.UserBL.Return_Details_use(username, password));
        }
        [HttpPost]
        [Route("GetUserDocuments")]
        public IHttpActionResult GetUserDocuments([FromBody]int id, [FromBody]int type)
        {
            return Ok(Bl.DocumentBL.GetUserDocuments(id, type));
        }
        [HttpPost]
        [Route("Ifhaveuse")]
        public IHttpActionResult ret([FromBody]string username, [FromBody]string password)
        {
            return Ok(Bl.UserBL.Haveuserforpassword(username, password));
        }
        [HttpPost]
        [Route("forgotpassword")]
        public IHttpActionResult m([FromBody]string username, [FromBody]string email)
        {
            return Ok(Bl.UserBL.Forgotpassword(username, email));
        }

    } 

}
