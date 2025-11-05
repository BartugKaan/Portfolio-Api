using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class Project: Entity
{
    public string ProjectImgUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Keyword { get; set; }
    public string ProjectUrl { get; set; }
}