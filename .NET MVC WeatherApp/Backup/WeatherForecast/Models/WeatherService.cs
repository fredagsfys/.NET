using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Models.Interfaces;
using WeatherForecast.Models.AbstractClasses;

namespace WeatherForecast.Models
{
    public class WeatherService : IWeatherService
    {
        private IWeatherRepository _repository;

        public WeatherService()
            : this(new WeatherRepository())
        {
            // Blank!
        }

        public WeatherService(IWeatherRepository repository)
        {
            this._repository = repository;
        }

        public string[] FindDistinctCities(string term)
        {
            var currentLocation = this._repository
                .QueryLocation()
                .Where(u => u.City.Contains(term))
                .Select(u => u.City)
                .Distinct()
                .OrderBy(s => s)
                .ToArray();

            return currentLocation;
        }

        public List<Location> FindLocation(string city)
        {
            // Try to get the user from the database.
                var currentLocation = this._repository
                .QueryLocation()
                .Where(u => u.City.Contains(city))
                .ToList();

            // If there is no user...
            if (currentLocation == null || currentLocation.Count == 0)
            {
                //// ...get the user from the web service, and...
                var webService = new WeatherWebService();
                currentLocation = webService.FindLocation(city);

                // ...save the user in the database.
                foreach (var item in currentLocation)
                {
                    this._repository.Add(item);
                }
                
                this._repository.Save();
            }

            return currentLocation;
        }
        public List<Weather> FindWeather(Location location)
        {
            var weather = this._repository
            .QueryWeather()
            .Where(u => u.LocationID == location.LocationID)
            .ToList();

    
            if (weather == null || weather.Count == 0)
            {
                var webService = new WeatherWebService();
                weather = webService.FindWeather(location);

                location.NextUpdate = webService.NextUpdate;
                this._repository.Update(location);

                foreach (var item in weather)
                {
                    this._repository.Add(item);
                }

                this._repository.Save();
            }

           
            if (!weather.Any() || location.NextUpdate < DateTime.Now)
            {
                
                weather.ToList()
                .ForEach(t => this._repository.Delete(t));

                weather.Clear();
               
                var webService = new WeatherWebService();
                webService.FindWeather(location)
                    .ForEach(t => weather.Add(t));
               
                location.NextUpdate = webService.NextUpdate;

                foreach (var item in weather)
                {
                    this._repository.Add(item);
                }
                //Krashar här...
                this._repository.Update(location);

                // ...save the changes in the database.
                this._repository.Save();
            }

            return weather.ToList();
        }
    }
}