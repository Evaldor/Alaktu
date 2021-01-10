using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EventDeliveryService
{
    class Processor
    {
        private readonly string _config;

        public Processor(string config)
        {
            _config = config;

        }
        public void WriteToFile()
        {

            int j = 0;
            while (j < 10)
            {
                using (StreamWriter writer = File.AppendText(_config))
                {
                    
                    
                    writer.WriteLine(DateTimeOffset.Now.ToString());
                }

                Thread.Sleep(1000);
                j++;
            }
        }
    }
}
