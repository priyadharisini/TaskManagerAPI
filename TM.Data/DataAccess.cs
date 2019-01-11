using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Entities;

namespace TM.Data
{
    public class DataAccess
    {
        TaskManagerEntitiesModel dbContext = new TaskManagerEntitiesModel();      


        public IQueryable<Task> Get()
        {
            return dbContext.Tasks.OrderByDescending( x => x.TaskId);
                    
        }

        public Task Get(int? id )
        {
            return dbContext.Tasks.FirstOrDefault(x => x.TaskId == id);
        }

        public int Add(TaskEntity taskEntity)
        {
            var parentId = taskEntity.ParentTask == "" ? 0 : 
                dbContext.Tasks.FirstOrDefault(x => x.TaskName == taskEntity.ParentTask).TaskId;
            var taskObj = new Task()
            {
                TaskName = taskEntity.TaskName,
                ParentId = parentId,
                StartDate = taskEntity.StartDate,
                EndDate = taskEntity.EndDate,
                Priority = taskEntity.Priority,
                Status = taskEntity.Status
                               
            };

            dbContext.Tasks.Add(taskObj);
            return  dbContext.SaveChanges();
        }

        public List<Task> Update(TaskEntity taskEntity)
        {
            try
            {
                var parentId = taskEntity.ParentTask == null ? 0 :
                   dbContext.Tasks.FirstOrDefault(x => x.TaskName == taskEntity.ParentTask).TaskId;
                var taskObj = new Task()
                {
                    TaskId = taskEntity.TaskId,
                    TaskName = taskEntity.TaskName,
                    ParentId = parentId,
                    StartDate = taskEntity.StartDate,
                    EndDate = taskEntity.EndDate,
                    Priority = taskEntity.Priority,
                    Status = taskEntity.Status
                };

                dbContext.Entry(taskObj).State = EntityState.Modified;
                var result = dbContext.SaveChanges();
                return (result > 0) ? Get().ToList() : new List<Task>();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public void Delete(TaskEntity taskEntity)
        {
            var task = dbContext.Tasks.FirstOrDefault(x => x.TaskId == taskEntity.TaskId);

            if (task != null)
            {
                dbContext.Entry(task).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }

        }

        public List<Task> EndTask(int TaskId)
        {
            var taskList = new List<Task>();
            var task = dbContext.Tasks.FirstOrDefault(x => x.TaskId == TaskId);

            if (task != null)
            {
                task.Status = "Closed";
                dbContext.Entry(task).State = EntityState.Modified;
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    taskList =  dbContext.Tasks.ToList();
                }
            }

            return taskList;
        }

        public List<Task> SearchTask(TaskEntity taskEntity)
        {
            var result = dbContext.Tasks.Where(x =>
                       (taskEntity.TaskName == string.Empty || x.TaskName.Contains(taskEntity.TaskName)));
            if (taskEntity.ParentTask != null)
            result = result.Where( x => (taskEntity.ParentTask == string.Empty || x.TaskName.Contains(taskEntity.ParentTask)));


           //|| (taskEntity.PriorityFrom == 0 || x.Priority > taskEntity.PriorityFrom)
           //|| (taskEntity.PriorityTo == 0 || x.Priority < taskEntity.PriorityTo)
           //|| (x.StartDate > taskEntity.StartDate.Date && x.EndDate < taskEntity.EndDate.Date));

            return result.ToList().Any() ? result.ToList() : new List<Task>();
        }

    }
}
