using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Dtos.Users
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
