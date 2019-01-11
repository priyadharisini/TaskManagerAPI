using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TM.Business;
using TM.Business.Request;
using TM.Business.Response;

namespace TaskManager.Controllers
{
    [RoutePrefix("v1/Tasks")]
    public class TaskController : ApiController
    {
        private readonly ITaskManagerBusiness _tmBusiness;

        public TaskController(ITaskManagerBusiness tmBusiness)
        {
            _tmBusiness = tmBusiness;
        }

        [Route("addnewtask")]
        [HttpPost]
        public HttpResponseMessage AddNewTask([FromBody] TaskRequest request)
        {
            var tmBusiness = new TaskManagerBusiness();
            var response = new HttpResponseMessage();
            var result = tmBusiness.AddNewTask(request);
            if (result > 0)
                return Request.CreateResponse(HttpStatusCode.OK, "New task details added successfully");

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to add task details,Please try again later");
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var tmBusiness = new TaskManagerBusiness();
            var result = tmBusiness.GetAllTask();
            if (result != null)
               return Request.CreateResponse(HttpStatusCode.OK, result);

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to get all task details,Please try again later");

        }


        [Route("update/{TaskId}")]
        [HttpPost]
        public HttpResponseMessage UpdateTask([FromBody] TaskRequest request, int TaskId)
        {
            var tmBusiness = new TaskManagerBusiness();
            if (request == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to process your request, Please enter a valid request.");

            request.TaskId = TaskId;

            var response = tmBusiness.UpdateTask(request);

            if (response != null)
                return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to get task details,Please try again later");
        }


        [Route("{TaskId}")]
        public HttpResponseMessage GetTaskById([FromUri] int TaskId)
        {
            var tmBusiness = new TaskManagerBusiness();
            if (TaskId == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please enter a valid task id");

            var response = tmBusiness.GetTaskById(TaskId);

            if (response != null)
                return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to get TaskId details,Please try again later");
        }

        [Route("end/{TaskId}")]
        [HttpPost]
        public HttpResponseMessage EndTaskById([FromUri] int TaskId)
        {
            var tmBusiness = new TaskManagerBusiness();
            if (TaskId == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please enter a valid task id");

            var response = tmBusiness.EndTask(TaskId);

            if (response != null)
                return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to get TaskId details,Please try again later");
        }

        [Route("search")]
        [HttpPost]
        public HttpResponseMessage SearchTask([FromBody] TaskRequest taskParameter)
        {
            if (taskParameter == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please enter a valid task parameters to search");

            var tmBusiness = new TaskManagerBusiness();
            var response = tmBusiness.SearchTask(taskParameter);

            if (response != null)
                return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to get TaskId details,Please try again later");
        }
    }
}
