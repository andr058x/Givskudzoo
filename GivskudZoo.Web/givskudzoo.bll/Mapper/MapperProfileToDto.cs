using AutoMapper;
using GivskudZoo.Dal;
using GivskudZoo.Model;

namespace GivskudZoo.Bll.Mapper
{
    public class MapperProfileToDto : Profile
    {
        public MapperProfileToDto()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<News, NewsDto>()
                .ForMember(src => src.Image, act => act.MapFrom(x => x.Images));
            CreateMap<Events, EventsDto>()
                .ForMember(src => src.Image, act => act.MapFrom(x => x.Images));
            CreateMap<Images, ImageDto>();
        }
    }
}
