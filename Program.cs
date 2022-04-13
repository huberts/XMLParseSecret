using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{

    [XmlRoot(ElementName = "Note")]
    public class Note
    {
        [XmlElement(ElementName = "From")]
        public string From { get; set; }
        [XmlElement(ElementName = "To")]
        public string To { get; set; }
        [XmlElement(ElementName = "Encoded")]
        public string Encoded { get; set; }
    }

    [XmlRoot(ElementName = "Secret")]
    public class Secret
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XMLParserSecret");
            var noteSerializer = new XmlSerializer(typeof(Note));
            Note n;
            using (Stream reader = new FileStream(Path.Combine(Environment.CurrentDirectory, "../../..", "input.xml"), FileMode.Open))
            {
                n = (Note)noteSerializer.Deserialize(reader);
            }

            var secretSerializer = new XmlSerializer(typeof(Secret));
            Secret s = (Secret)secretSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(n.Encoded.Trim())));

            Console.Write(
                "From:" + n.From + "\n" +
                "To:" + n.To + "\n" +
                "SecretKey:" + s.Key + "\n" +
                "SecretValue:" + s.Value + "\n"
            );
        }
    }
}
