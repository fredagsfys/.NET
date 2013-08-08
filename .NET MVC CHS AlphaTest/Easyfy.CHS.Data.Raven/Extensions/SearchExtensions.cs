using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Easyfy.CHS.Data.Raven.Facets;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.ViewModel;
using Easyfy.CHS.Model.Wod;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Linq;

namespace Easyfy.CHS.Data.Raven.Extensions
{
  public static class SearchExtensions
  {
    public static WodSearchViewModel AutoSearchWods(this IDocumentSession session, string searchWord, List<FacetFilter> filters) {
      var model = new WodSearchViewModel {CheckboxFilterList = filters};

      if (string.IsNullOrEmpty(searchWord) || searchWord == "*") {
        var wodlistempty = session.Advanced.LuceneQuery<WodBase, Wod_Search>()
                                  .SelectFields<WodListProjection>();

        //wodlistempty.Where("(Name:sp*^10 OR WodType:sp*) AND ExerciseList:deadlift"); // OR (WodType:(*) AND @in<ExerciseList>:(burpee) AND @in<ExerciseList>:(deadlift)) OR (BenchmarkType:(*) AND @in<ExerciseList>:(burpee) AND @in<ExerciseList>:(deadlift)) OR (ExerciseSearchField:(*) AND @in<ExerciseList>:(burpee)  AND @in<ExerciseList>:(deadlift))
        wodlistempty.UpdateQueryWithFilter(filters);

        model.WodList = wodlistempty.Take(100).ToList();

        model.FacetResults = session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Wod/Search", new IndexQuery { Query = wodlistempty.ToString() }, "Facets/WodFacets");

        return model;
      }

      RavenQueryStatistics stats;
      var wodlist = session.Advanced.LuceneQuery<WodBase, Wod_Search>()
                           .Statistics(out stats)
                           .SelectFields<WodListProjection>()
                           .Where("(")
                           .Search(x => x.Name, searchWord)
                           .Boost(10)
                           .OrElse().Search(x => x.WodType, searchWord)
                           .OrElse().Search(x => x.BenchmarkType, searchWord)
                           .OrElse().Search(x => x.ExerciseSearchField, searchWord)
                           .Where(")");

      wodlist.UpdateQueryWithFilter(filters);

      model.WodList = wodlist.Take(100).ToList();

      //model.WodList.ForEach(o=>o.Score = GetRelevance(session,o));

      // Add facets to CategoryModel
      model.FacetResults =
        session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Wod/Search", new IndexQuery { Query = wodlist.ToString() }, "Facets/WodFacets");


      return model;
    }

    public static void UpdateQueryWithFilter(this IDocumentQuery<WodListProjection> query, List<FacetFilter> filters)
    {
      foreach (var item in filters) {
        query.AndAlso()
             .WhereEquals(item.Key, item.Value);
      }

      // This is for or filter in exerciselist, don't need it now.
      //if (filters.Any(o => o.Key == "ExerciseList"))
      //  query.AndAlso().WhereIn("ExerciseList", filters.Where(o => o.Key == "ExerciseList").Select(o=>o.Value).ToArray());
      
      //foreach (var item in filters.Where(o => o.Key == "ExerciseList")) {
      //  query.AndAlso().WhereIn("ExerciseList", new[] {item.Value});
      //}
    
    }

    private static double GetRelevance<T>(IDocumentSession session, T candidate)
    {
      return session
        .Advanced
        .GetMetadataFor(candidate)
        .Value<double>("Temp-Index-Score");
    }

    public static AffiliateSearchViewModel AutoSearchAffiliate(this IDocumentSession session, string searchWord, List<FacetFilter> filters) {

      var model = new AffiliateSearchViewModel { CheckboxFilterList = filters };

      if (string.IsNullOrEmpty(searchWord)) {
        var emptyList = session.Advanced.LuceneQuery<Affiliate, Affiliate_Search>()
                             .SelectFields<AffiliateListProjection>();

        emptyList.AffiliateUpdateWithFilter(filters);

        model.AffiliateList = emptyList.Take(100).ToList();

        model.FacetResults = session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Affiliate/Search", new IndexQuery { Query = emptyList.ToString() }, "Facets/AffiliateFacets");

        return model;
      }

      var list = session.Advanced.LuceneQuery<Affiliate, Affiliate_Search>()
                     .SelectFields<AffiliateListProjection>()
                     .Where("(")
                     .Search(x => x.Name, searchWord)
                     .Boost(10)
                     .OrElse().Search(x => x.City, searchWord)
                     .OrElse().Search(x => x.Country, searchWord)
                     .Where(")");

      list.AffiliateUpdateWithFilter(filters);

      model.AffiliateList = list.Take(100).ToList();

      model.FacetResults =
        session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Affiliate/Search", new IndexQuery { Query = list.ToString() }, "Facets/AffiliateFacets");

      return model;
    }

    public static void AffiliateUpdateWithFilter(this IDocumentQuery<AffiliateListProjection> query, List<FacetFilter> filters)
    {
      foreach (var item in filters)
      {
        query.AndAlso()
              .WhereEquals(item.Key, item.Value);
      }
    }

    public static AthleteSearchViewModel AutoSearchAthletes(this IDocumentSession session, string searchWord, List<FacetFilter> filters, int page, int pageSize) {
      RavenQueryStatistics stats;

      var model = new AthleteSearchViewModel { CheckboxFilterList = filters };

      if (string.IsNullOrEmpty(searchWord)) {
        var emptyList = session.Advanced.LuceneQuery<Athlete, Athlete_Search>()
                             .SelectFields<AthleteListProjection>();

        emptyList.AthleteUpdateWithFilter(filters);

        model.AthleteList = emptyList.Statistics(out stats).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        model.FacetResults = session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Athlete/Search", new IndexQuery { Query = emptyList.ToString() }, "Facets/AthleteFacets");

        model.TotalPages = (int) Math.Ceiling((decimal) stats.TotalResults/pageSize);
        model.PageSize = pageSize;
        model.Page = page;

        return model;
      }

      var list = session.Advanced.LuceneQuery<Athlete, Athlete_Search>()
                     .Statistics(out stats)
                     .SelectFields<AthleteListProjection>()
                     .Where("(")
                     .Search(x => x.FirstName, searchWord)
                     .Boost(10)
                     .OrElse().Search(x => x.Username, searchWord)
                     .OrElse().Search(x => x.LastName, searchWord)
                     .OrElse().Search(x => x.City, searchWord)
                     .OrElse().Search(x => x.Country, searchWord)
                     .OrElse().Search(x => x.AffiliateSearchField, searchWord)
                     .Where(")");

      list.AthleteUpdateWithFilter(filters);

      model.AthleteList = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

      model.FacetResults =
        session.Advanced.DocumentStore.DatabaseCommands.ForDatabase(
          ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"])
               .GetFacets("Athlete/Search", new IndexQuery { Query = list.ToString() }, "Facets/AthleteFacets");

      model.TotalPages = (int)Math.Ceiling((decimal)stats.TotalResults / pageSize);
      model.PageSize = pageSize;
      model.Page = page;

      return model;
    }

    public static void AthleteUpdateWithFilter(this IDocumentQuery<AthleteListProjection> query, List<FacetFilter> filters)
    {
      foreach (var item in filters)
      {
        query.AndAlso()
              .WhereEquals(item.Key, item.Value);
      }
    }
  }
}