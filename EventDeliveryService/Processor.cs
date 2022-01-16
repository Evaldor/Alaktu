using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EventDeliveryService
{
    class Processor
    {
        private readonly Pipline _pipline;

        private Dictionary<int, string> SourceType;

        private string _logFolder; 
    public Processor(Pipline pipline, string logFolder)
        {
            _pipline = pipline;

            SourceType.Add(1, "CSV");
            SourceType.Add(2, "Excel");

            _logFolder = logFolder;

        }
        public void Execute()
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
}
