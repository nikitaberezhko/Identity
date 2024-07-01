using AutoMapper;
using Services.Services.Contracts.User;
using Services.Services.Contracts.User.Response;
using WebApi.Models.User.Requests;
using WebApi.Models.User.Responses;

namespace WebApi.Mapping;

public class UserModelMappingProfile : Profile
{
    public UserModelMappingProfile()
    {
        // Request models
        CreateMap<RequestAuthenticateUserModel, AuthenticateUserDto>()
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password));

        CreateMap<RequestAuthorizationUserModel, AuthorizationUserDto>()
            .ForMember(d => d.Token, map => map.MapFrom(c => c.Token));
        
        CreateMap<RequestCreateUserModel, CreateUserDto>()
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));

        CreateMap<RequestDeleteUserModel, DeleteUserDto>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        // response models
        CreateMap<ResponseAuthorizationUserDto, ResponseAuthorizationModel>()
            .ForMember(d => d.UserId, map => map.MapFrom(c => c.UserId))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId));
    }
}