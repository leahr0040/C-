using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dto;
namespace ArgemanExpress.Controllers
{
    //לכאן להוסיף את הניתוב לריאקט[RoutePrefix("api/")]

    public class PropertyController: ApiController
    {
        [Route("AddProperties")]// לבדוק איך קוראים בר
       public IHttpActionResult AddProperties([FromBody]PropertyDTO dt)
        {
            bool b = Bl.BLAddProperties.Properties(dt);
            if (b == true)
                return Ok();
            return BadRequest();
        }
    }
}
