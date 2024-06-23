using System.CodeDom;
using System.Collections;
using System.IO;
using System.Xml;

namespace Sandboxer
{

    public class PSParser
    {

        public static XmlDocument ParseXML(string filePath)
        {
            if (!filePath.EndsWith(".ps1")) return new XmlDocument();

            string xmlData = "";
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!line.StartsWith("#! SANDBOXER:")) continue;
                string temp = line.Replace("#! SANDBOXER:", "");
                
                xmlData += temp;
            }

            if (xmlData == "") return new XmlDocument();
            

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);
                return xmlDoc; // Return the loaded XmlDocument on successful load and node check
            }
            catch (System.Exception)
            {
                // Return an empty XmlDocument if any exception occurs during XML load or processing
                return new XmlDocument();
            }
        }

    }
}