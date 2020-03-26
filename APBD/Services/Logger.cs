using System;
using System.IO;

namespace APBD.Services
{
    public class Logger
    {
        private static Logger loggerInstance = null;
        private string errorLogFilePath = "./łog.txt";

        private Logger()
        {
        }

        public static Logger GetInstance()
        {
            if (loggerInstance == null)
            {
                loggerInstance = new Logger();
            }

            return loggerInstance;
        }

        public void Error(Exception exception)
        {
            var exceptionMessage = exception.Message + "\n";
            var errorPath = this.errorLogFilePath;
            
            File.AppendAllText(errorPath, exceptionMessage);
        }
    }
}