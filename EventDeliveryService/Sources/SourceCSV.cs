using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EventDeliveryService.Sources
{
    class SourceCSV : ISource
    {
        private Int32 sourceTypeId;

        private readonly Pipline _pipline;

        public SourceCSV(Pipline pipline)
        {
           sourceTypeId = 1;
            _pipline = pipline;
        }

        public Int32 GetSourceType()
        {
            return sourceTypeId;
        }
        public void Connect()
        {

        }
        public string[] Get()
        {

            IEnumerable<String> eBatch = File.ReadLines(_pipline.SourseConnection).Skip(_pipline.CurrentRow).Take(_pipline.CurrentRow + _pipline.BatchSize);

            //Int64 nextCurrentRow = _pipline.CurrentRow;

            return eBatch.ToArray();
        }

        public void Close()
        {

        }
    }
}
