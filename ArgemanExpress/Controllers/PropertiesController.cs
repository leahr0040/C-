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

    public class PropertiesController: ApiController
    {
        [Route("AddProperties")]// לבדוק איך קוראים בר
       public IHttpActionResult AddProperties([FromBody]DtoProperties dt)
        {
            bool b = Bl.BLAddProperties.Properties(dt);
            if (b == true)
                return Ok();
            return BadRequest();
        }
    }
}
