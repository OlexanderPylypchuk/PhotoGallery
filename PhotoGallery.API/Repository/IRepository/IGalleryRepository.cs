using PhotoGallery.API.Models;

namespace PhotoGallery.API.Repository.IRepository
{
	public interface IGalleryRepository : IRepository<Gallery>
	{
		Task Update(Gallery gallery);
	}
}
