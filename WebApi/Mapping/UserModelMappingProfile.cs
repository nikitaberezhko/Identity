using AutoMapper;
using Services.Services.Models.User.Request;
using Services.Services.Models.User.Response;
using WebApi.Models.User.Requests;
using WebApi.Models.User.Responses;

namespace WebApi.Mapping;

public class UserModelMappingProfile : Profile
{
    public UserModelMappingProfile()
    {
        // Request models
        CreateMap<AuthenticateUserRequest, AuthenticateUserModel>()
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password));

        CreateMap<AuthorizationUserRequest, AuthorizationUserModel>()
            .ForMember(d => d.Token, map => map.MapFrom(c => c.Token));
        
        CreateMap<CreateUserRequest, CreateUserModel>()
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Login, map => map.MapFrom(c => c.Login))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));

        CreateMap<DeleteUserRequest, DeleteUserModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        // response models
        CreateMap<UserModel, AuthorizationResponse>()
            .ForMember(d => d.UserId, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId));
        
        CreateMap<UserModel, DeleteUserResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name));
    }
}