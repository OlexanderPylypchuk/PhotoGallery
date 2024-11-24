using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Models;
using PhotoGallery.API.Models.Dtos;
using PhotoGallery.API.Repository.IRepository;
using PhotoGallery.API.Utility;

namespace PhotoGallery.API.Controllers
{
	[Route("api/photo")]
	[ApiController]
	public class PhotoController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private ResponceDto _responceDto;
		private readonly IMapper _mapper;

		public PhotoController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_responceDto = new ResponceDto();
			_mapper = mapper;
		}

		[HttpGet("{id:int}")]
		public async Task<ResponceDto> Get(int id)
		{
			try
			{
				var photo = await _unitOfWork.PhotoRepository.GetAsync(u => u.Id == id);

				if(photo == null)
				{
					throw new Exception("Failed to find photo");
				}

				_responceDto.Success = true;
				_responceDto.Result = photo;
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
				var photos = await _unitOfWork.PhotoRepository.GetAllAsync(pageSize : pageSize, pageNumber : pageNumber);

				if (photos == null)
				{
					throw new Exception("Failed to find photos");
				}

				_responceDto.Success = true;
				_responceDto.Result = photos;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpPost]
		[Route("create")]
		[Authorize]
		public async Task<ResponceDto> Create(PhotoDto photoDto)
		{
			try
			{
				string userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName).Value;

				if (photoDto.Photo != null)
				{
					Photo photo = _mapper.Map<Photo>(photoDto);
					photo.Id = 0;
					photo.UserId = userId;

					string fileName = Guid.NewGuid() + Path.GetExtension(photoDto.Photo.FileName);
					string filePath = @"wwwroot\images\" + fileName;

					var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);

					using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
					{
						photoDto.Photo.CopyTo(fileStream);
					}

					var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";

					photo.ImgUrl = baseUrl + "/images/" + fileName;
					photo.ImageLocalPath = filePath;

					await _unitOfWork.PhotoRepository.CreateAsync(photo);

					_responceDto.Success = true;
					_responceDto.Result = _mapper.Map<PhotoDto>(photo);
				}
				else
				{
					throw new Exception("No image uploaded");
				}
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
		public async Task<ResponceDto> Update(PhotoDto photoDto)
		{
			try
			{
				string userId = User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName)?.Value;

				if (userId != photoDto.UserId)
				{
					throw new Exception("User Id does not match");
				}

				var photo = _mapper.Map<Photo>(photoDto);

				if (photoDto.Photo != null)
				{
					string fileName = Guid.NewGuid() + Path.GetExtension(photoDto.Photo.FileName);
					string filePath = @"wwwroot\images\" + fileName;

					if (!string.IsNullOrEmpty(photo.ImageLocalPath))
					{
						var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), photoDto.ImageLocalPath);
						FileInfo file = new FileInfo(oldFileDirectory);

						if (file.Exists)
						{
							file.Delete();
						}
					}


					var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
					using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
					{
						photoDto.Photo.CopyTo(fileStream);

					}
					var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";

					photo.ImgUrl = baseUrl + "/images/" + fileName;
					photo.ImageLocalPath = filePath;
				}

				await _unitOfWork.PhotoRepository.Update(photo);
				_responceDto.Success = true;
				_responceDto.Result = _mapper.Map<PhotoDto>(photo);
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
				string userId = User.Claims.FirstOrDefault(u => u.Type == SD.IdClaimName)?.Value;
				var photo = await _unitOfWork.PhotoRepository.GetAsync(u => u.Id == id);
				if (photo.UserId != userId)
				{
					throw new Exception("User Id does not match");
				}

				if (!string.IsNullOrEmpty(photo.ImageLocalPath))
				{
					var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), photo.ImageLocalPath);
					FileInfo file = new FileInfo(oldFileDirectory);

					if (file.Exists)
					{
						file.Delete();
					}
				}

				await _unitOfWork.PhotoRepository.DeleteAsync(photo);
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
