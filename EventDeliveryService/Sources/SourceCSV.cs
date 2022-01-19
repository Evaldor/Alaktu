using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace EventDeliveryService.Sources
{
    class SourceCSV : ISource
    {
        private Int32 sourceTypeId;

        private readonly Pipline _pipline;

        private SourceCSVSettings sourceCSVSettings;

        public SourceCSV(Pipline pipline)
        {
           sourceTypeId = 1;
            _pipline = pipline;

            //sourceCSVSettings pipline.SourceTypeSettingsJson //TODO десериализавать а если пусто вставить значения по умолчанию
        }

        public Int32 GetSourceType()
        {
            return sourceTypeId;
        }
        public void Connect()
        {

        }
        public DataTable Get()
        {
            //IEnumerable<String> eBatch = File.ReadLines(_pipline.SourseConnection).Skip(_pipline.CurrentRow).Take(_pipline.CurrentRow + _pipline.BatchSize);

            DataTable eventBatch = new DataTable();

            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "eventBody";

            eventBatch.Columns.Add(column);


            string[] lines = File.ReadLines(_pipline.SourseConnection).Skip(_pipline.CurrentRow).Take(_pipline.CurrentRow + _pipline.BatchSize).ToArray();

            foreach (string line in lines)
            {
                row = eventBatch.NewRow();
                row["eventBody"] = line;
                eventBatch.Rows.Add(row);
            }

            //Int64 nextCurrentRow = _pipline.CurrentRow;

            return eventBatch;
        }

        public void Close()
        {

        }
    }
}
