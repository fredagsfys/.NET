using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using WeatherForecast.Models.AbstractClasses;

namespace WeatherForecast.Models
{
    public class WeatherRepository : WeatherRepositoryBase
    {
        private WeatherEntities _entities = new WeatherEntities();

    #region Weather
        public override void Add(Weather weather)
        {
            this._entities.Weathers.AddObject(weather);
        }

        public override void Update(Weather weather)
        {
            ObjectStateEntry entry;
            if (!this._entities.ObjectStateManager.TryGetObjectStateEntry(weather, out entry) ||
                entry.State == System.Data.EntityState.Detached)
            {
                this._entities.Weathers.Attach(weather);
            }

            if (weather.EntityState != System.Data.EntityState.Modified)
            {
                this._entities.ObjectStateManager.ChangeObjectState(weather, System.Data.EntityState.Modified);
            }
        }

        public override void Delete(Weather weather)
        {
            if (weather.EntityState == System.Data.EntityState.Detached)
            {
                this._entities.Weathers.Attach(weather);
            }
            this._entities.Weathers.DeleteObject(weather);
        }

        public override IQueryable<Weather> QueryWeather()
        {
            return this._entities.Weathers;
        }
    #endregion

    #region Location
        public override void Add(Location location)
        {
            this._entities.Locations.AddObject(location);
        }

        public override void Delete(Location location)
        {
            if (location.EntityState == System.Data.EntityState.Detached)
            {
                this._entities.Locations.Attach(location);
            }
            this._entities.Locations.DeleteObject(location);
        }

        public override void Update(Location location)
        {
            ObjectStateEntry entry;
            if(!this._entities.ObjectStateManager.TryGetObjectStateEntry(location, out entry) ||
                entry.State == System.Data.EntityState.Detached)
            {
                this._entities.Locations.Attach(location);
            }
            
            if(location.EntityState != System.Data.EntityState.Modified)
            {
                this._entities.ObjectStateManager.ChangeObjectState(location, System.Data.EntityState.Modified);
            }
        }

        public override IQueryable<Location> QueryLocation()
        {
            return this._entities.Locations.ToList().AsQueryable();
        }
    #endregion

    #region Common
        public override void Save()
        {
            this._entities.SaveChanges();
        }
    #endregion
    }
}