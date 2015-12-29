using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace RetroRPG
{
    // Třída pro serializaci tříd do XML
    public static class Serialization<T> where T : class
    {

        public static T DeserializeFromXmlFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            DataContractSerializer deserializer = new DataContractSerializer(typeof(T));

            using (Stream stream = File.OpenRead(fileName))
            {
                return (T)deserializer.ReadObject(stream);
            }
        }

        public static T SerializeToFile(string fileName)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(MyObject));
            var subReq = new MyObject();
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, subReq);
                var xml = sww.ToString(); // Your XML
            }
        }
    }
}
