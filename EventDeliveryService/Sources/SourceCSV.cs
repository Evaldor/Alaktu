using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlaktuManager.Shared;

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

            sourceCSVSettings = new SourceCSVSettings();

            string xStr = JsonSerializer.Serialize(sourceCSVSettings);

            sourceCSVSettings = JsonSerializer.Deserialize<SourceCSVSettings>(_pipline.SourceTypeSettingsJson ?? xStr);

        }

        public Int64 GetSourceType()
        {
            return sourceTypeId;
        }
        public void Connect()
        {

        }
        public EventBatch GetEventBatch()
        {
            //IEnumerable<String> eBatch = File.ReadLines(_pipline.SourseConnection).Skip(_pipline.CurrentRow).Take(_pipline.CurrentRow + _pipline.BatchSize);

            EventBatch eventBatch = new EventBatch();
            eventBatch.Batch = new DataTable();

            DataColumn column;
            DataRow row;
            string header = "";
            int currentRow = _pipline.CurrentRow;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "eventBody";

            eventBatch.Batch.Columns.Add(column);

            if (sourceCSVSettings.IsWithHeader) 
            {
                header = File.ReadLines(_pipline.SourseConnection).First();
                currentRow = currentRow == 0 ? 1 : currentRow;
            }

            string[] lines = File.ReadLines(_pipline.SourseConnection).Skip(currentRow).Take(_pipline.BatchSize).ToArray();

            foreach (string line in lines)
            {
                row = eventBatch.Batch.NewRow();
                row["eventBody"] = header+line;
                eventBatch.Batch.Rows.Add(row);
            }

            if (lines.Length < _pipline.BatchSize)
            {
                eventBatch.LastRowNumber = 0;
            }
            else
            {
                eventBatch.LastRowNumber = currentRow + lines.Length;
            }
            

            return eventBatch;
        }

        public void Close()
        {

        }
    }
}
