namespace Documentmanager.Core.Domain.Models.Document
{
    public class Document
    {
        //Parameterless constructor is recommended for dapper
        private Document() { }
        public Document(string name, string storagePath, int userId, int? organizationId)
        {
            Name = name;
            StoragePath = storagePath;
            UserId = userId;
            OrganizationId = organizationId;
            CreatedBy = userId;
            ModifiedBy = userId;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string StoragePath { get; private set; }
        public int UserId { get; private set; }
        public int? OrganizationId { get; private set; }
        public int CreatedBy { get; private set; }
        public int ModifiedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }

        public void Update(string name, string storagePath, int userId, int? organizationId)
        {
            Name = name;
            StoragePath = storagePath;
            UserId = userId;
            OrganizationId = organizationId;
            ModifiedBy = userId;
            DateModified = DateTime.UtcNow;
        }
    }
}
