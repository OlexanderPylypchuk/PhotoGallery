namespace PhotoGallery.API.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IPhotoInGalleryRepository PhotoInGalleryRepository { get; }
		IPhotoRepository PhotoRepository { get; }
		IGalleryRepository GalleryRepository { get; }
	}
}
