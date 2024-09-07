using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Dtos.OrganizationUser;
using Documentmanager.Core.Domain.Dtos.Users;
using Documentmanager.Core.Domain.Models.Organizations;
using System;

namespace Documentmanager.Core.Domain.Models.Users
{
    public class User
    {
        //Parameterless constructor is recommended for dapper
        private User(){}
        public User(string email, int userId)
        {
            Email = email;
            CreatedBy = userId;
            ModifiedBy = userId;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public int? OrganizationId { get; set; }
        public int CreatedBy { get; private set; }
        public int ModifiedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }  

        public void UpdateEmail(UpdateUserDto dto, int userId)
        {
            Email = dto.Email;
            ModifiedBy = userId;
            DateModified = DateTime.UtcNow;
        }

        public void UpdateUserOrganization(UpdateOrganizationUserDto dto, int userId)
        {
            OrganizationId = dto.OrganizationId;
            ModifiedBy = userId;
            DateModified= DateTime.UtcNow;
        }
    }
}
