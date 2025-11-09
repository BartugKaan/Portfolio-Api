using AutoMapper;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;
using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAboutCommand, About>();
        CreateMap<UpdateAboutCommand, About>();

    }
}