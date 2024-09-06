using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Models.Users;

namespace Documentmanager.Core.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetByEmail(string email);
    }
}
