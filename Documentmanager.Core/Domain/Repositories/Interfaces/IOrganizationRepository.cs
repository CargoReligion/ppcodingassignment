using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documentmanager.Core.Domain.Models.Organizations;

namespace Documentmanager.Core.Domain.Repositories.Interfaces
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        public Task<Organization> GetByName(string name);
    }
}
