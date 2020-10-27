using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Dto;

namespace ArgemanExpress.Controllers
{   [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/task")]
    //[Authorize(Roles =]
    public class TaskController : ApiController
    {
        [HttpPost]
        [Route("AddTask")]// לבדוק איך קוראים בר
        public IHttpActionResult AddTask([FromBody]TaskDTO dt)
        {
            bool b = Bl.TaskBL.AddTask(dt);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("DeleteTask")]
        public IHttpActionResult DeleteTask(IdDto id)
        {
            bool b = Bl.TaskBL.DeleteTask(id.id);
            if (b == true)
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdateTask")]
        public IHttpActionResult UpdateTask([FromBody]TaskDTO pd)
        {
            if (Bl.TaskBL.UpdateTask(pd))
                return Ok();
            return BadRequest();
        }
        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(TaskDTO td)
        {

            return Ok(Bl.TaskBL.Search(td.TaskTypeId,td.ClassificationID,td.DateForHandling,td.IsHandled));

        }
        [Route("GetAllTasks")]
        public IHttpActionResult GetAllTasks()
        {
            return Ok(Bl.TaskBL.GetAllTasks());
        }
        [Route("GetTimePassedTasks")]
        public IHttpActionResult GetTimePassedTasks()
        {
            return Ok(Bl.TaskBL.GetTimePassedTasks());
        }
        [HttpPost]
        [Route("gettypename")]
        public IHttpActionResult gettypename(IdDto id)
        {
            return Ok(Bl.TaskBL.GetTypeName(id.id));
        }
        [Route("GetAllClassificationTypes")]
        public IHttpActionResult GetAllClassificationTypes()
        {
            return Ok(Bl.TaskBL.GetAllClassificationTypes());
        }
        [Route("GetAllTaskTypes")]
        public IHttpActionResult GetAllTaskTypes()
        {
            return Ok(Bl.TaskBL.GetAllTaskTypes());
        }
        [HttpPost]
        [Route("GetTaskId")]
        public IHttpActionResult GetTaskId([FromBody]IdDto i)
        {
            return Ok(Bl.TaskBL.ReturnTaskbyid(i.id));
        }

    }
}
