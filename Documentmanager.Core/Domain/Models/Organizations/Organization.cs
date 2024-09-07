using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Dtos.Organizations;

namespace Documentmanager.Core.Domain.Models.Organizations
{
    public class Organization
    {
        //Parameterless constructor is recommended for dapper
        private Organization(){}
        public Organization(string name, int userId)
        {
            Name = name;
            CreatedBy = userId;
            ModifiedBy = userId;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public int ModifiedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }  

        public void UpdateName(UpdateOrganizationDto dto, int userId)
        {
            Name = dto.Name;
            ModifiedBy = userId;
            DateModified = DateTime.UtcNow;
        }
    }
}
