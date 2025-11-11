using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class About : Entity
{
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<Stack> Stacks { get; set; } = new List<Stack>();
    public List<Education> Educations { get; set; } = new List<Education>();
    public List<Experience> Experiences { get; set; } = new List<Experience>();
    
}