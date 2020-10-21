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

    [RoutePrefix("api/SubProperty")]
    public class SubPropertyController : ApiController
    {
        [HttpPost]
        [Route("AddSubProperty")]
        public IHttpActionResult AddSubProperties([FromBody]SubPropertyDTO spd)
        {
            bool b = Bl.SubPropertyBL.AddSubProperty(spd);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdateSubProperty")]
        public IHttpActionResult UpdateSubProperty([FromBody]SubPropertyDTO spd)
        {
            if (Bl.SubPropertyBL.UpdateSubProperty(spd))
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("DeleteSubProperty")]
        public IHttpActionResult DeleteSubProperty([FromBody]int id)
        {
            if (Bl.SubPropertyBL.DeleteSubProperty(id))
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(Nullable<int> PropertyID, Nullable<int> num, Nullable<double> Size, Nullable<double> RoomsNum, Nullable<bool> IsRented)
        {
            return Ok(Bl.SubPropertyBL.Search(PropertyID, num, Size, RoomsNum));
        }

        [Route("GetAllSubProperties")]
        public IHttpActionResult GetAllSubProperties()
        {
            return Ok(Bl.SubPropertyBL.GetAllSubProperties());
        }
        [HttpPost]
        [Route("GetSubPropertyByID")]
        public IHttpActionResult GetSubPropertyByID([FromBody]int id)
        {
            return Ok(Bl.SubPropertyBL.GetSubPropertyByID(id));
        }
        [HttpPost]
        [Route("GetSubPropertiesOfParentProperty")]
        public IHttpActionResult GetSubPropertiesOfParentProperty([FromBody]int id)
        {
            return Ok(Bl.SubPropertyBL.GetSubPropertiesOfParentProperty(id));
        }
    }
}
