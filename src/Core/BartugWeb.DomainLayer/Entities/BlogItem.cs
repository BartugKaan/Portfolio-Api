using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class BlogItem : Entity
{
    public string CoverImgUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}