using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class About : Entity
{
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Stacks { get; set; } = new List<string>();
    public List<string> Educations { get; set; } = new List<string>();
    public List<string> Experience { get; set; } = new List<string>();
    
}