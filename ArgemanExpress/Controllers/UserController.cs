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

        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody] UserDTO ud)
        {
            bool b = Bl.UserBL.AddUser(ud);
            if (b)
                return Ok();
            return BadRequest();

        }
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            bool b = Bl.UserBL.DeleteUser(id);
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
           return Ok(Bl.UserBL.Return_Details_user(username,password));

        }
        [Route("returnuserproperty")]
        public IHttpActionResult returnuserproperty(string username, string password)
        {
            return Ok(Bl.UserBL.Return_Details_use(username,password));

        }
        [Route("GetUserDocuments")]
        public IHttpActionResult GetUserDocuments(int id,int type)
        {
            return Ok(Bl.DocumentBL.GetUserDocuments(id, type));
        }
        [Route("Ifhaveuse")]
        public IHttpActionResult ret(string username, string password)
        {
            return Ok(Bl.UserBL.Haveuserforpassword(username, password));
        }
        [Route("forgotpassword")]
        public IHttpActionResult m(string username, string email)
        {
            return Ok(Bl.UserBL.Forgotpassword(username,email));
        }

    } 

}
