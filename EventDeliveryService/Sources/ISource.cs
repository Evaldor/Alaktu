using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventDeliveryService.Sources
{
    interface ISource
    {
        Int64 GetSourceType();
        void Connect();
        EventBatch GetEventBatch();
        void Close();
    }
}
