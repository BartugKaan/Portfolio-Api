using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Repositories;

public class AboutRepository : Repository<About>, IAboutRepository
{
    public AboutRepository(AppDbContext context) : base(context)
    {
    }
}