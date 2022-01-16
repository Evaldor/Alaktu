using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using EventDeliveryService.Sources;

namespace EventDeliveryService
{
    class Processor
    {
        private readonly Pipline _pipline;

        private Dictionary<int, string> _sourceType;

        private string _logFolder;

        private string _coreConnString;

        public Processor(Pipline pipline, string logFolder, string coreConnString)
        {
            _pipline = pipline;

            _sourceType = new Dictionary<int, string>();
            _sourceType.Add(1, "CSV");
            _sourceType.Add(2, "Excel");

            _logFolder = logFolder;
            _coreConnString = coreConnString;

        }
        public void Execute()
        {

            if (_pipline.SourceTypeId == 1)
            {
                SourceCSV sourceCSV = new SourceCSV(_pipline);

                sourceCSV.Connect();

                string[] dataSet = sourceCSV.Get();

                sourceCSV.Close();

                Write(dataSet);
            }
            if (_pipline.SourceTypeId == 2)
            {

            }
            else
            {
                int j = 0;
                while (j < 10)
                {
                    using (StreamWriter writer = File.AppendText(_logFolder + _pipline.Name + ".txt"))
                    {


                        writer.WriteLine(DateTimeOffset.Now.ToString());
                    }

                    Thread.Sleep(1000);
                    j++;
                }
            }     
        }

        private void Write(string[] dataSet)
        {
            using (SqlConnection connection = new SqlConnection(_coreConnString))
            {
                connection.Open();

                string sql_create =
                    "CREATE TABLE #EventBuffer " +
                    "([EventBody] NVARCHAR (MAX) NULL" +
                    ")";

                using (SqlCommand command_create = new SqlCommand(sql_create, connection))
                {
                    command_create.ExecuteNonQuery();
                };

                using (SqlBulkCopy sbc = new SqlBulkCopy(connection))
                {
                    sbc.DestinationTableName = "#EventBuffer";
                    //sbc.NotifyAfter = 10000;
                    //sbc.SqlRowsCopied += new SqlRowsCopiedEventHandler(s_SqlRowsCopied);
                    sbc.WriteToServer(dataSet); // TODO здесь конвертнуть в дататэйбл и потом дописать хранимку.
                    sbc.Close();
                }

                using (var command_process = new SqlCommand("dbo.Process_EventBatch", connection))
                {
                    command_process.CommandType = CommandType.StoredProcedure;

                    command_process.Parameters.Add(new SqlParameter("@PiplineRegisterId", _pipline.Id));

                    command_process.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
