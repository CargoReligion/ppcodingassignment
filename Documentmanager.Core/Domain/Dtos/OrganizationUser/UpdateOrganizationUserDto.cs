using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Dtos.OrganizationUser
{
    public class UpdateOrganizationUserDto
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
    }
}
