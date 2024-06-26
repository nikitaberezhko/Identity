using AutoMapper;
using Domain;
using Services.Services.Contracts.User;

namespace Services.Services.Implementations.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AuthenticateUserDto, User>()
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore());

        //CreateMap<AuthorizationUserDto, User>();

        CreateMap<CreateUserDto, User>()
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        CreateMap<DeleteUserDto, User>()
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore())
            .ForMember(d => d.Login, map => map.Ignore())
            .ForMember(d => d.Password, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

    }
}