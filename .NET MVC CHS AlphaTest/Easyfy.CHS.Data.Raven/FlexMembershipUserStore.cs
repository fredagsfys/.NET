using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FlexProviders.Membership;
using Microsoft.Web.WebPages.OAuth;
using Raven.Client;

namespace FlexProviders.Raven
{
    public class FlexMembershipUserStore<TUser>
        : IFlexUserStore
          where TUser : class,IFlexMembershipUser, new()
    {
        private readonly IDocumentSession _session;

        public FlexMembershipUserStore(IDocumentSession session)
        {
            _session = session;
        }

        public IFlexMembershipUser GetUserByUsername(string username)
        {
            return _session.Query<TUser>().SingleOrDefault(u => u.Username == username);
        }

        public IFlexMembershipUser Add(IFlexMembershipUser user)
        {
            _session.Store(user);
            _session.SaveChanges();
            return user;
        }

        public IFlexMembershipUser Save(IFlexMembershipUser user)
        {
            var existingUser = _session.Query<TUser>().SingleOrDefault(u => u.Username == user.Username);
            foreach(var property in user.GetType().GetProperties().Where(p => p.CanWrite))
            {
                property.SetValue(existingUser, property.GetValue(user));
            }
            _session.SaveChanges();
            return user;
        }

        public bool DeleteOAuthAccount(string provider, string providerUserId)
        {
            var user =
                _session.Query<TUser>()
                        .SingleOrDefault(u => u.OAuthAccounts
                                               .Any(o => o.ProviderUserId == providerUserId && o.Provider == provider));
            
            if(user != null)
            {
                var account =
                    user.OAuthAccounts.Single(o => o.Provider == provider && o.ProviderUserId == providerUserId);
                 user.OAuthAccounts.Remove(account);
                _session.SaveChanges();
                 return true;
            }           
            return false;
        }

        public IFlexMembershipUser GetUserByPasswordResetToken(string passwordResetToken)
        {
            return
                _session.Query<TUser>().SingleOrDefault(u => u.PasswordResetToken == passwordResetToken);
        }

        public IFlexMembershipUser GetUserByOAuthProvider(string provider, string providerUserId)
        {
            return
                _session.Query<TUser>().SingleOrDefault(u => u.OAuthAccounts.Any(r => r.Provider == provider && r.ProviderUserId == providerUserId));
        }

        public IFlexMembershipUser CreateOAuthAccount(string provider, string providerUserId, IFlexMembershipUser user)
        {
            var account = new FlexOAuthAccount {Provider = provider, ProviderUserId = providerUserId};
            if (user.OAuthAccounts == null)
            {
                user.OAuthAccounts = new Collection<FlexOAuthAccount>();
            }
            user.OAuthAccounts.Add(account);           
            _session.SaveChanges();

            return user;
        }

        public IEnumerable<OAuthAccount> GetOAuthAccountsForUser(string username)
        {
            return _session
                .Query<TUser>()
                .Single(u => u.Username == username)
                .OAuthAccounts
                .ToArray()
                .Select(o => new OAuthAccount(o.Provider, o.ProviderUserId));
        }
    }
}