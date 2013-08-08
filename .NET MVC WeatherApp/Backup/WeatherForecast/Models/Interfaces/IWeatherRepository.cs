using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecast.Models.AbstractClasses
{
    public interface IWeatherRepository
    {
        void Add(Weather weather);
        void Add(Location location);
        void Delete(Weather weather);
        void Delete(Location location);
        List<Weather> FindAllWeather();
        List<Weather> FindAllLocation();
        Weather FindWeather(int weatherId);
        Weather FindLocation(int locationId);
        IQueryable<Weather> QueryWeather();
        IQueryable<Location> QueryLocation();
        void Save();
        void Update(Weather weather);
        void Update(Location location);
    }
}