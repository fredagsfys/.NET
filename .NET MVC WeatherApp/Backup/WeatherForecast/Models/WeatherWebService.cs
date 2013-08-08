using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace WeatherForecast.Models
{
    public class WeatherWebService
    {
        private DateTime _nextUpdate;
        public DateTime NextUpdate
        {
            get
            {
                return _nextUpdate;
            }
            set
            {
                _nextUpdate = value;
            }
        }

        public List<Location> FindLocation(string city)
        {
            // Load the response from the webservice into an XML document.
            //document = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/scottgu_user.xml"));
            string requestUriString = String.Format(@"http://api.geonames.org/search?name_equals={0}&username=devHaris&featureClass=P&style=full", city);
            var document = LoadDocument(requestUriString);

            // Return the one and only found user.
            return document.Descendants("geoname")
                .Select(u => LocationFactory.Create(u))
                .ToList();
        }

        public List<Weather> FindWeather(Location location)
        {
            // Load the response from the webservice into an XML document.
            //document = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/scottgu_timeline.xml"));
            string requestUriString = String.Format(@"http://www.yr.no/place/{0}/{1}/{2}/forecast.xml", location.Country, location.County, location.City);
            var document = LoadDocument(requestUriString);

            NextUpdate = DateTime.Parse((from nu in document.Descendants("nextupdate") select nu).SingleOrDefault().Value); ;

            return (from time in document.Descendants("time") // Alla time noder.
                    where Int32.Parse(time.Attribute("period").Value) >= 2 // Där period är större än 2.
                    group time by DateTime.Parse(time.Attribute("from").Value).Date into g // Gruppera varje nodes perioders värde. Sätter in dem i gruppen g.
                    select (from t in g select t).First()) // Så väljs den första i varje par.
                    .Take(5).Select(forecast => WeatherFactory.Create(forecast, location.LocationID)).ToList();
        }

        private XDocument LoadDocument(string requestUriString)
        {
            // Initialize a new WebRequest instance for the GeoNames Search Webservice.
            var request = WebRequest.Create(requestUriString);

            // Get the response from the web service.
            using (var response = request.GetResponse())
            {
                // Parse the response.
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Load the response into an XML document.
                    return XDocument.Load(stream);
                }
            }
        }
    }
}