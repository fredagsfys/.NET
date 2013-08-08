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


public class Index_MultiWallPost_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_MultiWallPost_2fView()
	{
		this.ViewText = @"docs.WallPosts.Select(wallpost => new {
    FullName = String.Format(""{0} {1}"", this.LoadDocument(wallpost.ReferenceTo).FirstName, this.LoadDocument(wallpost.ReferenceTo).LastName),
    Content = wallpost.Content,
    ReferenceTo = wallpost.ReferenceTo,
    ReferenceFrom = wallpost.ReferenceFrom,
    Date = wallpost.Date,
    Comments = wallpost.Comments.OrderBy(m => m.Created),
    PictureUrl = this.LoadDocument(wallpost.ReferenceTo).ListImageUrl,
    FriendlyUrl = this.LoadDocument(wallpost.ReferenceTo).FriendlyUrl,
    WallPostId = wallpost.__document_id,
    ScheduleWOD = (string) null,
    ScheduleWodId = (string) null,
    WallPostType = wallpost.WallPostType,
    Results = (string) null,
    AffiliateName = (string) null
})
docs.Workouts.Select(workout => new {
    FullName = String.Format(""{0} {1}"", this.LoadDocument(workout.ReferenceTo).FirstName, this.LoadDocument(workout.ReferenceTo).LastName),
    Content = workout.Content,
    ReferenceTo = workout.ReferenceTo,
    ReferenceFrom = workout.ReferenceFrom,
    Date = workout.Date,
    Comments = workout.Comments.OrderBy(m => m.Created),
    PictureUrl = this.LoadDocument(workout.ReferenceTo).ListImageUrl,
    FriendlyUrl = this.LoadDocument(workout.ReferenceTo).FriendlyUrl,
    WallPostId = workout.__document_id,
    ScheduleWOD = this.LoadDocument(workout.ScheduleWodId),
    ScheduleWodId = this.LoadDocument(workout.ScheduleWodId).Id,
    WallPostType = workout.WallPostType,
    Results = workout.Results,
    AffiliateName = this.LoadDocument(this.LoadDocument(workout.ScheduleWodId).AffiliateId).Name
})";
		this.ForEntityNames.Add("WallPosts");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "WallPosts", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(wallpost => new {
			FullName = String.Format("{0} {1}", this.LoadDocument(wallpost.ReferenceTo).FirstName, this.LoadDocument(wallpost.ReferenceTo).LastName),
			Content = wallpost.Content,
			ReferenceTo = wallpost.ReferenceTo,
			ReferenceFrom = wallpost.ReferenceFrom,
			Date = wallpost.Date,
			Comments = wallpost.Comments.OrderBy((Func<dynamic, dynamic>)(m => m.Created)),
			PictureUrl = this.LoadDocument(wallpost.ReferenceTo).ListImageUrl,
			FriendlyUrl = this.LoadDocument(wallpost.ReferenceTo).FriendlyUrl,
			WallPostId = wallpost.__document_id,
			ScheduleWOD = (string)null,
			ScheduleWodId = (string)null,
			WallPostType = wallpost.WallPostType,
			Results = (string)null,
			AffiliateName = (string)null,
			__document_id = wallpost.__document_id
		})));
		this.ForEntityNames.Add("Workouts");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Workouts", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(workout => new {
			FullName = String.Format("{0} {1}", this.LoadDocument(workout.ReferenceTo).FirstName, this.LoadDocument(workout.ReferenceTo).LastName),
			Content = workout.Content,
			ReferenceTo = workout.ReferenceTo,
			ReferenceFrom = workout.ReferenceFrom,
			Date = workout.Date,
			Comments = workout.Comments.OrderBy((Func<dynamic, dynamic>)(m => m.Created)),
			PictureUrl = this.LoadDocument(workout.ReferenceTo).ListImageUrl,
			FriendlyUrl = this.LoadDocument(workout.ReferenceTo).FriendlyUrl,
			WallPostId = workout.__document_id,
			ScheduleWOD = this.LoadDocument(workout.ScheduleWodId),
			ScheduleWodId = this.LoadDocument(workout.ScheduleWodId).Id,
			WallPostType = workout.WallPostType,
			Results = workout.Results,
			AffiliateName = this.LoadDocument(this.LoadDocument(workout.ScheduleWodId).AffiliateId).Name,
			__document_id = workout.__document_id
		})));
		this.AddField("FullName");
		this.AddField("Content");
		this.AddField("ReferenceTo");
		this.AddField("ReferenceFrom");
		this.AddField("Date");
		this.AddField("Comments");
		this.AddField("PictureUrl");
		this.AddField("FriendlyUrl");
		this.AddField("WallPostId");
		this.AddField("ScheduleWOD");
		this.AddField("ScheduleWodId");
		this.AddField("WallPostType");
		this.AddField("Results");
		this.AddField("AffiliateName");
		this.AddField("__document_id");
	}
}
