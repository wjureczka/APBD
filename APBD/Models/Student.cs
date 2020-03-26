﻿using System;
 using System.Xml.Serialization;
 using APBD.Exceptions;


 namespace APBD.Models
{
    public class Student
    {

        public Student()
        {
        }
        private string email { get; set; }

        [XmlElement("email")]
        public string Email
        {
            get => email;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Email: " + value);

                email = value;
            }
        }
        
        private string name { get; set; }
        
        [XmlElement("fname")]
        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Name: " + value);

                name = value;
            }
        }
        
        private string surname { get; set; }
        
        [XmlElement("lname")]
        public string Surname
        {
            get => surname;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Surname: " + value);

                surname = value;
            }
        }

        private Study study { get; set; }
        
        [XmlElement("studies")]
        public Study Study
        {
            get => study;

            set => study = value;
        }

        private DateTime birthDate { get; set; }
        
        [XmlElement("birthdate")]
        public DateTime Birthdate
        {
            get => birthDate;

            set
            {
                if (!DateTime.TryParse(value.ToString(), out value)) throw new NotValidStudentData("Birthday: " + value);

                birthDate = value;
            }
        }

        private string mothersName { get; set; }
        
        [XmlElement("mothersName")]
        public string MothersName
        {
            get => mothersName;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("MothersName: " + value);

                mothersName = value;
            }
        }
        
        private string fathersName { get; set; }
        
        [XmlElement("fathersName")]
        public string FathersName
        {
            get => fathersName;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("FathersName: " + value);

                mothersName = value;
            }
        }
        
        private string studentId { get; set; }
        
        [XmlAttribute("indexNumber")]
        public string StudentId
        {
            get => studentId;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("StudentId: " + value);

                studentId = value;
            }
        }
    }
}
