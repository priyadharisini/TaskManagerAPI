using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Configuration;
using StructureMap.Pipeline;
using TM.Business;

namespace TM.Infrastructure
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<ITaskManagerBusiness>().Use<TaskManagerBusiness>();
        }
    }
}
