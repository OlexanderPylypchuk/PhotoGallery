using AutoMapper;
using PhotoGallery.API.Models;
using PhotoGallery.API.Models.Dtos;


namespace PhotoGallery.API.MapperConfig
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<Photo, PhotoDto>().ReverseMap();

                config.CreateMap<Gallery, GalleryDto>().ReverseMap();

				config.CreateMap<PhotoInGallery, PhotoDto>()
					.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PhotoId))
					.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Photo.Title)) 
					.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Photo.Description)) 
					.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Photo.UserId))
					.ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Photo.ImgUrl)) 
					.ForMember(dest => dest.ImageLocalPath, opt => opt.MapFrom(src => src.Photo.ImageLocalPath))
					.ForMember(dest => dest.Photo, opt => opt.Ignore());

				config.CreateMap<PhotoDto, PhotoInGallery>()
					.ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.Id ?? 0))
					.ForMember(dest => dest.Photo, opt => opt.Ignore()) 
					.ForMember(dest => dest.GalleryId, opt => opt.Ignore())
					.ForMember(dest => dest.Gallery, opt => opt.Ignore());
			});
            return mapping;
        }
    }
}
