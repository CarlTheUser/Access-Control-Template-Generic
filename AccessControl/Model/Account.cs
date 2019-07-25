using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Model
{
    public class Account
    {
        #region Factory Methods

        public static Account New(string username, HashedPassword password, IEnumerable<AccessPriviledge> priviledges)
        {
            CheckUsernameValidity(username);
            CheckPasswordValidity(password);
            CheckPriviledgesValidity(priviledges);

            return new Account(null, username, password, priviledges);
        }

        public static Account Existing(Identity identity, string username, HashedPassword password, IEnumerable<AccessPriviledge> priviledges)
        {
            return new Account(identity, username, password, priviledges);
        }

        #endregion

        #region Properties

        public Identity Identity { get; private set; }

        public string Username { get; private set; }

        public HashedPassword Password { get; private set; }

        public IEnumerable<AccessPriviledge> Priviledges => _Priviledges;

        private readonly List<AccessPriviledge> _Priviledges;

        #endregion

        #region Constructors

        private Account(
            Identity identity,
            string username,
            HashedPassword password,
            IEnumerable<AccessPriviledge> priviledges)
        {
            Identity = identity;
            Username = username;
            Password = password;
            _Priviledges = priviledges.ToList();
        }

        #endregion

        #region public Behaviors

        public void InitializeIdentity(Identity identity)
        {
            if (identity == null) throw new AccessDomainViolationException("Cannot initialize account identity with null value.");
            if (Identity != null) throw new AccessDomainViolationException("Identity for account has already been initialized with value.");
            Identity = identity;
        }

        public void Rename(string newUsername)
        {
            CheckUsernameValidity(newUsername);
            Username = newUsername;
        }

        public void ChangePassword(HashedPassword newPassword)
        {
            CheckPasswordValidity(newPassword);
            Password = newPassword;
        }

        public void AddPriviledge(AccessPriviledge priviledge)
        {
            if (IsInPriviledge(priviledge)) throw new AccessDomainViolationException("Account is already in priviledge.");
            _Priviledges.Add(priviledge);
        }

        public void RemovePriviledge(AccessPriviledge priviledge)
        {
            _Priviledges.Remove(priviledge);
            CheckPriviledgesValidity(_Priviledges);
        }

        public bool IsInPriviledge(AccessPriviledge priviledge)
        {
            return Priviledges.Contains(priviledge);
        }

        #endregion

        #region Private Utility Methods

        private static void CheckUsernameValidity(string username)
        {
            if (username.ToCharArray()
                .Any(character => char.IsWhiteSpace(character))) throw new AccessDomainViolationException("Username cannot contain white spaces.");
        }

        private static void CheckPasswordValidity(HashedPassword password)
        {
            if (password == null) throw new AccessDomainViolationException("Password is null.");
            if (password.Salt.Length == 0) throw new AccessDomainViolationException("Password salt has no value.");
            if (password.Value.Length == 0) throw new AccessDomainViolationException("Password has no value.");

        }

        private static void CheckPriviledgesValidity(IEnumerable<AccessPriviledge> priviledges)
        {
            if (priviledges.Count() == 0) throw new AccessDomainViolationException("Account has no access left in the system.");
        }
        #endregion
    }
}
