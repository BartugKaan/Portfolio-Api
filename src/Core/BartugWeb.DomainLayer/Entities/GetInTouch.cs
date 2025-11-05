using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class GetInTouch : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactMessage { get; set; }
    
}