using PhotoGallery.API.Data;
using PhotoGallery.API.Models;
using PhotoGallery.API.Repository.IRepository;

namespace PhotoGallery.API.Repository
{
	public class PhotoRepository : Repository<Photo>, IPhotoRepository
	{
		private readonly AppDbContext _db;
		
        public PhotoRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Update(Photo photo)
		{
			_db.Photos.Update(photo);
			await _db.SaveChangesAsync();
		}
	}
}
