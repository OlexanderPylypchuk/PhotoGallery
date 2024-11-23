using PhotoGallery.API.Data;
using PhotoGallery.API.Repository.IRepository;

namespace PhotoGallery.API.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;

		public UnitOfWork(AppDbContext db)
        {
            _db = db;
			PhotoInGalleryRepository = new PhotoInGalleryRepository(_db);
			PhotoRepository = new PhotoRepository(_db);
			GalleryRepository = new GalleryRepository(_db);
        }

		public IPhotoInGalleryRepository PhotoInGalleryRepository { get; private set; }
		public IPhotoRepository PhotoRepository { get; private set; }
		public IGalleryRepository GalleryRepository { get; private set; }
	}
}
