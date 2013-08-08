using System;
using Easyfy.CHS.Model.Wod;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easyfy.CHS.Model.Exercise
{
  public class ExerciseBase {
    //public ExerciseBase()
    //{
    //    LengthType = LengthType.Meter;
    //    AuditInfo = new AuditInformation();
    //}
    public string GetIntId() {
      if (!String.IsNullOrWhiteSpace(Id))
        return Id.Split('/')[1];
      return "0";
    }

    public string Id { get; set; }

    [DisplayName("Name:")]
    [Required(ErrorMessage = "The exercise must have a name.")]
    public string Name { get; set; }

    [DisplayName("Exercise type:")]
    [Required(ErrorMessage = "Please choose a exercise type.")]
    public ExerciseType ExerciseType { get; set; }

    [DisplayName("Length:")]
    [Required(ErrorMessage = "Please enter a length.")]
    public double Length { get; set; }

    [DisplayName("Length:")]
    [Required(ErrorMessage = "Please enter a lengthunit.")]
    public string LengthUnit { get; set; }
    [DisplayName("Length type")]
    [Required(ErrorMessage = "Please choose a length type.")]
    public LengthType LengthType { get; set; }

    public AuditInformation AuditInfo { get; set; }

    [JsonIgnore]
    public string LengthTypeToString {
      get {
        if (this.LengthType == LengthType.Meter)
          return "m";
        else if (this.LengthType == LengthType.KiloMeter)
          return "km";
        else if (this.LengthType == LengthType.Mile)
          return (this.Length > 1 ? "miles" : "mile");
        else if (this.LengthType == LengthType.Foot)
          return (this.Length > 1 ? "feet" : "foot");
        else
          return "";
      }
    }
  }
}
