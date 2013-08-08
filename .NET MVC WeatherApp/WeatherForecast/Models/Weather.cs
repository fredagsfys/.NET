using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace WeatherForecast.Models
{
    [MetadataType(typeof(Weather_Metadata))]
    public partial class Weather : EntityObject
    {
        public DateTime NextUpdate { get; set; }
    }
    public class Weather_Metadata
    {
        [DisplayName("Time")]
        [Required(ErrorMessage = "Time was incorrect")]
        [StringLength(50, ErrorMessage = "Time exceeded length limit, 50 characters")]
        public string Time { get; set; }

        [DisplayName("Symbol")]
        [Required(ErrorMessage = "Symbol was incorrect")]
        [StringLength(50, ErrorMessage = "Symbol exceeded length limit, 50 characters")]
        public string Symbol { get; set; }

        [DisplayName("Temp")]
        [Required(ErrorMessage = "Temp was incorrect")]
        [StringLength(50, ErrorMessage = "Temp exceeded length limit, 50 characters")]
        public string Temp { get; set; }

        public int LocationID { get; set; }
      
    }
}