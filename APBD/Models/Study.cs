using System.Xml.Serialization;
using APBD.Exceptions;

namespace APBD.Models
{
    public class Study
    {

        public Study()
        {
        }
        public Study(string name, string mode)
        {
            this.Name = name;
            this.Mode = mode;
        }
        
        private string name { get; set; }

        [XmlElement("name")]
        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Study name: " + value);
                name = value;
            }
        }
        
        private string mode { get; set; }

        [XmlElement("mode")]
        public string Mode
        {
            get => mode;

            set { 
                if (string.IsNullOrEmpty(value)) throw new NotValidStudentData("Study mode: " + value);
                mode = value; 
            }
        }
    }
}