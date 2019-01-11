using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Business.Response
{
    public class TaskResponse
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }

        public int ParentTaskId { get; set; }

        public string ParentTask { get; set; }

        public int Priority { get; set; }

        public DateTime  StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string   Status { get; set; }

    }
}
