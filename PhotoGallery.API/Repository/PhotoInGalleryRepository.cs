using System.Runtime.CompilerServices;
using PhotoGallery.API.Data;
using PhotoGallery.API.Models;
using PhotoGallery.API.Repository.IRepository;

namespace PhotoGallery.API.Repository
{
	public class PhotoInGalleryRepository : Repository<PhotoInGallery>, IPhotoInGalleryRepository
	{
		private readonly AppDbContext _db;

		public PhotoInGalleryRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task Update(PhotoInGallery photoInGallery)
		{
			_db.PhotoInGallery.Update(photoInGallery);
			await _db.SaveChangesAsync();
		}
	}
}
