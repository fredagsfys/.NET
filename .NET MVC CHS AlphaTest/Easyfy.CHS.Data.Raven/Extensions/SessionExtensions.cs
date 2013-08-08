using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;

namespace Easyfy.CHS.Data.Raven.Extensions
{
	public static class SessionExtensions
	{
		public static string GetId<T>(this IDocumentSession session, int id) {
			return session.Advanced.DocumentStore.Conventions.FindFullDocumentKeyFromNonStringIdentifier(id, typeof (T), false);
		}
	}
}