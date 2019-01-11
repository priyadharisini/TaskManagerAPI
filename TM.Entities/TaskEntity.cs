using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TM.Entities
{
    public class TaskEntity
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int ParentTaskId { get; set; }

        public string   ParentTask { get; set; }

        public int Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string   Status { get; set; }

        public int PriorityFrom { get; set; }

        public int PriorityTo { get; set; }

    }
}
