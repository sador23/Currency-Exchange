using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyExchange.Services
{
    public class XMLParser
    {

        public Dictionary<DateTime, Dictionary<string, double>> StreamParser(Stream stream, DateTime lastDate)
        {
            Dictionary<DateTime, Dictionary<string, double>> result = new Dictionary<DateTime, Dictionary<string, double>>();
            XmlReader xmlReader = XmlReader.Create(stream);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName.Contains("Cube") )
                {
                    if (xmlReader.AttributeCount == 1)
                    {
                        
                        Dictionary<string, double> dailyRate = new Dictionary<string, double>();
                        DateTime date = DateTime.Parse(xmlReader.GetAttribute(0));
                        if (date.Date.CompareTo(lastDate.Date) <= 0) break;
                        result.Add(date.Date, dailyRate);
                        while (xmlReader.Read() && xmlReader.AttributeCount == 2)
                        {
                            dailyRate.Add(xmlReader.GetAttribute(0), Double.Parse(xmlReader.GetAttribute(1), System.Globalization.CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
            return result;
        }
    }
}
