using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easyfy.CHS.ViewModels
{
    public class EditAthleteViewModel
    {
        public enum GenderOptions { Male, Female }
        public string Username { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "The provided email seems to be invalid, please correct and try again.")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public bool ShowPicture { get; set; }

        [DisplayName("City")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string City { get; set; }

        [DisplayName("Country")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Country { get; set; }

        [DisplayName("Weight")]
        [RegularExpression(@"^([0-9]{1,3}([,\.]{1}[0-9]{1,2})?)$", ErrorMessage = "Weight has to be provided in the following format: 75,55.")]
        public string Weight { get; set; }

        [DisplayName("Height")]
        [RegularExpression(@"^([0-9]{1}[,\.]{0,1}[0-9]{2})$", ErrorMessage = "Height has to be formated as such: 1,93, 193, 1.93")]
        public string Height { get; set; }

        [DisplayName("Gender")]
        [DataType(DataType.Text)]
        [Required]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }

        public SelectList GenderOptionValues
        {
            get
            {
                var options = new Dictionary<GenderOptions, string>
                {
                    { GenderOptions.Male, "Male" },
                    { GenderOptions.Female, "Female" }
                };
                return new SelectList(options, "Key", "Value");
            }
        }
    }
}