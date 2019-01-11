using System;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using TM.Business;
using TaskManager.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TM.Business.Response;

namespace TaskManagerServices.Tests
{
    [TestClass]
    public class TaskControllerTests
    {
        private Mock<ITaskManagerBusiness> _taskBusinessMock;

        private TaskController _taskController;


        [TestInitialize]
        public void TestSetUp()
        {
            _taskBusinessMock = new Mock<ITaskManagerBusiness>();

            _taskController = new TaskController(_taskBusinessMock.Object);
            _taskController.Request = new HttpRequestMessage();
            _taskController.Request.SetConfiguration(new HttpConfiguration());
        }

        [TestMethod]
        public void Verify_LoadAllTasks_Success()
        {
            var expectedTaskResponse = GetTasks();
            _taskBusinessMock.Setup(x => x.GetAllTask()).Returns(expectedTaskResponse);
            var executionResult = _taskController.Get();
            var actualResponse = executionResult.StatusCode;
            Assert.AreEqual(actualResponse, System.Net.HttpStatusCode.OK);
        }

       
        [TestMethod]
        public void Verify_LoadAllTaskById_Success()
        {
            var expectedTaskResponse = GetTasks().Find(x => x.TaskId ==1);
            _taskBusinessMock.Setup(x => x.GetTaskById(1)).Returns(expectedTaskResponse);
            var executionResult = _taskController.GetTaskById(1);
            var actualResponse = executionResult.StatusCode;
            Assert.AreEqual(actualResponse, System.Net.HttpStatusCode.OK);
        }

        private List<TaskResponse> GetTasks()
        {
            var tasks = new List<TaskResponse>();
            tasks.Add(new TaskResponse()
            {
                TaskId = 1,
                TaskName = "Task001",
                Priority = 21,
                StartDate = new DateTime(2019, 1, 02),
                EndDate = new DateTime(2019, 1, 02)
            });
           
            return tasks;
        }
    }
}
