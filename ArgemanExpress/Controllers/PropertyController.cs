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
    [RoutePrefix("api/Property")]//לכאן להוסיף את הניתוב לריאקט

    public class PropertyController : ApiController
    {
        [HttpPost]
        [Route("AddProperty")]// לבדוק איך קוראים בר
        public IHttpActionResult AddProperties([FromBody]PropertyDTO dt)
        {
            bool b = Bl.PropertyBL.AddProperty(dt);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("DeleteProperty")]// לבדוק איך קוראים בר
        public IHttpActionResult DeleteProperty(int id)
        {
            bool b = Bl.PropertyBL.DeleteProperty(id);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdateProperty")]
        public IHttpActionResult UpdateProperty([FromBody]PropertyDTO pd)
        {
            if (Bl.PropertyBL.UpdateProperty(pd))
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(string cityName, string streetName, string number, Nullable<int> floor, Nullable<double> roomsNum, Nullable<bool> isRented)
        {
            return Ok(Bl.PropertyBL.Search(cityName, streetName, number, floor, isRented));
        }
        [Route("GetAllProperties")]
        public IHttpActionResult GetAllProperties()
        {
            return Ok(Bl.PropertyBL.GetAllProperties());
        }
        [HttpPost]
        [Route("GetPropertyByID")]
        public IHttpActionResult GetPropertyByID([FromBody]int id)
        {
            return Ok(Bl.PropertyBL.GetPropertyByID(id));
        }
        [HttpPost]
        [Route("GetRentalByPropertyID")]
        public IHttpActionResult GetRentalByPropertyID([FromBody]int id)
        {
            return Ok(Bl.PropertyBL.GetRentalByPropertyID(id));
        }
        [HttpPost]
        [Route("GetRentalBySubPropertyID")]
        public IHttpActionResult GetRentalBySubPropertyID([FromBody]int id)
        {
            return Ok(Bl.PropertyBL.GetRentalBySubPropertyID(id));
        }
        [HttpPost]
        [Route("AddCity")]// לבדוק איך קוראים בר
        public IHttpActionResult AddCity([FromBody]string name)
        {
            bool b = Bl.PropertyBL.AddCity(name);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [Route("GetAllCities")]
        public IHttpActionResult GetAllCities()
        {
            return Ok(Bl.PropertyBL.GetAllCities());
        }
        [HttpPost]
        [Route("AddStreet")]// לבדוק איך קוראים בר
        public IHttpActionResult AddStreet([FromBody]StreetDTO sDTO)
        {
            bool b = Bl.PropertyBL.AddStreet(sDTO);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("GetStreetsByCityID")]// לבדוק איך קוראים בר
        public IHttpActionResult GetStreetsByCityID([FromBody]int CityId)
        {
            return Ok(Bl.PropertyBL.GetStreetsByCityID(CityId));
        }
        [HttpPost]
        [Route("GetStreetByID")]// לבדוק איך קוראים בר
        public IHttpActionResult GetStreetByID([FromBody]int streetId)
        {
            return Ok(Bl.PropertyBL.GetStreetByID(streetId));
        }
        [HttpPost]
        [Route("AddExclusivityPerson")]
        public IHttpActionResult AddExclusivityPerson([FromBody]ExclusivityPersonDTO ep)
        {
            return Ok(Bl.PropertyBL.AddExclusivityPerson(ep));
        }
    }
}
