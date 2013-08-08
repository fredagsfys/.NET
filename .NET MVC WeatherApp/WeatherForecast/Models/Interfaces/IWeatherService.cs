using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast.Models.Interfaces
{
    public interface IWeatherService
    {
        System.Collections.Generic.List<Weather> FindWeather(Location location);
    }
}
