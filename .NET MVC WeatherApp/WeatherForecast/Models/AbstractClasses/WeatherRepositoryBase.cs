using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Models.Interfaces;

namespace WeatherForecast.Models.AbstractClasses
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {
        #region Weather

        public abstract void Add(Weather weather);
        public abstract void Update(Weather weather);
        public abstract void Delete(Weather weather);
        public abstract IQueryable<Weather> QueryWeather();

        public Weather FindWeather(int weatherId)
        {
            return this.QueryWeather().SingleOrDefault(t => t.WeatherID == weatherId);
        }

        public List<Weather> FindAllWeather()
        {
            return this.QueryWeather().ToList();
        }

        #endregion

        #region Location

        public abstract void Add(Location location);
        public abstract void Delete(Location location);
        public abstract void Update(Location location);
        public abstract IQueryable<Location> QueryLocation();

        public Location FindLocation(int locationId)
        {
            return this.QueryLocation().SingleOrDefault(u => u.LocationID == locationId);
        }

        public List<Location> FindAllLocation()
        {
            return this.QueryLocation().ToList();
        }
        List<Weather> IWeatherRepository.FindAllLocation()
        {
            throw new NotImplementedException();
        }

        Weather IWeatherRepository.FindLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Common

        public abstract void Save();

        #endregion
    }
}