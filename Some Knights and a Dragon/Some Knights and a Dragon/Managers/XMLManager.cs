using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class XMLManager <T> // Manages loading XML for a specific type
    {
        XmlSerializer xmlSerializer;
        public XMLManager()
        {
            xmlSerializer = new XmlSerializer(typeof(T));
        }

        public T Get(string filepath) // Loads an XML file and returns it as an object of the generic type
        {
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/../../../" + filepath))
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }

        public void Serialize(T obj, string filepath) // Serialize an object to an XML file (used for saving)
        {
            using (StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "/../../../" + filepath))
            {
                xmlSerializer.Serialize(writer, obj);
            }
        }
    }
}
