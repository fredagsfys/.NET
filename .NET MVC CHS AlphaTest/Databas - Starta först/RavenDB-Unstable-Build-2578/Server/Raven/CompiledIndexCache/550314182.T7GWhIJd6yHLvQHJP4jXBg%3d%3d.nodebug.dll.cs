using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Workouts_2fForUser : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Workouts_2fForUser()
	{
		this.ViewText = @"docs.WorkoutResults.Select(workout => new {
    AthleteName = String.Format(""{0} {1}"", this.LoadDocument(workout.AthleteId).FirstName, this.LoadDocument(workout.AthleteId).LastName),
    AthleteId = workout.AthleteId,
    Date = workout.Date,
    Results = workout.Results,
    WorkoutResultId = workout.__document_id,
    AthleteImageUrl = this.LoadDocument(workout.AthleteId).WallImageUrl
})";
		this.ForEntityNames.Add("WorkoutResults");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "WorkoutResults", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(workout => new {
			AthleteName = String.Format("{0} {1}", this.LoadDocument(workout.AthleteId).FirstName, this.LoadDocument(workout.AthleteId).LastName),
			AthleteId = workout.AthleteId,
			Date = workout.Date,
			Results = workout.Results,
			WorkoutResultId = workout.__document_id,
			AthleteImageUrl = this.LoadDocument(workout.AthleteId).WallImageUrl,
			__document_id = workout.__document_id
		})));
		this.AddField("AthleteName");
		this.AddField("AthleteId");
		this.AddField("Date");
		this.AddField("Results");
		this.AddField("WorkoutResultId");
		this.AddField("AthleteImageUrl");
		this.AddField("__document_id");
	}
}
