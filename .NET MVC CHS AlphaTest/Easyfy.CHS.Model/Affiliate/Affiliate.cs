using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Easyfy.CHS.Model.Wod;

namespace Easyfy.CHS.Model.Affiliate
{
    public class Affiliate
    {
        public Affiliate()
        {
            Wods = new List<WodBase>();
        }

        public string Id { get; set; }

        //[RegularExpression(@"^(([a-zA-ZéÉåäöÅÄ_\-\.]{1,30}).)*$",
        //    ErrorMessage = "The provided name seems to be invalid, please correct and try again.")]
        [Required(ErrorMessage = "Please enter a name for your affiliate.")]
        public string Name { get; set; }

        //[RegularExpression(@"^(([a-zA-Z0-9-.,!?éÉåäöÅÄÖ]{1,250}).)*$",
        //    ErrorMessage = "The provided description seems to be invalid, please correct and try again.")]
        public string Description { get; set; }

        //[RegularExpression(@"^(.*)([a-zA-Z0-9éÉåäöÅÄ_\-\.]{1,40}[0-9]{1,5})$",
        //    ErrorMessage = "The provided Address seems to be invalid, please correct and try again.")]
        [Required(ErrorMessage = "For your visitors sake, please provide an address for your affiliate.")]
        public string Address { get; set; }

        //[RegularExpression(@"^(([a-zA-ZéÉåäöÅÄ]{1,40}).)*$",
        //    ErrorMessage = "The provided Address seems to be invalid, please correct and try again.")]
        [Required(ErrorMessage = "In which city is your affiliate located?")]
        public string City { get; set; }

        //[RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$)",
        //    ErrorMessage = "The provided postal code seems to be invalid, please correct and try again.")]
        [Required(ErrorMessage = "Whats the locations postal code for your affiliate?")]
        public string Postal { get; set; }

        //[RegularExpression(@"^(([a-zA-ZéÉåäöÅÄ]{1,40}).)*$",
        //    ErrorMessage = "The provided Country seems to be invalid, please correct and try again.")]
        [Required(ErrorMessage = "In which country is your affiliate located?")]
        public string Country { get; set; }

        public string FriendlyUrl { get; set; }

        public List<WodBase> Wods { get; set; }
    }
}