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
                            _namedTasks.ElementAt(namedTaskIndex ?? 0).task = Task.Factory.StartNew(() => ExecuteProcessor(pipline));
                        }
                    }
                    else
                    {
                        NamedTask namedTask = new NamedTask(pipline.Name);

                        _namedTasks.Add(namedTask);

                        _namedTasks.Last<NamedTask>().task = Task.Factory.StartNew(() => ExecuteProcessor(pipline));
                    }
                }

                await Task.Delay(1000, stoppingToken);

            }
        }

        private static void ExecuteProcessor(Pipline pipline)
        {
            string config = pipline.Name;

            Processor processor = new Processor(String.Concat("D:\\tmp\\",config,".txt"));

            processor.WriteToFile();

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
                    ",[ProcessedAt] " +
                    ",[ActualRowVersion] " +
                    ",[PrimaryKeyIsUsed] " +
                    ",[PrimaryKeySchemeJson] " +
                    ",[TransferToFlatTableIsEnabled] " +
                    ",[TransferToFlatTableSchemeJson] " +
                    "FROM [dbo].[PiplineRegister]" +
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
                            pipline.ProcessedAt = (!reader.IsDBNull(2)) ? reader.GetDateTime(2): pipline.ProcessedAt;
                            pipline.ActualRowVersion = (!reader.IsDBNull(3)) ? reader.GetFieldValue<byte[]>(3) : pipline.ActualRowVersion;
                            pipline.PrimaryKeyIsUsed = (!reader.IsDBNull(4)) ? reader.GetBoolean(4) : pipline.PrimaryKeyIsUsed;
                            pipline.PrimaryKeySchemeJson = (!reader.IsDBNull(5)) ? reader.GetString(5) : pipline.PrimaryKeySchemeJson;
                            pipline.TransferToFlatTableIsEnabled = (!reader.IsDBNull(6)) ? reader.GetBoolean(6) : pipline.TransferToFlatTableIsEnabled;
                            pipline.TransferToFlatTableSchemeJson = (!reader.IsDBNull(7)) ? reader.GetString(7) : pipline.TransferToFlatTableSchemeJson; 

                            piplineRegister.Add(pipline);
                        }
                    }
                }

            }
        }
    }

}
