using AutoMapper;
using Services.Services.Contracts.User;
using WebApi.Models.User;
using WebApi.Models.User.Requests;

namespace WebApi.Mapping;

public class UserModelMappingProfile : Profile
{
    public UserModelMappingProfile()
    {
        // Request models
        CreateMap<RequestAuthenticateUserModel, AuthenticateUserDto>();

        CreateMap<RequestAuthorizationUserModel, AuthorizationUserDto>();

        CreateMap<RequestCreateUserModel, CreateUserDto>();

        CreateMap<RequestDeleteUserModel, DeleteUserDto>();
    }
}