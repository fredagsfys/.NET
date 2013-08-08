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


public class Index_Exercise_2fSearch : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Exercise_2fSearch()
	{
		this.ViewText = @"docs.ExerciseBases.Select(exercise => new {
    Id = exercise.__document_id,
    ExerciseTitle = exercise.ExerciseType == ""Metabolic"" && ((double) exercise.Length) > 0 ? String.Format(""{0} {1} {2}"", exercise.Name, ((object)((double) exercise.Length)), exercise.LengthTypeToString) : exercise.Name,
    Group = exercise.Group,
    ExerciseType = exercise.ExerciseType,
    FriendlyUrl = exercise.FriendlyUrl
})";
		this.ForEntityNames.Add("ExerciseBases");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "ExerciseBases", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(exercise => new {
			Id = exercise.__document_id,
			ExerciseTitle = exercise.ExerciseType == "Metabolic" && ((double)exercise.Length) > 0 ? String.Format("{0} {1} {2}", exercise.Name, ((object)((double)exercise.Length)), exercise.LengthTypeToString) : exercise.Name,
			Group = exercise.Group,
			ExerciseType = exercise.ExerciseType,
			FriendlyUrl = exercise.FriendlyUrl,
			__document_id = exercise.__document_id
		})));
		this.AddField("Id");
		this.AddField("ExerciseTitle");
		this.AddField("Group");
		this.AddField("ExerciseType");
		this.AddField("FriendlyUrl");
		this.AddField("__document_id");
	}
}
