using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CurrencyExchange.Services
{
    public class XMLParser
    {

        public string StreamParser(Stream stream)
        {
            XmlReader xmlReader = XmlReader.Create(stream);
            string output = "";
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName.Contains("Cube") )
                {
                    output += "This is a node : " + xmlReader.LocalName;
                    for (int i = 0; i < xmlReader.AttributeCount; i++)
                    {
                        output += "Attribute " + i + " is : " + xmlReader.GetAttribute(i);
                    }
                }
            }
            return output;
        }
    }
}
