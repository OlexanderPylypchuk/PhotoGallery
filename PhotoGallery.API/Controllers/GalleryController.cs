using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.API.Data;
using PhotoGallery.API.Models;
using PhotoGallery.API.Models.Dtos;
using PhotoGallery.API.Repository.IRepository;
using PhotoGallery.API.Utility;

namespace PhotoGallery.API.Controllers
{
	[Route("api/gallery")]
	[ApiController]
	public class GalleryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly AppDbContext _db;
		private ResponceDto _responceDto;

		public GalleryController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext db)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_responceDto = new ResponceDto();
			_db = db;
		}

		[HttpGet("{id:int}")]
		public async Task<ResponceDto> Get(int id)
		{
			try
			{
				var gallery = await _unitOfWork.GalleryRepository.GetAsync(u => u.Id == id, includeProperties: "Photos");

				if (gallery == null)
				{
					throw new Exception("Failed to find gallery");
				}

				await GetPhotos(gallery);

				_responceDto.Success = true;
				_responceDto.Result = gallery;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpGet("{name}")]
		public async Task<ResponceDto> Get(string title)
		{
			try
			{
				var gallery = await _unitOfWork.GalleryRepository.GetAsync(u => u.Name == title, includeProperties: "Photos");

				if (gallery == null)
				{
					throw new Exception("Failed to find gallery");
				}

				await GetPhotos(gallery);

				_responceDto.Success = true;
				_responceDto.Result = gallery;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpGet]
		public async Task<ResponceDto> GetAll([FromQuery] int pageSize = 5, int pageNumber = 1)
		{
			try
			{
				var galleries = _unitOfWork.GalleryRepository.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber, includeProperties: "Photos")
					.GetAwaiter()
					.GetResult()
					.ToList();

				if (galleries == null)
				{
					throw new Exception("Failed to find galleries");
				}

				//Дуже погано і не ефективно, потрібен рефакторинг
				foreach(var gallery in galleries)
				{
					await GetPhotos(gallery);
				}

				var galleriesDto = _mapper.Map<List<GalleryDto>>(galleries);
				_responceDto.Success = true;
				_responceDto.Result = galleriesDto;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		private async Task GetPhotos(Gallery gallery)
		{
			foreach (var photoInGallery in gallery.Photos)
			{
				photoInGallery.Photo = await _unitOfWork.PhotoRepository.GetAsync(p => p.Id == photoInGallery.PhotoId);
			}
		}

		[HttpPost]
		[Authorize]
		[Route("create")]
		public async Task<ResponceDto> Create([FromBody] GalleryDto galleryDto)
		{
			try
			{
				var gallery = _mapper.Map<Gallery>(galleryDto);
				var userid = HttpContext.User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName).Value;
				gallery.UserId = userid;

				await _unitOfWork.GalleryRepository.CreateAsync(gallery);

				foreach(var photodto in galleryDto.Photos)
				{
					var existingPhoto = await _unitOfWork.PhotoInGalleryRepository
						.GetAsync(p => p.GalleryId == gallery.Id && p.PhotoId == photodto.Id.Value);

					if (existingPhoto == null)
					{
						gallery.Photos.Add(new PhotoInGallery()
						{
							GalleryId = gallery.Id,
							PhotoId = photodto.Id.Value
						});
					}

				}

				await _unitOfWork.GalleryRepository.Update(gallery);

				await GetPhotos(gallery);

				_responceDto.Success = true;
				_responceDto.Result = _mapper.Map<GalleryDto>(gallery);
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpPut]
		[Authorize]
		[Route("update")]
		public async Task<ResponceDto> Update([FromBody] GalleryDto galleryDto)
		{
			try
			{
				string userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName).Value;

				if (userId != galleryDto.UserId)
				{
					throw new Exception("User Id does not match");
				}

				var gallery = _mapper.Map<Gallery>(galleryDto);
				gallery.UserId = userId;

				await _unitOfWork.GalleryRepository.Update(gallery);

				await GetPhotos(gallery);

				_responceDto.Success = true;
				_responceDto.Result = _mapper.Map<GalleryDto>(gallery);
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpDelete]
		[Authorize]
		[Route("delete/{id:int}")]
		public async Task<ResponceDto> Delete(int id)
		{
			try
			{
				var gallery = await _unitOfWork.GalleryRepository.GetAsync(u => u.Id==id);
				string userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName).Value;

				if (userId != gallery.UserId)
				{
					throw new Exception("User Id does not match");
				}

				await _unitOfWork.GalleryRepository.DeleteAsync(gallery);

				_responceDto.Success = true;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}
	}
}
