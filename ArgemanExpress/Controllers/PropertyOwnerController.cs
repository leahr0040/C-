using Dto;
using System.Web.Http;
using System.Web.Http.Cors;

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
            bool b = Bl.PropertyOwnerBL.AddPropertyOwner(po);
            if (b)
                return Ok();
            return BadRequest();

        }
        [HttpPost]
        [Route("DeletePropertyOwner")]
        public IHttpActionResult DeletePropertyOwner(IdDto id)
        {
            bool b = Bl.PropertyOwnerBL.DeletePropertyOwner(id.id);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdatePropertyOwner")]
        public IHttpActionResult UpdatePropertyOwner([FromBody] PropertyOwnerDTO po)
        {
            bool b = Bl.PropertyOwnerBL.UpdatePropertyOwner(po);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] PropertyOwnerDTO pd)
        {
            return Ok(Bl.PropertyOwnerBL.Search(pd.OwnerFirstName, pd.OwnerLastName, pd.Phone, pd.Email));
        }

        [Route("GetAllOwners")]
        public IHttpActionResult GetAllOwners()
        {
            return Ok(Bl.PropertyOwnerBL.getAllOwners());
        }
        [HttpPost]
        [Route("GetOwnerByID")]
        public IHttpActionResult GetOwnerByID([FromBody] IdDto id)
        {
            return Ok(Bl.PropertyOwnerBL.GetOwnerByID(id.id));
        }
        [HttpPost]
        [Route("GetPropertiesbyOwnerID")]
        public IHttpActionResult GetPropertiesbyOwnerID([FromBody] IdDto id)//דירות ששוכר לפי איידי
        {
            return Ok(Bl.PropertyOwnerBL.getPropertiesbyOwnerID(id.id));
        }
        [HttpPost]
        [Route("getRentalsbyOwnerID")]
        public IHttpActionResult getRentalsbyOwnerID([FromBody] IdDto id)//פרטי השכרה לפי איידי
        {
            return Ok(Bl.PropertyOwnerBL.getRentalsbyOwnerID(id.id));
        }

    }
}
