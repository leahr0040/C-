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
    [RoutePrefix("api/users")]
    
    
    public class UserController : ApiController
    {

        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody]UserDTO ud)
        {
            bool b = Bl.UserBL.AddUser(ud);
            if (b)
                return Ok();
            return BadRequest();

        }
        [Route("UpdateUser")]
        public IHttpActionResult UpdateRental([FromBody]UserDTO ud)
        {
            bool b = Bl.UserBL.UpdateUser(ud);
            if (b)
                return Ok();
            return BadRequest();
        }
    }
}
