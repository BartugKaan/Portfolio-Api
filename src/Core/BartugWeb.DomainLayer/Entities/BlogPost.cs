using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class BlogPost : Entity
{
    public string HeaderImageUrl { get; set; }
    public string Title { get; set; }
    public string BlogContent { get; set; }
    public List<string> Keywords { get; set; } = new List<string>();
}