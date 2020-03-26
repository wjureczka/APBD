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

        [XmlAttribute("email")]
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
        
        [XmlAttribute("fname")]
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
        
        [XmlAttribute("lname")]
        public string Surname
        {
            get => surname;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Surname: " + value);

                surname = value;
            }
        }

        private string studies { get; set; }
        
        [XmlAttribute("studies")]
        public string Studies
        {
            get => studies;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Studies: " + value);

                studies = value;
            }
        }

        private string studiesType { get; set; }

        [XmlAttribute("studiesType")]
        public string StudiesType
        {
            get => studiesType;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("StudiesType: " + value);

                studiesType = value;
            }
        }

        private DateTime birthDate { get; set; }
        
        [XmlAttribute("birthdate")]
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
        
        [XmlAttribute("mothersName")]
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
        
        [XmlAttribute("fathersName")]
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
