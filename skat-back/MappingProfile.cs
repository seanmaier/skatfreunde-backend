using AutoMapper;
using skat_back.dto.BlogPostDto;
using skat_back.dto.MatchRoundDto;
using skat_back.dto.MatchSessionDto;
using skat_back.DTO.PlayerDTO;
using skat_back.dto.PlayerRoundResultDto;
using skat_back.DTO.UserDTO;
using skat_back.models;

namespace skat_back;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, ResponseUserDto>();

        CreateMap<CreatePlayerDto, Player>();
        CreateMap<UpdatePlayerDto, Player>();
        CreateMap<Player, ResponsePlayerDto>();

        CreateMap<BlogPostRequest, BlogPost>();

        CreateMap<CreateMatchSessionDto, MatchSession>();
        CreateMap<UpdateMatchSessionDto, MatchSession>();
        CreateMap<MatchSession, ResponseMatchSessionDto>();

        CreateMap<CreateMatchRoundDto, MatchRound>();
        CreateMap<UpdateMatchRoundDto, MatchRound>();
        CreateMap<MatchRound, ResponseMatchRoundDto>();

        CreateMap<CreatePlayerRoundStatsDto, PlayerRoundStats>();
        CreateMap<UpdatePlayerRoundStatsDto, PlayerRoundStats>();
        CreateMap<PlayerRoundStats, ResponsePlayerRoundStatsDto>();
    }
}