using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using APBD.Models;

namespace APBD
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataPath = "data.csv";
            var resultPath = "żesult.xml";
            var extensionType = "xml";
            
            if (!String.IsNullOrEmpty(args[0]))
            {
                dataPath = args[0];
            }

            var isPathValid = dataPath.IndexOfAny(Path.GetInvalidPathChars()) == -1;

            if (!isPathValid)
            {
                throw new ArgumentException("Podana ścieżka jest niepoprawna");
            }

            if (!File.Exists(dataPath))
            {
                throw new FileNotFoundException("Plik " + dataPath + " nie istnieje");
            }

            if (!String.IsNullOrEmpty(args[1]))
            {
                resultPath = args[1];
            }
            
            if (!String.IsNullOrEmpty(args[2]))
            {
                extensionType = args[2];
            }
            
            ArrayList Students = new ArrayList();
            
            var fi = new FileInfo(dataPath);
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    
                    Student Student = new Student();

                    Student.Name = columns[0];
                    Student.Surname = columns[1];
                    Student.Studies = columns[2];
                    Student.StudiesType = columns[3];
                    Student.StudentId = columns[4];
                    Student.Birthday = DateTime.Parse(columns[5]);
                    Student.Email = columns[6];
                    Student.MothersName = columns[7];
                    Student.FathersName = columns[8];
                }
            }
            //stream.Dispose();
        }
    }
}