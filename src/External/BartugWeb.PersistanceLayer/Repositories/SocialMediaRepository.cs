using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;

namespace BartugWeb.PersistanceLayer.Repositories;

public class SocialMediaRepository : Repository<SocialMedia>, ISocialMediaRepository
{
    public SocialMediaRepository(AppDbContext context) : base(context)
    {
    }
}