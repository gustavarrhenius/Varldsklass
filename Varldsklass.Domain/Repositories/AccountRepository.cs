using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories.Abstract;

namespace Varldsklass.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        bool IsValid(string email, string password);
    }

    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository() : base() { }

        public bool IsValid(string email, string password)
        {
            Account account = FindAll(u => u.Email == email).FirstOrDefault();
            if (account == null) return false;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, account.Salt);
            return (FindAll(u => u.Email == email && u.Password == hashedPassword).Count() > 0);
        }
    }
}
