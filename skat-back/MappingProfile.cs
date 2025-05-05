using AutoMapper;
using skat_back.dto.BlogPostDto;
using skat_back.DTO.PlayerDTO;
using skat_back.DTO.UserDTO;
using skat_back.models;

namespace skat_back;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<User, UserResponseDto>();

        CreateMap<CreatePlayerDto, Player>();
        CreateMap<Player, PlayerResponseDto>();

        CreateMap<BlogPostRequest, BlogPost>();
    }
}