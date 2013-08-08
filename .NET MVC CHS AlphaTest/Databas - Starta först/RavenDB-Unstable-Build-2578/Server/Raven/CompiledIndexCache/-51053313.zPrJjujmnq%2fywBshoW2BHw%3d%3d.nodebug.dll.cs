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


public class Index_ContentPage_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_ContentPage_2fView()
	{
		this.ViewText = @"docs.ContentPages.Select(category => new {
    Id = category.__document_id,
    Title = category.Title,
    Url = category.Url,
    MetaData = category.MetaData,
    MenuData = category.MenuData,
    PublishFrom = category.PublishFrom,
    PublishTo = category.PublishTo,
    IsPublished = category.IsPublished,
    Roles = category.Roles,
    AdditionalContent = category.ContentReferences.Select(o => this.LoadDocument(o.ToString(CultureInfo.InvariantCulture)))
})";
		this.ForEntityNames.Add("ContentPages");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "ContentPages", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(category => new {
			Id = category.__document_id,
			Title = category.Title,
			Url = category.Url,
			MetaData = category.MetaData,
			MenuData = category.MenuData,
			PublishFrom = category.PublishFrom,
			PublishTo = category.PublishTo,
			IsPublished = category.IsPublished,
			Roles = category.Roles,
			AdditionalContent = category.ContentReferences.Select((Func<dynamic, dynamic>)(o => this.LoadDocument(o.ToString(CultureInfo.InvariantCulture)))),
			__document_id = category.__document_id
		})));
		this.AddField("Id");
		this.AddField("Title");
		this.AddField("Url");
		this.AddField("MetaData");
		this.AddField("MenuData");
		this.AddField("PublishFrom");
		this.AddField("PublishTo");
		this.AddField("IsPublished");
		this.AddField("Roles");
		this.AddField("AdditionalContent");
		this.AddField("__document_id");
	}
}
