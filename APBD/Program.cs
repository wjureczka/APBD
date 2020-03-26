using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using APBD.Exceptions;
using APBD.Models;
using APBD.Services;

namespace APBD
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataPath = "data.csv";
            var resultPath = "żesult.json";
            var extensionType = "json";

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

            try
            {
                if (!String.IsNullOrEmpty(args[1]))
                {
                    resultPath = args[1];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Output path not found, using: " + resultPath);
            }


            try
            {
                if (!String.IsNullOrEmpty(args[2]))
                {
                    extensionType = args[2];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Extension type not found, using: " + extensionType);
            }
            
            Dictionary<string, Student> students = StudentParser.ParseStudentsFromCSV(dataPath);
            
            University university = new University();
            
            foreach (var studentKeyValue in students)
            {
                university.Students.Add(studentKeyValue.Value);
            }

            if (extensionType.Equals("xml"))
            {
                StudentParser.UniversityToXml(university, resultPath);
            }

            if (extensionType.Equals("json"))
            {
                StudentParser.UniversityToJSON(university, resultPath);
            }
        }
    }
}