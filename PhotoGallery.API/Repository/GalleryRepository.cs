using PhotoGallery.API.Data;
using PhotoGallery.API.Models;
using PhotoGallery.API.Repository.IRepository;

namespace PhotoGallery.API.Repository
{
	public class GalleryRepository : Repository<Gallery>, IGalleryRepository
	{
		private readonly AppDbContext _db;

        public GalleryRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public async Task Update(Gallery gallery)
		{
			_db.Galleries.Update(gallery);
			await _db.SaveChangesAsync();
		}
	}
}
