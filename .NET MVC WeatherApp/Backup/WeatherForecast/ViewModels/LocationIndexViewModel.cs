using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.ViewModels
{
    public class LocationIndexViewModel
    {
        public List<Location> Locations { get; set; }
        public List<Weather> Weathers { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "A city is required")]
        [StringLength(50, ErrorMessage="City exceeded length limit, 50 characters")]
        public string City { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}