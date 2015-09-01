using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Arena.Serialization
{
    public class XmlSerializer<T> where T : class
    {
        private readonly XmlSerializer _serializer;
        private readonly Encoding _encoding;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
            _encoding = new UTF8Encoding();
        }

        public XmlSerializer(Encoding encoding)
        {
            _serializer = new XmlSerializer(typeof(T));
            _encoding = encoding;
        }

        public string Serialize(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, _encoding))
                {
                    using (var writer = XmlWriter.Create(streamWriter))
                    {
                        _serializer.Serialize(writer, obj);
                        var buffer = memoryStream.ToArray();
                        return _encoding.GetString(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        public T Deserialize(string xmlString)
        {
            using (var memoryStream = new MemoryStream(_encoding.GetBytes(xmlString)))
            {
                using (var reader = new StreamReader(memoryStream, _encoding))
                {
                   return (T)_serializer.Deserialize(reader);
                }
            }
        }
    }
}
