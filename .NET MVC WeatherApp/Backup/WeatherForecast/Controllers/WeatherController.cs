using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.ViewModels;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "City")] LocationIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var service = new WeatherService();
                    model.Locations = service.FindLocation(model.City);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View("Error");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult FindDistinctCities(string term)
        {
            try
            {
                var service = new WeatherService();
                var cities = Json(service.FindDistinctCities(term), JsonRequestBehavior.AllowGet);
                return cities;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View("Error");
        }

        
        public ActionResult Weather(Location loc)
        {
            try
            {
                var service = new WeatherService();
                var model = new LocationIndexViewModel();
                model.Lat = loc.Lat;
                model.Lng = loc.Lng;
                model.Weathers = service.FindWeather(loc);

                return View("Weather", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View("Error");
        }
    }
}
