using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ninject;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Mvc;

namespace Varldsklass.Web.Infrastructure
{
    public class CustomMembership : MembershipProvider
    {
        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        private ProviderSettings providerSettings;

        public CustomMembership()
        {
            MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
            providerSettings = membershipSection.Providers[membershipSection.DefaultProvider];
        }

        public void CreateUser(string firstName, string lastName, string email, string password, out MembershipCreateStatus createStatus)
        {
            Account account = new Account();
            account.Email = email;
            account.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            account.Password = BCrypt.Net.BCrypt.HashPassword(password, account.Salt);
            account.CreatedDate = DateTime.Now;
            account.FirstName = firstName;
            account.LastName = lastName;
            account.Administrator = false;
            AccountRepository.Save(account);

            createStatus = MembershipCreateStatus.Success;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return int.Parse( providerSettings.Parameters["minRequiredPasswordLength"] );
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override bool ValidateUser(string email, string password)
        {
            return AccountRepository.IsValid(email, password);
        }

        public bool IsAdmin(string email)
        {
            return (AccountRepository.FindAll(u => u.Email == email && u.Administrator == true).Count() > 0);
        }
    }
}