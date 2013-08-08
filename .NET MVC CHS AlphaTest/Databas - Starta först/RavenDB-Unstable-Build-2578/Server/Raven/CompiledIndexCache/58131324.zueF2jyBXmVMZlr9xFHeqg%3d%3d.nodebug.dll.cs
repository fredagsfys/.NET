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


public class Index_Affiliate_2fSearch : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Affiliate_2fSearch()
	{
		this.ViewText = @"docs.Affiliates.Select(affiliate => new {
    Id = affiliate.__document_id,
    FriendlyUrl = affiliate.FriendlyUrl,
    Name = affiliate.Name,
    Description = affiliate.Description,
    Created = affiliate.AuditInfo.CreatedOn,
    Address = affiliate.Address,
    Postal = affiliate.Postal,
    City = affiliate.City,
    Country = affiliate.Country,
    Latitude = affiliate.Latitude,
    Longitude = affiliate.Longitude,
    Image = affiliate.Image
})
results.Select(affiliate => new {
    Id = affiliate.Id,
    FriendlyUrl = affiliate.FriendlyUrl,
    Name = affiliate.Name,
    Description = affiliate.Description,
    Created = affiliate.Created,
    Address = affiliate.Address,
    Postal = affiliate.Postal,
    City = affiliate.City,
    Country = affiliate.Country,
    Latitude = affiliate.Latitude,
    Longitude = affiliate.Longitude,
    Image = affiliate.Image,
    Score = affiliate[""@metadata""].Value(""Temp-Index-Score"")
})";
		this.ForEntityNames.Add("Affiliates");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Affiliates", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(affiliate => new {
			Id = affiliate.__document_id,
			FriendlyUrl = affiliate.FriendlyUrl,
			Name = affiliate.Name,
			Description = affiliate.Description,
			Created = affiliate.AuditInfo.CreatedOn,
			Address = affiliate.Address,
			Postal = affiliate.Postal,
			City = affiliate.City,
			Country = affiliate.Country,
			Latitude = affiliate.Latitude,
			Longitude = affiliate.Longitude,
			Image = affiliate.Image,
			__document_id = affiliate.__document_id
		})));
		this.TransformResultsDefinition = (Database, results) => results.Select((Func<dynamic, dynamic>)(affiliate => new {
			Id = affiliate.Id,
			FriendlyUrl = affiliate.FriendlyUrl,
			Name = affiliate.Name,
			Description = affiliate.Description,
			Created = affiliate.Created,
			Address = affiliate.Address,
			Postal = affiliate.Postal,
			City = affiliate.City,
			Country = affiliate.Country,
			Latitude = affiliate.Latitude,
			Longitude = affiliate.Longitude,
			Image = affiliate.Image,
			Score = affiliate["@metadata"].Value("Temp-Index-Score")
		}));
		this.AddField("Id");
		this.AddField("FriendlyUrl");
		this.AddField("Name");
		this.AddField("Description");
		this.AddField("Created");
		this.AddField("Address");
		this.AddField("Postal");
		this.AddField("City");
		this.AddField("Country");
		this.AddField("Latitude");
		this.AddField("Longitude");
		this.AddField("Image");
		this.AddField("__document_id");
	}
}
