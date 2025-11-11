using AutoMapper;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;
using BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.CreateCommand;
using BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.CreateCommands;
using BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.UpdateCommands;
using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // About Mappings
        CreateMap<CreateAboutCommand, About>();
        CreateMap<UpdateAboutCommand, About>();
        
        // Experience Mappings
        CreateMap<CreateExperienceCommand, Experience>();
        CreateMap<UpdateExperienceCommand, Experience>();

        // Education Mappings
        CreateMap<CreateEducationCommand, Education>();
        CreateMap<UpdateEducationCommand, Education>();

        // Stack Mappings
        CreateMap<CreateStackCommand, Stack>();
        CreateMap<UpdateStackCommand, Stack>();

        // BlogPost Mappings
        CreateMap<CreateBlogPostCommand, BlogPost>();
        CreateMap<UpdateBlogPostCommand, BlogPost>();

        // BlogItem Mappings
        CreateMap<CreateBlogItemCommand, BlogItem>();
        CreateMap<UpdateBlogItemCommand, BlogItem>();
        
        // GetInTouch Mappings
        CreateMap<CreateGetInTouchCommand, GetInTouch>();
        CreateMap<UpdateGetInTouchCommand, GetInTouch>();
        
        // Hero Mappings
        CreateMap<CreateHeroCommand, Hero>();
        CreateMap<UpdateHeroCommand, Hero>();
        
        // Project Mappings
        CreateMap<CreateProjectCommand, Project>();
        CreateMap<UpdateProjectCommand, Project>();
        
        // SocialMedia Mappings
        CreateMap<CreateSocialMediaCommand, SocialMedia>();
        CreateMap<UpdateSocialMediaCommand, SocialMedia>();
    }
}
