using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CVMe.Common.Helpers
{
    public interface IXmlSerializerHelper
    {
        T DeserializeXml<T>(string xml);
        string SerializeXml<T>(T objectToSerialize);
    }

    public class XmlSerializerHelper : IXmlSerializerHelper
    {
        public T DeserializeXml<T>(string xml)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xml))
            {
                return (T)mySerializer.Deserialize(stringReader);
            }
        }

        public string SerializeXml<T>(T objectToSerialize)
        {
            XmlSerializer mySerializer = new XmlSerializer(objectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                mySerializer.Serialize(textWriter, objectToSerialize);
                return textWriter.ToString();
            }
        }
    }
}
