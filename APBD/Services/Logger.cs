using System;

namespace APBD.Services
{
    public class Logger
    {
        private Logger _logger;
        private string errorLogFile = "łog.txt";

        private Logger()
        {
            
        }
        
        private void WriteToFile(string fileName, string message)
        {
            
        }

        public Logger GetLogger()
        {
            if (_logger == null)
            {
                this._logger = new Logger();
            }

            return this._logger;
        }

        public void Error(Exception exception) 
        {
            this.WriteToFile(this.errorLogFile, exception.Message);
        }
    }
}