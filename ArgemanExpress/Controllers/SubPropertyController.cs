using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dto;

namespace ArgemanExpress.Controllers
{
    [RoutePrefix("api/SubProperty")]
    public class SubPropertyController : ApiController
    {
        [Route("AddSubProperty")]
        public IHttpActionResult AddSubProperties([FromBody]SubPropertyDTO spd)
        {
            bool b = Bl.SubPropertyBL.AddSubProperty(spd);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [Route("UpdateSubProperty")]
        public IHttpActionResult UpdateSubProperty([FromBody]SubPropertyDTO spd)
        {
            if (Bl.SubPropertyBL.UpdateSubProperty(spd))
                return Ok();
            return BadRequest();
        }
        [Route("Search")]
        public IHttpActionResult Search(Nullable<int> PropertyID, Nullable<int> num, Nullable<double> Size, Nullable<double> RoomsNum, Nullable<bool> IsRented)
        {
            return Ok(Bl.SubPropertyBL.Search(PropertyID, num, Size, RoomsNum, IsRented));
        }
       
    }
}
