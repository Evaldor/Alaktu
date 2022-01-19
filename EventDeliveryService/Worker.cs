using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace EventDeliveryService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerConfig _workerConfig;
        private List<NamedTask> _namedTasks;
        private List<Pipline> _piplineRegister;  

        public Worker(ILogger<Worker> logger, WorkerConfig workerConfig)
        {
            _logger = logger;
            _workerConfig = workerConfig;
            _piplineRegister = new List<Pipline>();
            _namedTasks = new List<NamedTask>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                UpdatePiplineRegister(_workerConfig.ConnString,ref _piplineRegister);

                int limit = _piplineRegister.Count();

                foreach (Pipline pipline in _piplineRegister)
                {
                    int? namedTaskIndex = null;

                    foreach (NamedTask namedTask in _namedTasks)
                    {
                        if (pipline.Name == namedTask.taskName)
                        {
                            namedTaskIndex = _namedTasks.IndexOf(namedTask);
                        }
                    }

                    if (namedTaskIndex != null)
                    {
                        if (_namedTasks.ElementAt(namedTaskIndex ?? 0).task.Status.ToString() != "Running")
                        {
                            _namedTasks.ElementAt(namedTaskIndex ?? 0).task = Task.Factory.StartNew(() => ExecuteProcessor(pipline, _workerConfig));
                        }
                    }
                    else
                    {
                        NamedTask namedTask = new NamedTask(pipline.Name);

                        _namedTasks.Add(namedTask);

                        _namedTasks.Last<NamedTask>().task = Task.Factory.StartNew(() => ExecuteProcessor(pipline, _workerConfig));
                    }
                }

                await Task.Delay(1000, stoppingToken);

            }
        }

        private static void ExecuteProcessor(Pipline pipline,WorkerConfig wc)
        {
            
            Processor processor = new Processor(pipline, wc.WorkFolder,wc.ConnString);

            processor.Execute();

        }

        private static void UpdatePiplineRegister(String connectionString, ref List<Pipline> piplineRegister)
        {
            piplineRegister.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql =
                    "SELECT [Id] " +
                    ",[Name] " +
                    ",[SourceTypeId] " +
                    ",[SourseConnection] " +
                    ",[SourseCredentials] " +
                    ",[LastProcessedAt] " +
                    ",[ActualRowVersion] " +
                    ",[PrimaryKeyIsUsed] " +
                    ",[PrimaryKeySchemeJson] " +
                    ",[VersioningIsUsed] " +
                    ",[VersioningSchemeJson] " +
                    ",[TransferToFlatTableIsEnabled] " +
                    ",[TransferToFlatTableSchemeJson] " +
                    ",[LastProcessedStatus] " +
                    ",[BatchSize] " +
                    ",[CurrentRow] " +
                    ",[SourceTypeSettingsJson] " +
                    "FROM [dbo].[PiplineRegister] " +
                    "WHERE [IsEnabled] = 1 ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pipline pipline = new Pipline();

                            pipline.Id = reader.GetInt64(0);
                            pipline.Name = reader.GetString(1);
                            pipline.SourceTypeId = reader.GetInt32(2);
                            pipline.SourseConnection = (!reader.IsDBNull(3)) ? reader.GetString(3) : pipline.SourseConnection;
                            pipline.SourseCredentials = (!reader.IsDBNull(4)) ? reader.GetString(4) : pipline.SourseCredentials;
                            pipline.LastProcessedAt = (!reader.IsDBNull(5)) ? reader.GetDateTime(5): pipline.LastProcessedAt;
                            pipline.ActualRowVersion = (!reader.IsDBNull(6)) ? reader.GetFieldValue<byte[]>(6) : pipline.ActualRowVersion;
                            pipline.PrimaryKeyIsUsed = (!reader.IsDBNull(7)) ? reader.GetBoolean(7) : pipline.PrimaryKeyIsUsed;
                            pipline.PrimaryKeySchemeJson = (!reader.IsDBNull(8)) ? reader.GetString(8) : pipline.PrimaryKeySchemeJson;
                            pipline.VersioningIsUsed = (!reader.IsDBNull(9)) ? reader.GetBoolean(9) : pipline.VersioningIsUsed;
                            pipline.VersioningSchemeJson = (!reader.IsDBNull(10)) ? reader.GetString(10) : pipline.VersioningSchemeJson;
                            pipline.TransferToFlatTableIsEnabled = (!reader.IsDBNull(11)) ? reader.GetBoolean(11) : pipline.TransferToFlatTableIsEnabled;
                            pipline.TransferToFlatTableSchemeJson = (!reader.IsDBNull(12)) ? reader.GetString(12) : pipline.TransferToFlatTableSchemeJson;
                            pipline.LastProcessedStatus = (!reader.IsDBNull(13)) ? reader.GetString(13) : pipline.LastProcessedStatus;
                            pipline.BatchSize = (!reader.IsDBNull(14)) ? reader.GetInt32(14) : 0;
                            pipline.CurrentRow = reader.GetInt32(15);
                            pipline.SourceTypeSettingsJson = (!reader.IsDBNull(16)) ? reader.GetString(16) : pipline.SourceTypeSettingsJson;

                            piplineRegister.Add(pipline);
                        }
                    }
                }

            }
        }
    }

}
