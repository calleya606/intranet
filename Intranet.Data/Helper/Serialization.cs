using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Xml.Serialization;

    public class SerializationHelper
    {
        public byte[] Serialize2Bytes(object data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                MemoryStream streamMemory = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(streamMemory, data);
                return streamMemory.GetBuffer();
            }
        }

        public T DeserializeFromBytes<T>(byte[] binData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(binData);
            return (T)formatter.Deserialize(ms);
        }

        public string Serialize2String(object data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            else
            {
                MemoryStream streamMemory = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(streamMemory, data);
                return Convert.ToBase64String(streamMemory.GetBuffer());
            }
        }

        public object DeserializeFromString(string binString)
        {
            if (binString == null)
            {
                return null;
            }
            else
            {
                if (binString.Length == 0)
                {
                    return null;
                }
                else
                {
                    byte[] binData = Convert.FromBase64String(binString);
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream(binData);
                    return formatter.Deserialize(ms);
                }
            }
        }

        public string SerializeToXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}