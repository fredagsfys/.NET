using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using FlexProviders.Membership;

namespace Easyfy.CHS.Model.Athlete
{
  public sealed class Athlete : IFlexMembershipUser
  {
    public Athlete()
    {
      //Achivements = new List<Achievement>();
      Goals = new List<Goal>();
      AthleteStatistics = new AthleteStatistics();
      Follows = new List<FollowReference>();
      Affiliates = new List<AffiliateRoleReference>();
      OAuthAccounts = new Collection<FlexOAuthAccount>();
    }

    public AthleteReference ToAthleteReference() {
      return new AthleteReference {Id = Id, Name = FirstName + " " + LastName, ProfileImage = ProfilePicture};
    }

    public AthleteStatistics AthleteStatistics { get; set; }
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string PasswordResetToken { get; set; }
    public DateTime PasswordResetTokenExpiration { get; set; }

    [DisplayName("Email")]
    [DataType(DataType.EmailAddress)]
    [Required]
    [RegularExpression(
      @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
      ,
      ErrorMessage = "The provided email seems to be invalid, please correct and try again.")]
    [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }

    [DisplayName("City")]
    [DataType(DataType.Text)]
    [StringLength(50)]
    public string City { get; set; }

    [DisplayName("Country")]
    [DataType(DataType.Text)]
    [StringLength(50)]
    public string Country { get; set; }

    [DisplayName("Weight")]
    [RegularExpression(@"^([0-9]{1,3}([,\.]{1}[0-9]{1,2})?)$",
      ErrorMessage = "Weight has to be provided in the following format: 75,55.")]
    public string Weight { get; set; }

    [DisplayName("Height")]
    [RegularExpression(@"^([0-9]{1}[,\.]{0,1}[0-9]{2})$",
      ErrorMessage = "Height has to be formated as such: 1,93, 193, 1.93")]
    public string Height { get; set; }

    [DisplayName("Gender")]
    [DataType(DataType.Text)]
    [Required]
    public string Gender { get; set; }

    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime Birth { get; set; }

    // True if user is siteadministrator 
    public bool Admin { get; set; }

    // Gets the image url used when rendering wall posts
    public string WallImageUrl { get { return "https://graph.facebook.com/johan.uddh/picture?type=square"; } }

    //public List<Achievement> Achivements { get; set; }
    public List<Goal> Goals { get; set; }
    public List<FollowReference> Follows { get; set; }
    public List<AffiliateRoleReference> Affiliates { get; set; }
    public ICollection<FlexOAuthAccount> OAuthAccounts { get; set; }

    public string CalcAge()
    {
      var today = DateTime.Today;
      var a = (today.Year * 100 + today.Month) * 100 + today.Day;
      var b = (Birth.Year * 100 + Birth.Month) * 100 + Birth.Day;

      return ((a - b) / 10000).ToString(CultureInfo.InvariantCulture);
    }

    public string AffiliateSearchField { get { return AffiliateToString(); } }

    public string AffiliateToString()
    {
      var result = new StringBuilder();

      foreach (var item in Affiliates)
      {
        result.Append(item.Name + " ");
      }

      return result.ToString();
    }
  }

  public class AthleteReference
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string ProfileImage { get; set; }
  }

}