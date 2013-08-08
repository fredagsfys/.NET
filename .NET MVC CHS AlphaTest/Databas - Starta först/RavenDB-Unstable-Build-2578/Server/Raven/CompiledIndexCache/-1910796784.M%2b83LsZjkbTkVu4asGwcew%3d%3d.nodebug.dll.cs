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
    WallId = wallpost.__document_id,
    ScheduleWOD = (string) null,
    ScheduleWodId = (string) null,
    WallPostType = wallpost.WallPostType,
    Results = (string) null,
    AffiliateName = (string) null,
    AchievementPictureUrl = (string) null,
    RankTitle = (string) null
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
    WallId = workout.__document_id,
    ScheduleWOD = this.LoadDocument(workout.ScheduleWodId),
    ScheduleWodId = this.LoadDocument(workout.ScheduleWodId).Id,
    WallPostType = workout.WallPostType,
    Results = workout.Results,
    AffiliateName = this.LoadDocument(this.LoadDocument(workout.ScheduleWodId).AffiliateId).Name,
    AchievementPictureUrl = (string) null,
    RankTitle = (string) null
})
docs.NewRankPosts.Select(newlevel => new {
    FullName = String.Format(""{0} {1}"", this.LoadDocument(newlevel.ReferenceTo).FirstName, this.LoadDocument(newlevel.ReferenceTo).LastName),
    Content = newlevel.Content,
    ReferenceTo = newlevel.ReferenceTo,
    ReferenceFrom = newlevel.ReferenceFrom,
    Date = newlevel.Date,
    Comments = newlevel.Comments.OrderBy(m => m.Created),
    PictureUrl = this.LoadDocument(newlevel.ReferenceTo).ListImageUrl,
    FriendlyUrl = this.LoadDocument(newlevel.ReferenceTo).FriendlyUrl,
    WallId = newlevel.__document_id,
    ScheduleWOD = (string) null,
    ScheduleWodId = (string) null,
    WallPostType = newlevel.WallPostType,
    Results = (string) null,
    AffiliateName = (string) null,
    AchievementPictureUrl = newlevel.AchievementPictureUrl,
    RankTitle = newlevel.RankTitle
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
			WallId = wallpost.__document_id,
			ScheduleWOD = (string)null,
			ScheduleWodId = (string)null,
			WallPostType = wallpost.WallPostType,
			Results = (string)null,
			AffiliateName = (string)null,
			AchievementPictureUrl = (string)null,
			RankTitle = (string)null,
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
			WallId = workout.__document_id,
			ScheduleWOD = this.LoadDocument(workout.ScheduleWodId),
			ScheduleWodId = this.LoadDocument(workout.ScheduleWodId).Id,
			WallPostType = workout.WallPostType,
			Results = workout.Results,
			AffiliateName = this.LoadDocument(this.LoadDocument(workout.ScheduleWodId).AffiliateId).Name,
			AchievementPictureUrl = (string)null,
			RankTitle = (string)null,
			__document_id = workout.__document_id
		})));
		this.ForEntityNames.Add("NewRankPosts");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "NewRankPosts", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(newlevel => new {
			FullName = String.Format("{0} {1}", this.LoadDocument(newlevel.ReferenceTo).FirstName, this.LoadDocument(newlevel.ReferenceTo).LastName),
			Content = newlevel.Content,
			ReferenceTo = newlevel.ReferenceTo,
			ReferenceFrom = newlevel.ReferenceFrom,
			Date = newlevel.Date,
			Comments = newlevel.Comments.OrderBy((Func<dynamic, dynamic>)(m => m.Created)),
			PictureUrl = this.LoadDocument(newlevel.ReferenceTo).ListImageUrl,
			FriendlyUrl = this.LoadDocument(newlevel.ReferenceTo).FriendlyUrl,
			WallId = newlevel.__document_id,
			ScheduleWOD = (string)null,
			ScheduleWodId = (string)null,
			WallPostType = newlevel.WallPostType,
			Results = (string)null,
			AffiliateName = (string)null,
			AchievementPictureUrl = newlevel.AchievementPictureUrl,
			RankTitle = newlevel.RankTitle,
			__document_id = newlevel.__document_id
		})));
		this.AddField("FullName");
		this.AddField("Content");
		this.AddField("ReferenceTo");
		this.AddField("ReferenceFrom");
		this.AddField("Date");
		this.AddField("Comments");
		this.AddField("PictureUrl");
		this.AddField("FriendlyUrl");
		this.AddField("WallId");
		this.AddField("ScheduleWOD");
		this.AddField("ScheduleWodId");
		this.AddField("WallPostType");
		this.AddField("Results");
		this.AddField("AffiliateName");
		this.AddField("AchievementPictureUrl");
		this.AddField("RankTitle");
		this.AddField("__document_id");
	}
}
