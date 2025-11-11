using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.DomainLayer.Entities;

public class Education : Entity
{
    public string AboutId { get; set; }
    public string Title { get; set; }
    public string SchoolName { get; set; }
    public int StartYear{ get; set; }
    public int? EndYear { get; set; }
    
    public About About { get; set; }
}