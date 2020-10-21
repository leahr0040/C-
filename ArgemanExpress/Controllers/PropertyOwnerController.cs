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
    [RoutePrefix("api/PropertyOwner")]
    public class PropertyOwnerController : ApiController
    {
        [HttpPost]
        [Route("AddPropertyOwner")]
        public IHttpActionResult AddPropertyOwner(PropertyOwnerDTO po)
        {
            //  bool b = Bl.PropertyOwnerBL.AddPropertyOwner(po);
            // if (b)
            //     return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeletePropertyOwner")]
        public IHttpActionResult DeletePropertyOwner(int id)
        {
            bool b = Bl.PropertyOwnerBL.DeletePropertyOwner(id);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdatePropertyOwner")]
        public IHttpActionResult UpdatePropertyOwner([FromBody]PropertyOwnerDTO po)
        {
            bool b = Bl.PropertyOwnerBL.UpdatePropertyOwner(po);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search([FromBody]string OwnerFirstName, [FromBody]string OwnerLastName, [FromBody]string Phone, [FromBody]string Email)
        {
            return Ok(Bl.PropertyOwnerBL.Search(OwnerFirstName, OwnerLastName, Phone, Email));
        }

        [Route("GetAllOwners")]
        public IHttpActionResult GetAllOwners()
        {
            return Ok(Bl.PropertyOwnerBL.getAllOwners());
        }
        [HttpPost]
        [Route("GetOwnerByID")]
        public IHttpActionResult GetOwnerByID([FromBody]int id)
        {
            return Ok(Bl.PropertyOwnerBL.GetOwnerByID(id));
        }
        [HttpPost]
        [Route("GetPropertiesbyOwnerID")]
        public IHttpActionResult GetPropertiesbyOwnerID([FromBody]int id)//דירות ששוכר לפי איידי
        {
            return Ok(Bl.PropertyOwnerBL.getPropertiesbyOwnerID(id));
        }
        [HttpPost]
        [Route("getRentalsbyOwnerID")]
        public IHttpActionResult getRentalsbyOwnerID([FromBody]int id)//פרטי השכרה לפי איידי
        {
            return Ok(Bl.PropertyOwnerBL.getRentalsbyOwnerID(id));
        }

    }
}
