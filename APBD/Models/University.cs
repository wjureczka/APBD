using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace APBD.Models
{
    [XmlRoot("uczelnia")]
    public class University
    {

        public University()
        {
        }

        public University(Dictionary<string, Student> students)
        {
            foreach (var studentKeyValue in students)
            {
                Students.Add(studentKeyValue.Value);
            }

            var activeStudies = new Dictionary<string, ActiveStudy>();
            
            foreach (Student student in Students)
            {
                if (activeStudies.ContainsKey(student.Study.Name))
                {
                    var activeStudy = activeStudies[student.Study.Name];

                    activeStudy.NumberOfStudents += 1;
                }
                else
                {
                    var activeStudy = new ActiveStudy(student.Study.Name);
                    activeStudy.NumberOfStudents += 1;
                    activeStudies.Add(student.Study.Name, activeStudy);
                }
            }
            
            foreach (var keyValueActiveStudy in activeStudies)
            {
                this.ActiveStudies.Add(keyValueActiveStudy.Value);
            }
        }
        
        [XmlAttribute("createdAt")]
        public DateTime classCreatedAt = DateTime.Now;
        
        [XmlAttribute("author")]
        public string authorOfClass = "Wojciech Jureczka";
        
        [XmlArrayItem(ElementName = "student", Type = typeof(Student))]
        [XmlArray("studenci")]
        public ArrayList Students = new ArrayList();
        
        [XmlArrayItem(ElementName = "studies", Type = typeof(ActiveStudy))]
        [XmlArray("activeStudies")]
        public ArrayList ActiveStudies = new ArrayList();
    }
}