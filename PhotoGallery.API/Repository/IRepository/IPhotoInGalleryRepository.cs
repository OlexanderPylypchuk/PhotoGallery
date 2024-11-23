using PhotoGallery.API.Models;

namespace PhotoGallery.API.Repository.IRepository
{
	public interface IPhotoInGalleryRepository : IRepository<PhotoInGallery>
	{
		Task Update(PhotoInGallery photoInGallery);
	}
}
