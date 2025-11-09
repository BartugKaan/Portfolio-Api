using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;

namespace BartugWeb.PersistanceLayer.Repositories;

public class GetInTouchRepository : Repository<GetInTouch>, IGetInTouchRepository
{
    public GetInTouchRepository(AppDbContext context) : base(context)
    {
    }
}