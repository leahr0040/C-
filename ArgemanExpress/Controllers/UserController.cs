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
        [Route("adduser")]
        public IHttpActionResult adduser([FromBody] UserDTO ud)
        {
        bool b = Bl.UserBL.AddUser(ud);
           if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser([FromBody]IdDto id)
        {
            bool b = Bl.UserBL.DeleteUser(id.id);
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
        public IHttpActionResult ee([FromBody]Dtostrstr ds)
        {
            return Ok(Bl.UserBL.Return_Details_user(ds.username, ds.passemail));

        }
        //[HttpPost]
        //[Route("returnuserproperty")]
        //public IHttpActionResult returnuserproperty([FromBody]Dtostrstr dp)
        //{ 
        //    return Ok(Bl.UserBL.Return_Details_use(dp.username, dp.passemail));
        //}
        [HttpPost]
        [Route("GetUserDocuments")]
        public IHttpActionResult GetUserDocuments([FromBody]Dtointint di)
        {
            return Ok(Bl.DocumentBL.GetUserDocuments(di.id, di.type));
        }
        [Route("GetAllDocuments")]
        public IHttpActionResult GetAllDocuments()
        {
            return Ok(Bl.DocumentBL.GetAllDocuments());
        }
        [HttpPost]
        [Route("DeleteUserDocument")]
        public IHttpActionResult DeleteUserDocument([FromBody] DocumentDTO doc)
        {
            return Ok(Bl.DocumentBL.DeleteUserDocument(doc));
        }
        [HttpPost]
        [Route("Ifhaveuse")]
        public IHttpActionResult ret([FromBody]Dtostrstr dn)
        {
            return Ok(Bl.UserBL.Haveuserforpassword(dn.username,dn.passemail));
        }
        [HttpPost]
        [Route("forgotpassword")]
        public IHttpActionResult m([FromBody]Dtostrstr dx)
        {
            return Ok(Bl.UserBL.Forgotpassword(dx.username, dx.passemail));
        }
        [HttpGet]
        [Route("SendAllRenter")]
        public IHttpActionResult SendAllRenter()
        {
            return Ok(Bl.UserBL.MailToAllUser());
        }

    } 

}
