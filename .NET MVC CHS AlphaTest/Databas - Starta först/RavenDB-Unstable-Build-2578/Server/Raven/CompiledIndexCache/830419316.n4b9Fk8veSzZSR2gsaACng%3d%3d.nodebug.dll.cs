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


public class Index_Notification_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Notification_2fView()
	{
		this.ViewText = @"docs.Notifications.Select(notification => new {
    FullName = String.Format(""{0} {1}"", this.LoadDocument(notification.ReferenceFrom).FirstName, this.LoadDocument(notification.ReferenceFrom).LastName),
    ReferenceFrom = notification.ReferenceFrom,
    ReferenceTo = notification.ReferenceTo,
    Created = notification.Created,
    Content = notification.Content,
    WallPostId = notification.WallPostId,
    NotificationType = notification.NotificationType,
    NotificationId = notification.__document_id,
    PictureUrl = this.LoadDocument(notification.ReferenceFrom).WallImageUrl,
    FriendlyUrl = this.LoadDocument(notification.ReferenceTo).FriendlyUrl
})";
		this.ForEntityNames.Add("Notifications");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Notifications", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(notification => new {
			FullName = String.Format("{0} {1}", this.LoadDocument(notification.ReferenceFrom).FirstName, this.LoadDocument(notification.ReferenceFrom).LastName),
			ReferenceFrom = notification.ReferenceFrom,
			ReferenceTo = notification.ReferenceTo,
			Created = notification.Created,
			Content = notification.Content,
			WallPostId = notification.WallPostId,
			NotificationType = notification.NotificationType,
			NotificationId = notification.__document_id,
			PictureUrl = this.LoadDocument(notification.ReferenceFrom).WallImageUrl,
			FriendlyUrl = this.LoadDocument(notification.ReferenceTo).FriendlyUrl,
			__document_id = notification.__document_id
		})));
		this.AddField("FullName");
		this.AddField("ReferenceFrom");
		this.AddField("ReferenceTo");
		this.AddField("Created");
		this.AddField("Content");
		this.AddField("WallPostId");
		this.AddField("NotificationType");
		this.AddField("NotificationId");
		this.AddField("PictureUrl");
		this.AddField("FriendlyUrl");
		this.AddField("__document_id");
	}
}
