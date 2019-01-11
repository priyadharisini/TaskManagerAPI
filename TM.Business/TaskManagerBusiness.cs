using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Business.Request;
using TM.Business.Response;
using TM.Data;
using TM.Entities;

namespace TM.Business
{
    public interface ITaskManagerBusiness
    {
        int AddNewTask(TaskRequest request);

        List<TaskResponse> GetAllTask();

        List<TaskResponse> UpdateTask(TaskRequest taskRequest);


        List<TaskResponse> EndTask(int Taskid);


        List<TaskResponse> SearchTask(TaskRequest taskRequest);

        TaskResponse GetTaskById(int TaskId);
    }


    public class TaskManagerBusiness : ITaskManagerBusiness
    {
        DataAccess dbAccess = new DataAccess();
        public int AddNewTask(TaskRequest request)
        {
            var task = new TaskEntity()
            {
                ParentTask = request.ParentTaskId == null ? "" : request.ParentTaskId,
                TaskName = request.TaskName,
                Priority = request.Priority,
                StartDate = request.StartDate == DateTime.MinValue ? DateTime.Now : request.StartDate,
                EndDate = request.EndDate == DateTime.MinValue ? DateTime.Now : request.EndDate,
                Status = "Open"
            };

            return dbAccess.Add(task);
        }


        public List<TaskResponse> GetAllTask()
        {
            var taskData = dbAccess.Get().ToList();
            var tasks = new List<TaskResponse>();
            
            foreach (var taskitem in taskData)
            {
                var parentTask = taskitem.ParentId == 0 ? string.Empty : dbAccess.Get().FirstOrDefault(x => x.TaskId == taskitem.ParentId).TaskName;

                var task = new TaskResponse()
                {
                    TaskId = taskitem.TaskId,
                    TaskName = taskitem.TaskName,
                    EndDate = (DateTime)taskitem.EndDate,
                    StartDate = (DateTime)taskitem.StartDate,
                    ParentTask = parentTask,
                    Priority = taskitem.Priority ?? 0,
                    Status = taskitem.Status                    
                };

                tasks.Add(task);
            }

            return tasks;
        }

        public List<TaskResponse> UpdateTask(TaskRequest taskRequest)
        {
            var taskResponseList = new List<TaskResponse>();
            var taskEntity = new TaskEntity()
            {
                TaskId = taskRequest.TaskId,
                EndDate = taskRequest.EndDate,
                ParentTask = taskRequest.ParentTaskId,
                Priority = taskRequest.Priority,
                StartDate = taskRequest.StartDate,
                TaskName = taskRequest.TaskName,
                Status = "Open"                
            };

            var taskDetails = dbAccess.Update(taskEntity);
            if (taskDetails == null) return taskResponseList;

            foreach (var task in taskDetails)
            {
                var parentTaskName = taskDetails.FirstOrDefault(x => x.ParentId == task.ParentId).TaskName;
                var response = new TaskResponse()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    ParentTask = parentTaskName,
                    EndDate = (DateTime)task.EndDate,
                    Priority = task.Priority ?? 0,
                    ParentTaskId = task.ParentId ?? 0,
                    StartDate = (DateTime)task.StartDate,
                    Status = task.Status

                };

                taskResponseList.Add(response);
            }

            return taskResponseList;
        }

        public TaskResponse GetTaskById(int TaskId)
        {
            var response = new TaskResponse();

            var task = dbAccess.Get(TaskId);

            var parentTask = task.ParentId == 0 ? string.Empty : dbAccess.Get(task.ParentId).TaskName;
            if (task != null)
            {
                response =  new TaskResponse()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    ParentTask = parentTask,
                    EndDate = (DateTime)task.EndDate,
                    Priority = task.Priority ?? 0,
                    ParentTaskId = task.ParentId ?? 0,
                    StartDate = (DateTime)task.StartDate,
                    Status = task.Status
                };
            }

            return response;
        }


        public List<TaskResponse> EndTask(int TaskId)
        {
            var taskResponseList = new List<TaskResponse>();
            var taskDetails = dbAccess.EndTask(TaskId);
            foreach (var task in taskDetails)
            {
                var parentTaskName = taskDetails.FirstOrDefault(x => x.ParentId == task.ParentId).TaskName;
                var response = new TaskResponse()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    ParentTask = parentTaskName,
                    EndDate = (DateTime)task.EndDate,
                    Priority = task.Priority ?? 0,
                    ParentTaskId = task.ParentId ?? 0,
                    StartDate = (DateTime)task.StartDate,
                    Status = task.Status

                };

                taskResponseList.Add(response);
            }

            return taskResponseList;

        }


        public List<TaskResponse> SearchTask(TaskRequest taskRequest)
        {
            var taskResponseList = new List<TaskResponse>();

            var taskEntity = new TaskEntity()
            {
                TaskId = taskRequest.TaskId,
                EndDate = taskRequest.EndDate,
                ParentTask = taskRequest.ParentTaskId,
                Priority = taskRequest.Priority,
                StartDate = taskRequest.StartDate,
                TaskName = taskRequest.TaskName,
                PriorityFrom = taskRequest.PriorityFrom,

            };
            var taskDetails = dbAccess.SearchTask(taskEntity);
            foreach (var task in taskDetails)
            {
                var parentTaskName = taskDetails.FirstOrDefault(x => x.ParentId == task.ParentId).TaskName;
                var response = new TaskResponse()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    ParentTask = parentTaskName,
                    EndDate = (DateTime)task.EndDate,
                    Priority = task.Priority ?? 0,
                    ParentTaskId = task.ParentId ?? 0,
                    StartDate = (DateTime)task.StartDate,
                    Status = task.Status

                };

                taskResponseList.Add(response);
            }

            return taskResponseList;
        }

    }
}
