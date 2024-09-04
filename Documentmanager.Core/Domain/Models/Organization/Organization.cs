namespace Documentmanager.Core.Domain.Models.Organization
{
    public class Organization
    {
        public Organization(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
