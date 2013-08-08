using System.Collections.Generic;
using Easyfy.CHS.Model.Exercise;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easyfy.CHS.Model.Wod
{
    public class Round
    {
        public Round()
        {
            ExerciseRounds = new List<ExerciseRound>();
        }

        [DisplayName("Laps: ")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Must be atleast 1.")]
        public int Laps { get; set; }
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "Please choose atleast one exercise.")]
        public List<ExerciseRound> ExerciseRounds { get; set; }
    }
}
