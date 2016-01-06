using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RetroRpgPlayground
{
    [Serializable]
    class GameObject
    {
       
        public int value;


        public List<GameObject> Load()
        {
            string file = "filepath";
            List<GameObject> listofa = new List<GameObject>();
            XmlSerializer formatter = new XmlSerializer(GetType());
            FileStream aFile = new FileStream(file, FileMode.Open);
            byte[] buffer = new byte[aFile.Length];
            aFile.Read(buffer, 0, (int)aFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            return (List<GameObject>)formatter.Deserialize(stream);
        }

        public void Save(List<GameObject> listofa)
        {
            string path = "filepath";
            FileStream outFile = File.Create(path);
            XmlSerializer formatter = new XmlSerializer(GetType());
            formatter.Serialize(outFile, listofa);
        }
    }
}
