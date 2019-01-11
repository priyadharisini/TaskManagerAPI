namespace TM.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TaskManagerEntitiesModel : DbContext
    {
        // Your context has been configured to use a 'TaskManagerEntitiesModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TM.Data.TaskManagerEntitiesModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TaskManagerEntitiesModel' 
        // connection string in the application configuration file.
        public TaskManagerEntitiesModel()
            : base("name=TaskManagerEntitiesModel")
        {
            Database.SetInitializer<TaskManagerEntitiesModel>(new CreateDatabaseIfNotExists<TaskManagerEntitiesModel>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }

    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<System.DateTime> StartDate{ get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Status { get; set; }
    }
}