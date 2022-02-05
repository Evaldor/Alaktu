using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using EventDeliveryService.Sources;
using AlaktuManager.Shared;

namespace EventDeliveryService
{
    class Processor
    {
        private readonly Pipline _pipline;

        private readonly List<SourceType> _sourceTypes;

        private string _logFolder;

        private string _coreConnString;

        public Processor(Pipline pipline, string logFolder, string coreConnString)
        {
            _pipline = pipline;

            _sourceTypes = new List<SourceType>();
            SourceType _sourceType = new SourceType();
            _sourceType.Id = 1;
            _sourceType.Name = "CSV";
            _sourceTypes.Add(_sourceType);
            _sourceType.Id = 2;
            _sourceType.Name = "Excel";
            _sourceTypes.Add(_sourceType);

            _logFolder = logFolder;
            _coreConnString = coreConnString;

        }
        public void Execute()
        {

            if (_pipline.SourceTypeId == 1)
            {
                SourceCSV sourceCSV = new SourceCSV(_pipline);

                sourceCSV.Connect();

                EventBatch eventBatch = sourceCSV.GetEventBatch();

                sourceCSV.Close();

                Write(eventBatch);
            }
            if (_pipline.SourceTypeId == 2)
            {
                //todo
            }
            else
            {
                //todo
            }     
        }

        private void Write(EventBatch eventBatch)
        {
            using (SqlConnection connection = new SqlConnection(_coreConnString))
            {
                connection.Open();

                string sql_create =
                    "CREATE TABLE #EventBatch " +
                    "([eventBody] NVARCHAR (MAX) NULL" +
                    ");";

                using (SqlCommand command_create = new SqlCommand(sql_create, connection))
                {
                    command_create.ExecuteNonQuery();
                };

                using (SqlBulkCopy sbc = new SqlBulkCopy(connection))
                {
                    sbc.DestinationTableName = "#EventBatch";
                    sbc.WriteToServer(eventBatch.Batch);
                    sbc.Close();
                }

                using (var command_process = new SqlCommand("dbo.Process_EventBatch", connection))
                {
                    command_process.CommandType = CommandType.StoredProcedure;

                    command_process.Parameters.Add(new SqlParameter("@PiplineId", _pipline.Id));
                    command_process.Parameters.Add(new SqlParameter("@LastRowNumber", eventBatch.LastRowNumber));

                    command_process.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
