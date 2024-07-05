using AutoMapper;
using Domain;
using Services.Services.Models.User.Request;
using Services.Services.Models.User.Response;

namespace Services.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AuthenticateUserModel, User>()
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore());

        CreateMap<CreateUserModel, User>()
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        CreateMap<DeleteUserModel, User>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.Login, map => map.Ignore())
            .ForMember(d => d.Password, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        CreateMap<User, UserModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));
    }
}