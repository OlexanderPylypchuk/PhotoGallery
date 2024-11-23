using PhotoGallery.API.Models;

namespace PhotoGallery.API.Repository.IRepository
{
	public interface IPhotoRepository : IRepository<Photo>
	{
		Task Update(Photo photo);
	}
}
