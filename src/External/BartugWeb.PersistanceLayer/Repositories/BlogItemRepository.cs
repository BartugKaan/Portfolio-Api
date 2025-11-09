using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;

namespace BartugWeb.PersistanceLayer.Repositories;

public class BlogItemRepository : Repository<BlogItem>, IBlogItemRepository
{
    public BlogItemRepository(AppDbContext context) : base(context)
    {
    }
}