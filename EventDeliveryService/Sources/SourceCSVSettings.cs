using System;
using System.Collections.Generic;
using System.Text;

namespace EventDeliveryService.Sources
{
    class SourceCSVSettings
    {
        public bool IsWithHeader { get; set; }
        public string ColumnDelimeter { get; set; }
        public string LineDelimeter { get; set; }
    }
}
