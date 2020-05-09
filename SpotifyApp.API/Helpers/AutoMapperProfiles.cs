using AutoMapper;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<AlbumDto, AlbumDto>();
            CreateMap<AlbumRateDto, AlbumRate>();
            CreateMap<ArtistRateDto, ArtistRate>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CommentForCreationDto, Comment>().ReverseMap();
            CreateMap<Comment, CommentToReturnDto>();
        }
    }
}