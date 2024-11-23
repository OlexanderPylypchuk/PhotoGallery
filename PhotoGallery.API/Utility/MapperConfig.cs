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
				config.CreateMap<Gallery, GalleryDto>()
					.ForMember(dto => dto.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => new PhotoDto
					{
						Id = p.PhotoId,
						Title = p.Photo.Title,
						Description = p.Photo.Description,
						ImageLocalPath = p.Photo.ImageLocalPath,
						ImgUrl = p.Photo.ImgUrl,
						UserId = p.Photo.UserId
					})));

				config.CreateMap<GalleryDto, Gallery>()
					.ForMember(entity => entity.Photos, opt => opt.MapFrom(dto => dto.Photos.Select(p => new PhotoInGallery
					{
						PhotoId = p.Id.Value,
						Photo = new Photo {
							Id = p.Id.Value,
							Title = p.Title,
							Description = p.Description,
							ImageLocalPath = p.ImageLocalPath,
							ImgUrl = p.ImgUrl,
							UserId = p.UserId
						} 
					})));
            });
            return mapping;
        }
    }
}
