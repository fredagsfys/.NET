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
            return new Location
            {
                City = element.Element("name").Value,
                County = element.Element("adminName1").Value,
                Country = element.Element("countryName").Value,
                Lat = element.Element("lat").Value,
                Lng = element.Element("lng").Value
            };
        }
    }
}