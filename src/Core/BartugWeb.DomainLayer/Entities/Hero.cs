using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class Hero : Entity
{
    public string HeroImageUrl { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string JobTitles { get; set; }
    public string Description { get; set; }
}