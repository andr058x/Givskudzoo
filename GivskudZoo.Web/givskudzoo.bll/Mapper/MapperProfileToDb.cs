using AutoMapper;
using GivskudZoo.Dal;
using GivskudZoo.Model;

namespace GivskudZoo.Bll.Mapper
{
    public class MapperProfileToDb : Profile
    {
        public MapperProfileToDb() {
            CreateMap<ActivityDto, Activity>();
            CreateMap<Activity, Activity>()
                .ForMember(src => src.Id, act => act.Ignore())
                .ForMember(src => src.CreationDate, act => act.Ignore());
            CreateMap<NewsDto, News>()
                .ForMember(src => src.Images, act => act.MapFrom(x => x.Image));
            CreateMap<News, News>()
                .ForMember(src => src.Id, act => act.Ignore())
                .ForMember(src => src.ImageId, act => act.Ignore())
                .ForMember(src => src.Images, act => act.Ignore())
                .ForMember(src => src.CreationDate, act => act.Ignore());
            CreateMap<EventsDto, Events>()
                .ForMember(src => src.Images, act => act.MapFrom(x => x.Image));
            CreateMap<Events, Events>()
                .ForMember(src => src.Id, act => act.Ignore())
                .ForMember(src => src.ImageId, act => act.Ignore())
                .ForMember(src => src.Images, act => act.Ignore())
                .ForMember(src => src.CreationDate, act => act.Ignore());
            CreateMap<ImageDto, Images>();
        }
    }
}
