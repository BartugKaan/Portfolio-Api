using BartugWeb.DomainLayer.Abstracts;
using BartugWeb.DomainLayer.Enums;

namespace BartugWeb.DomainLayer.Entities;

public class BlogItem : Entity
{
    public string CoverImgUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public BlogCategory BlogCategory { get; set; } = BlogCategory.Programming;
}