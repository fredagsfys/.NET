using System;
using System.Collections.Generic;

namespace Easyfy.CHS.Model.Athlete
{
    public class WallPost
    {
        public WallPost()
        {
            Comments = new List<Comment>();            
        }
        public string UserReference { get; set; }
        public List<Comment> Comments { get; set; }
        //I content sparas nyhet, status eller resultat i strängform.
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
    public class Comment
    {
        public string Message { get; set; }
        public AthleteReference AthleteReference { get; set; }
        public DateTime Created { get; set; }
    }
}
