using System;
using System.Collections.Generic;
using System.Linq;
using Easyfy.CHS.Data.Raven.Facets;
using Easyfy.CHS.Model.Wod;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Exercise;
using Easyfy.CHS.Model.Affiliate;

namespace Easyfy.CHS.App_Start
{
  public static class EnsureDataExists
  {
    public static void CheckData()
    {
      var ds = new DocumentStore {ConnectionStringName = "RavenDB"}.Initialize();

      using (var documentSession = ds.OpenSession())
      {

        EnsureAthleteExists(documentSession);
        EnsureAffiliateExists(documentSession);
        EnsureExerciseExists(documentSession);
        EnsureWodExists(documentSession);
        EnsureFacetExists(documentSession);
      }
    }

    private static void EnsureAthleteExists(IDocumentSession documentSession)
    {
      //Kontrollerar att de finns någon användare..
      if (documentSession.Query<Athlete>().Any())
      {
        return;
      }

      //Ny användare, lösen qwerty, fyll på om du behöver fler
      var user = new Athlete
        {
          FirstName = "Easyfy",
          LastName = "Easyfy",
          Email = "easyfy@pewpew.xxx",
          Username = "Easyfy",
          Password = "YmSfqXyvNCMLbjrOsBDfWawzgxW6T1cqln851yWwRUk=",
          Salt = "Ac07XEQplpqNOeisFYtrBg==",
          City = "Kalmar",
          Country = "Sweden",
          Gender = "Male",
          Admin = true
        };

      documentSession.Store(user);

      documentSession.SaveChanges();


    }

    private static void EnsureExerciseExists(IDocumentSession documentSession)
    {
      if (documentSession.Query<ExerciseBase>().Any())
      {
        return;
      }

      var pullup = new ExerciseBase
        {
          Name = "Pull-up",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.Gymnastic,
        };
      var pushup = new ExerciseBase
        {
          Name = "Push-up",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.Gymnastic,
        };
      var powerclean = new ExerciseBase
        {
          Name = "Power-clean",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.WeightLift,
        };
      var deadlift = new ExerciseBase
        {
          Name = "Deadlift",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.WeightLift,
        };
      var running = new ExerciseBase
        {
          Name = "Run",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          Length = 400,
          LengthType = LengthType.Meter,
          ExerciseType = ExerciseType.Metabolic,
        };
      var burpee = new ExerciseBase
        {
          Name = "Burpee",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.Gymnastic,
        };
      var thruster = new ExerciseBase
        {
          Name = "Thruster",
          AuditInfo = new AuditInformation {CreatedBy = "Easyfy", CreatedOn = DateTime.Now},
          ExerciseType = ExerciseType.WeightLift,
        };

      var kettlebells = new ExerciseBase
      {
        Name = "Kettlebells",
        AuditInfo = new AuditInformation { CreatedBy = "Easyfy", CreatedOn = DateTime.Now },
        ExerciseType = ExerciseType.WeightLift,
      };

      //Sparar ner i store
      documentSession.Store(kettlebells);
      documentSession.Store(pullup);
      documentSession.Store(pushup);
      documentSession.Store(powerclean);
      documentSession.Store(deadlift);
      documentSession.Store(running);
      documentSession.Store(burpee);
      documentSession.Store(thruster);

      documentSession.SaveChanges();

    }

    private static void EnsureWodExists(IDocumentSession documentSession)
    {

      var affiliateRef = documentSession.Query<Affiliate>().SingleOrDefault(m => m.FriendlyUrl == "Crossfit-Kalmar");
      var allExercises = documentSession.Query<ExerciseBase>().Customize(o=>o.WaitForNonStaleResultsAsOfNow()).ToList();

      var deadlift = allExercises.Find(u => u.Name == "Deadlift");
      var burpee = allExercises.Find(u => u.Name == "Burpee");
      var pullup = allExercises.Find(u => u.Name == "Pull-up");
      var pushup = allExercises.Find(u => u.Name == "Push-up");
      var thruster = allExercises.Find(u => u.Name == "Thruster");
      var running = allExercises.Find(u => u.Name == "Run" && u.Length == 400);
      var kettlebells = allExercises.Find(u => u.Name == "Kettlebells");

      if (affiliateRef != null) {

        if (!documentSession.Query<WodBase>().Any(o => o.Name == "Helen")) {
          var helen = new WodBase {
            AffiliateReference = new AffiliateReference {
              Id = affiliateRef.Id,
              Name = affiliateRef.Name,
              FriendlyUrl = affiliateRef.FriendlyUrl,
            },
            Name = "Helen",
            WodType = WodType.TimeWod,
            BenchmarkType = BenchmarkType.Girls,
            Description = "Complete all reps as fast as possible",
            DefaultResultAggregation = ResultAggregation.Min,
            DefaultResultType = ResultType.Time,
            LowerIsBetter = true,
            Rounds = new List<Round> {
              new Round {
                Laps = 3,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(running) {
                    Reps = 1
                  },
                  new ExerciseRound(kettlebells) {
                    Reps = 24,
                    Weight = 24,
                    WeightType = WeightType.Fix,
                    WeightUnit = WeightUnit.Kilogram
                  },
                  new ExerciseRound(pullup) {
                    Reps = 12
                  }
                }
              }
            }
          };
          documentSession.Store(helen);
        }

        if (!documentSession.Query<WodBase>().Any(o => o.Name == "Stinger")) {

          var stinger = new WodBase {
            AffiliateReference = new AffiliateReference {
              Id = affiliateRef.Id,
              Name = affiliateRef.Name,
              FriendlyUrl = affiliateRef.FriendlyUrl,
            },

            Name = "Stinger",
            WodType = WodType.RepsWod,
            BenchmarkType = BenchmarkType.NotBenchMark,
            Description = "8min AMRAP, Deadlifts & Burpees",
            Time = new TimeSpan(0, 00, 8, 00, 00),
            DefaultResultAggregation = ResultAggregation.None,
            DefaultResultType = ResultType.Reps,
            LowerIsBetter = false,
            Rounds = new List<Round> {
              new Round {
                Laps = 1,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(deadlift) {
                    Reps = 8,
                    Weight = 90.0,
                    WeightUnit = WeightUnit.Kilogram,
                  },
                  new ExerciseRound(burpee) {
                    Reps = 8,
                  },
                },
              }
            }
          };
          documentSession.Store(stinger);
        }

        if (!documentSession.Query<WodBase>().Any(o => o.Name == "300 Spartan")) {

          var spartan = new WodBase {
            AffiliateReference = new AffiliateReference {
              Id = affiliateRef.Id,
              Name = affiliateRef.Name,
              FriendlyUrl = affiliateRef.FriendlyUrl,
            },

            Name = "300 Spartan",
            WodType = WodType.TimeWod,
            BenchmarkType = BenchmarkType.NotBenchMark,
            Description = "",
            Time = new TimeSpan(0),
            DefaultResultAggregation = ResultAggregation.None,
            DefaultResultType = ResultType.Time,
            LowerIsBetter = true,
            Rounds = new List<Round> {
              new Round {
                Laps = 1,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(pullup) {
                    Reps = 25,
                  },
                  new ExerciseRound(deadlift) {
                    Reps = 50,
                    Weight = 60.0,
                    WeightUnit = WeightUnit.Kilogram,
                  },
                  new ExerciseRound(pushup) {
                    Reps = 50,
                  },
                },
              }
            }
          };
          documentSession.Store(spartan);
        }

        if (!documentSession.Query<WodBase>().Any(o => o.Name == "Fran")) {

          var fran = new WodBase {
            AffiliateReference = new AffiliateReference {
              Id = affiliateRef.Id,
              Name = affiliateRef.Name,
              FriendlyUrl = affiliateRef.FriendlyUrl,
            },

            Name = "Fran",
            WodType = WodType.TimeWod,
            BenchmarkType = BenchmarkType.Girls,
            Description = "",
            Time = new TimeSpan(0),
            DefaultResultAggregation = ResultAggregation.None,
            DefaultResultType = ResultType.Time,
            LowerIsBetter = true,
            Rounds = new List<Round> {
              new Round {
                Laps = 1,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(thruster) {
                    Reps = 21,
                    Weight = 43.0,
                    WeightUnit = WeightUnit.Kilogram,
                  },
                  new ExerciseRound(pullup) {
                    Reps = 21,
                  },
                },
              },
              new Round {
                Laps = 1,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(thruster) {
                    Reps = 15,
                    Weight = 43.0,
                    WeightUnit = WeightUnit.Kilogram,
                  },
                  new ExerciseRound(pullup) {
                    Reps = 15,
                  },
                },
              },
              new Round {
                Laps = 1,
                SortOrder = 0,
                ExerciseRounds = new List<ExerciseRound> {
                  new ExerciseRound(thruster) {
                    Reps = 9,
                    Weight = 43.0,
                    WeightUnit = WeightUnit.Kilogram,
                  },
                  new ExerciseRound(pullup) {
                    Reps = 9,
                  },
                },
              }
            }
          };
          documentSession.Store(fran);
        }

        documentSession.SaveChanges();
      }
    }

    private static void EnsureAffiliateExists(IDocumentSession documentSession)
    {
      if (documentSession.Query<Affiliate>().Any())
      {
        return;
      }

      //Ny affiliate, fyll på om du behöver fler
      var affiliate = new Affiliate
        {
          Name = "Crossfit Kalmar",
          Address = "Trångsundsvägen 2",
          City = "Kalmar",
          Country = "Sweden",
          Postal = "392 38",
          FriendlyUrl = "Crossfit-Kalmar"
        };

      var affiliate2 = new Affiliate
      {
        Name = "Crossfit Kiruna",
        Address = "Borgenvägen 1",
        City = "Kiruna",
        Country = "Sweden",
        Postal = "981 41",
        FriendlyUrl = "Crossfit-Kiruna"
      };

      var affiliate3 = new Affiliate
      {
        Name = "Crossfit Linköping",
        Address = "Gillbergagatan 41",
        City = "Linköping",
        Country = "Sweden",
        Postal = "582 73",
        FriendlyUrl = "Crossfit-Linköping"
      };

      documentSession.Store(affiliate);
      documentSession.Store(affiliate2);
      documentSession.Store(affiliate3);
      documentSession.SaveChanges();

    }

    private static void EnsureFacetExists(IDocumentSession documentSession) {
      var wodFacets = new WodFacets();
      var affiliateFacets = new AffiliateFacets();
      var athleteFacets = new AthleteFacets();

      if (documentSession.Load<FacetSetup>(wodFacets.FacetKey) == null) {

        var wodSetup = new FacetSetup() {
          Id = wodFacets.FacetKey,
          Facets = wodFacets.FacetList
        };

        documentSession.Store(wodSetup);
        documentSession.SaveChanges();
      }

      if (documentSession.Load<FacetSetup>(affiliateFacets.FacetKey) == null) {
        var affiliateSetup = new FacetSetup()
        {
          Id = affiliateFacets.FacetKey,
          Facets = affiliateFacets.FacetList
        };

        documentSession.Store(affiliateSetup);
        documentSession.SaveChanges();
      }

      if (documentSession.Load<FacetSetup>(athleteFacets.FacetKey) == null)
      {
        var athleteSetup = new FacetSetup()
        {
          Id = athleteFacets.FacetKey,
          Facets = athleteFacets.FacetList
        };

        documentSession.Store(athleteSetup);
        documentSession.SaveChanges();
      }
    }
  }
}