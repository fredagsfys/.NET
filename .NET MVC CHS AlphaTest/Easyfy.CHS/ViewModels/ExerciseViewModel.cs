using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easyfy.CHS.Model.Exercise;

namespace Easyfy.CHS.ViewModels
{
    public class ExerciseViewModel
    {
        public ExerciseViewModel()
        {
            Exercises = new List<ExerciseBase>();
        }
        public ExerciseBase ExerciseBase { get; set; }
        public IList<ExerciseBase> Exercises { get; set; }
    }
}