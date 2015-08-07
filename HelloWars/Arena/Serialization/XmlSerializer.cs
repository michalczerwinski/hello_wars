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
            string result = string.Empty;
            using (var memStm = new MemoryStream())
            using (var sww = new StreamWriter(memStm, _encoding))
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                _serializer.Serialize(writer, obj);
                byte[] buffer = memStm.ToArray();
                result = _encoding.GetString(buffer, 0, buffer.Length);
            }
            return result;
        }

        public T Deserialize(string xmlString)
        {
            T result = null;
            using (var memStm = new MemoryStream(_encoding.GetBytes(xmlString)))
            using (var reader = new StreamReader(memStm, _encoding))
            {
                result = (T) _serializer.Deserialize(reader);
            }
            return result;
        }
    }
}
