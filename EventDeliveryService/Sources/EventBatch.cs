using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventDeliveryService
{
    class EventBatch
    {
        public DataTable Batch { get; set; }

        public Int32 LastRowNumber { get; set; }
    }
}
