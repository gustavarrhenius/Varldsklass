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
            IQueryable<Account> matchingUsers = FindAll(u => u.Email == email && u.Password == password);
            return (matchingUsers.Count() > 0);
        }
    }
}
