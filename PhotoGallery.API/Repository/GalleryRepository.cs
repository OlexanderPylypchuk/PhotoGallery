using Microsoft.EntityFrameworkCore;
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
			var list = _db.PhotoInGallery.Where(x => x.GalleryId == gallery.Id);

			_db.RemoveRange(list);

			_db.Galleries.Update(gallery);
			await _db.SaveChangesAsync();
		}
	}
}
