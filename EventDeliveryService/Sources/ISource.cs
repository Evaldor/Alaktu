using System;
using System.Collections.Generic;
using System.Text;

namespace EventDeliveryService.Sources
{
    interface ISource
    {
        Int32 GetSourceType();
        void Connect();
        string[] Get();
        void Close();
    }
}
