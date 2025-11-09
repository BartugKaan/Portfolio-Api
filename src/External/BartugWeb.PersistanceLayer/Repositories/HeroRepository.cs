using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;

namespace BartugWeb.PersistanceLayer.Repositories;

public class HeroRepository : Repository<Hero>, IHeroRepository
{
    public HeroRepository(AppDbContext context) : base(context)
    {
    }
}