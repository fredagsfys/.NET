using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Globalization;

namespace WeatherForecast.Models
{
    public class LocationFactory
    {
        public static Location Create(XElement element)
        {
          var xElement = element.Element("name");
          if (xElement != null)
          {
            var xElement1 = element.Element("adminName1");
            if (xElement1 != null)
            {
              var element1 = element.Element("countryName");
              if (element1 != null)
              {
                var xElement2 = element.Element("lat");
                if (xElement2 != null)
                {
                  var element2 = element.Element("lng");
                  if (element2 != null)
                    return new Location
                      {
                        City = xElement.Value,
                        County = xElement1.Value,
                        Country = element1.Value,
                        Lat = xElement2.Value,
                        Lng = element2.Value
                      };
                }
              }
            }
          }
          return null;
        }
    }
}