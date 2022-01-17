using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventDeliveryService.Sources
{
    interface ISource
    {
        Int32 GetSourceType();
        void Connect();
        DataTable Get();
        void Close();
    }
}
