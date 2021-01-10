using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventDeliveryService
{
    class NamedTask
    {
        public string taskName { get; set; }
        public Task task { get; set; }

        public NamedTask(string name)
        {
            taskName = name;
        }
    }
}
