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
          var xElement = status.Element("symbol");
          if (xElement != null)
          {
            var element = status.Element("temperature");
            if (element != null)
              return new Weather
                {
                  Symbol = xElement.Attribute("name").Value.Replace(" ", "_"),
                  Time = status.Attribute("from").Value,
                  Period = status.Attribute("period").Value,
                  Temp = element.Attribute("value").Value,
                  LocationID = locationId
                
                };
          }
          return null;
        }
    }
}