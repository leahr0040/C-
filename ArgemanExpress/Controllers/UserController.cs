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
    [RoutePrefix("api/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class UserController : ApiController
    {

        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody] UserDTO ud)
        {
            bool b = Bl.UserBL.AddUser(ud);
            if (b)
                return Ok();
            return BadRequest();

        }
        [Route("UpdateUser")]
        public IHttpActionResult UpdateRental([FromBody] UserDTO ud)
        {
            bool b = Bl.UserBL.UpdateUser(ud);
            if (b)
                return Ok();
            return BadRequest();
        }

        [Route("returnuser")]
        public IHttpActionResult ee(string username, string password)
        {
            return Ok(Bl.UserBL.Return_Details_user(username, password));

        }
        [Route("returnuserproperty")]
        public IHttpActionResult returnuserproperty(string username, string password)
        {
            return Ok(Bl.UserBL.Return_Details_user(username,password));

        }
    } 

}
