using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Dtos.Users;

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
        public int CreatedBy { get; private set; }
        public int ModifiedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }  

        public void UpdateEmail(UpdateUserDto dto)
        {
            Email=dto.Email;
        }
    }
}
