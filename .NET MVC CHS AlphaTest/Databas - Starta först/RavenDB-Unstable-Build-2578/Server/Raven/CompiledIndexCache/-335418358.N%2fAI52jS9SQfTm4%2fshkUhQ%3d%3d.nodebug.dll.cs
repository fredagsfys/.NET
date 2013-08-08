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


public class Index_WallPost_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_WallPost_2fView()
	{
		this.ViewText = @"docs.WallPosts.Select(wallpost => new {
    FullName = String.Format(""{0} {1}"", this.LoadDocument(wallpost.ReferenceTo).FirstName, this.LoadDocument(wallpost.ReferenceTo).LastName),
    ReferenceTo = wallpost.ReferenceTo,
    ReferenceFrom = wallpost.ReferenceFrom,
    Date = wallpost.Date,
    Content = wallpost.Content,
    Comments = wallpost.Comments.OrderBy(m => m.Created),
    PictureUrl = this.LoadDocument(wallpost.ReferenceTo).ListImageUrl,
    WallId = wallpost.__document_id,
    FriendlyUrl = this.LoadDocument(wallpost.ReferenceTo).FriendlyUrl
})";
		this.ForEntityNames.Add("WallPosts");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "WallPosts", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(wallpost => new {
			FullName = String.Format("{0} {1}", this.LoadDocument(wallpost.ReferenceTo).FirstName, this.LoadDocument(wallpost.ReferenceTo).LastName),
			ReferenceTo = wallpost.ReferenceTo,
			ReferenceFrom = wallpost.ReferenceFrom,
			Date = wallpost.Date,
			Content = wallpost.Content,
			Comments = wallpost.Comments.OrderBy((Func<dynamic, dynamic>)(m => m.Created)),
			PictureUrl = this.LoadDocument(wallpost.ReferenceTo).ListImageUrl,
			WallId = wallpost.__document_id,
			FriendlyUrl = this.LoadDocument(wallpost.ReferenceTo).FriendlyUrl,
			__document_id = wallpost.__document_id
		})));
		this.AddField("FullName");
		this.AddField("ReferenceTo");
		this.AddField("ReferenceFrom");
		this.AddField("Date");
		this.AddField("Content");
		this.AddField("Comments");
		this.AddField("PictureUrl");
		this.AddField("WallId");
		this.AddField("FriendlyUrl");
		this.AddField("__document_id");
	}
}
