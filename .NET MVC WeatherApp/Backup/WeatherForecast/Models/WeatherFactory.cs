using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Globalization;

namespace WeatherForecast.Models
{
    public class WeatherFactory
    {
        public static Weather Create(XElement status, int locationId)
        {
            return new Weather
            {
                Symbol = status.Element("symbol").Attribute("name").Value.Replace(" ", "_"),
                Time = status.Attribute("from").Value,
                Period = status.Attribute("period").Value,
                Temp = status.Element("temperature").Attribute("value").Value,
                LocationID = locationId
                
            };
        }
    }
}