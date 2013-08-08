using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Model.Wod;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;

namespace Easyfy.CHS.Data.Raven {
  public class RavenStore {
    private static IDocumentStore _documentStore;

    /// <summary>
    /// Singleton implementation to prevent multiple stores instantiating
    /// </summary>
    public static IDocumentStore DocumentStore {
      get {
        if (_documentStore != null)
          return _documentStore;

        lock (typeof (RavenController)) {
          if (_documentStore != null)
            return _documentStore;

          _documentStore = new DocumentStore {
            ConnectionStringName = "RavenDb",
          }
          .Initialize();

          //_documentStore.Conventions.IdentityPartsSeparator = "-";


          if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"]))
            _documentStore.DatabaseCommands.EnsureDatabaseExists(
              ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"]);


          var wodindex = new CompositionContainer(new AssemblyCatalog(typeof(Wod_Search).Assembly));
          IndexCreation.CreateIndexes(wodindex,
                                      _documentStore.DatabaseCommands.ForDatabase(
                                        ConfigurationManager.AppSettings["RavenDb.DefaultDatabase"]),
                                      _documentStore.Conventions);
        }

        return _documentStore;
      }
    }

    public static IDocumentSession GetSession() {
      return DocumentStore.OpenSession(ConfigurationManager.AppSettings["RavenDb.DefaultDataBase"]);
    }
  }
}