using System.IO;
using System.Text;
using System.Xml;

namespace Arena.Serialization
{
    public class XmlSerializer<T> where T : class 
    {
        private readonly System.Xml.Serialization.XmlSerializer _serializer;
        private readonly Encoding _encoding;

        public XmlSerializer()
        {
            _serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            _encoding = new UTF8Encoding();
        }

        public XmlSerializer(Encoding encoding)
        {
            _serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            _encoding = encoding;
        }

        public string Serialize(T obj)
        {
            var result = string.Empty;
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, _encoding))
            using (XmlWriter writer = XmlWriter.Create(streamWriter))
            {
                _serializer.Serialize(writer, obj);
                byte[] buffer = memoryStream.ToArray();
                result = _encoding.GetString(buffer, 0, buffer.Length);
            }

            return result;
        }

        public T Deserialize(string xmlString)
        {
            T result;
            using (var memoryStream = new MemoryStream(_encoding.GetBytes(xmlString)))
            using (var reader = new StreamReader(memoryStream, _encoding))
            {
                result = (T) _serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
