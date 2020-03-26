using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using APBD.Exceptions;
using APBD.Models;

namespace APBD.Services
{
    public class StudentParser
    {
        public static Dictionary<string, Student> ParseStudentsFromCSV(string dataPath)
        {
            Logger logger = Logger.GetInstance();
            Dictionary<string, Student> students = new Dictionary<string, Student>();

            var fi = new FileInfo(dataPath);
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');

                    Student student = new Student();
                    
                    try
                    {
                        student.Name = columns[0];
                        student.Surname = columns[1];
                        student.Studies = columns[2];
                        student.StudiesType = columns[3];
                        student.StudentId = columns[4];
                        student.Birthdate = DateTime.Parse(columns[5]);
                        student.Email = columns[6];
                        student.MothersName = columns[7];
                        student.FathersName = columns[8];

                        if (students.ContainsKey(student.StudentId))
                        {
                            Exception exception = new DuplicatedStudentDataExceptionException(line);
                            logger.Error(exception);
                        }
                        else
                        {
                            students.TryAdd(student.StudentId, student);
                        }
                    }
                    catch (Exception exception)
                    {
                        Exception richException = new Exception(exception.Message + " -> " + line);
                        logger.Error(richException);
                    }
                }
                
                stream.Dispose();
            }
            
            return students;
        }

        public static void StudentsToXML(Dictionary<string, Student> students)
        {
            foreach (var studentsKeyValue in students)
            {
                Student student = studentsKeyValue.Value;

                XmlSerializer serializer = new XmlSerializer(student.GetType());
                
                serializer.Serialize(Console.Out, student);
                Console.WriteLine();
            }
        }
    }
}