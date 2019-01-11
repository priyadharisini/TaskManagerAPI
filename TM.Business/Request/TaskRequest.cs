using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Business.Request
{
    public class TaskRequest
    {
        public int TaskId { get; set; }
        public string   TaskName { get; set; }

        public string ParentTaskId { get; set; }

        public int Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PriorityFrom { get; set; }

        public int PriorityTo { get; set; }

    }
}
