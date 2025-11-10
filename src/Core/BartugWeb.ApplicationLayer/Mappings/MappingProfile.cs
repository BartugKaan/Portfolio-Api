using AutoMapper;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;
using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // About Mappings
        CreateMap<CreateAboutCommand, About>();
        CreateMap<UpdateAboutCommand, About>();
        
        // BlogPost Mappings
        CreateMap<CreateBlogPostCommand, BlogPost>();
        CreateMap<UpdateBlogPostCommand, BlogPost>();

    }
}