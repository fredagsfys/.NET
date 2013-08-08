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


public class Index_Banner_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Banner_2fView()
	{
		this.ViewText = @"docs.BannerSections.Select(banner => new {
    Id = banner.__document_id,
    IsPublished = banner.IsPublished,
    Section = banner.Section,
    Banners = banner.BannerReferences.Select(o => this.LoadDocument(o.ToString(CultureInfo.InvariantCulture)))
})";
		this.ForEntityNames.Add("BannerSections");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "BannerSections", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(banner => new {
			Id = banner.__document_id,
			IsPublished = banner.IsPublished,
			Section = banner.Section,
			Banners = banner.BannerReferences.Select((Func<dynamic, dynamic>)(o => this.LoadDocument(o.ToString(CultureInfo.InvariantCulture)))),
			__document_id = banner.__document_id
		})));
		this.AddField("Id");
		this.AddField("IsPublished");
		this.AddField("Section");
		this.AddField("Banners");
		this.AddField("__document_id");
	}
}
