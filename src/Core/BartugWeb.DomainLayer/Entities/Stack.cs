using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class Stack : Entity
{
    public string AboutId { get; set; }
    public string Category { get; set; }  // Backend, Frontend, Database, AI Development, Version Control
    public string Technology { get; set; } // React.js, PostgreSQL, etc.
    public int Order { get; set; }  // Display order within category

    // Navigation property
    public About About { get; set; }
}
