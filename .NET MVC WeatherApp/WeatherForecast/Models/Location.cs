using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace WeatherForecast.Models
{
    [MetadataType(typeof(Location_Metadata))]
    public partial class Location : EntityObject
    {

    }
    public class Location_Metadata
    {
        [DisplayName("City")]
        [Required(ErrorMessage = "City was incorrect")]
        [StringLength(50, ErrorMessage="City exceeded length limit, 50 characters")]
        public string City { get; set; }

        [DisplayName("County")]
        [Required(ErrorMessage = "County was incorrect")]
        [StringLength(50, ErrorMessage = "County exceeded length limit, 50 characters")]
        public string County { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country was incorrect")]
        [StringLength(50, ErrorMessage = "Country exceeded length limit, 50 characters")]
        public string Country { get; set; }

        [DisplayName("Latitude")]
        [Required(ErrorMessage = "Latitude is incorrect")]
        [StringLength(10, ErrorMessage = "Latitude exceeded length limit, 10 characters")]
        public string Lat { get; set; }

        [DisplayName("Longitude")]
        [Required(ErrorMessage = "Longitude is incorrect")]
        [StringLength(10, ErrorMessage = "Longitude exceeded length limit, 10 characters")]
        public string Lng { get; set; }

        [DisplayName("Next update")]
        [Required(ErrorMessage = "Next update is incorrect")]
        public DateTime NextUpdate { get; set; }

        public int LocationID { get; set; }
    }
}
