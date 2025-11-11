using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class Experience : Entity
{
    public string AboutId { get; set; }
    public string Company { get; set; }
    public string Position { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public About About { get; set; }
    
}