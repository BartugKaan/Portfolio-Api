using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class SocialMedia : Entity
{
    public string IconUrl { get; set; }
    public string LinkUrl { get; set; }
}