using System;
using System.Collections;
using System.Xml.Serialization;


namespace APBD.Models
{
    [XmlRoot("uczelnia")]
    public class University
    {
        public University()
        {
        }
        
        [XmlAttribute("createdAt")]
        public DateTime classCreatedAt = DateTime.Now;
        
        [XmlAttribute("author")]
        public string authorOfClass = "Wojciech Jureczka";
        
        [XmlArrayItem(ElementName = "student", Type = typeof(Student))]
        [XmlArray("studenci")]
        public ArrayList Students = new ArrayList();
    }
}