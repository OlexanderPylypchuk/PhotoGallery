using AutoMapper;


namespace PhotoGallery.API.MapperConfig
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<ConferenceRoom, ConferenceRoomDto>().ReverseMap();
                config.CreateMap<ConferenceRoomRent, ConferenceRoomRentDto>().ReverseMap();
                config.CreateMap<Utility, UtilityDto>().ReverseMap();
            });
            return mapping;
        }
    }
}
