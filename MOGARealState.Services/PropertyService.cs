
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MOGARealState.Core.Entities;
using MOGARealState.Core.Enums;
using MOGARealState.Core.Repositories;
using MOGARealState.Core.Services;
using MOGARealState.Core.Specifications.PropertySpec;

namespace MOGARealState.Services
{
    public class PropertyService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment,
        UserManager<AppUser> userManager) : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<PropertyResponse> AddPropertyAsync(AddPropertyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The body cannot be empty");
            }

            if (!Enum.TryParse<Purpose>(request.Purpose, true, out var purpose))
            {
                throw new ArgumentException($"Invalid Purpose value {request.Purpose}");
            }

            if (!Enum.TryParse<PropertyType>(request.Type, true, out var type))
            {
                throw new ArgumentException($"Invalid PropertyType value");
            }

            if (!Enum.TryParse<PropertyStatus>(request.Status, true, out var status))
            {
                throw new ArgumentException("Invalid PropertyStatus value");
            }

            var property = new Property
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Size = request.Size,
                Price = request.Price,
                RoomsCount = request.RoomsCount,
                BathroomsCount = request.BathroomsCount,
                AgentId = request.AgentId,
                HasParking = request.HasParking,
                HasWifi = request.HasWifi,
                HasElevator = request.HasElevator,
                Purpose = purpose,
                Type = type,
                Status = status,
                IsFurnished = request.IsFurnished,
                FloorsCount = request.FloorsCount,

            };

            if (request.HeadImage != null)
            {
                property.HeadImage = await _fileUploadService.UploadFileAsync(request.HeadImage, "properties");
            }

            if (request.Image1 != null)
            {
                property.Image1 = await _fileUploadService.UploadFileAsync(request.Image1, "properties");
            }

            if (request.Image2 != null)
            {
                property.Image2 = await _fileUploadService.UploadFileAsync(request.Image2, "properties");
            }

            if (request.Image3 != null)
            {
                property.Image3 = await _fileUploadService.UploadFileAsync(request.Image3, "properties");
            }

            if (request.Video != null)
            {
                property.VideoUrl = await _fileUploadService.UploadFileAsync(request.Video, "properties");
            }

            _unitOfWork.Repository<Property>().Add(property);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add property");
            }

            return new PropertyResponse
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Location = property.Location,
                Size = property.Size,
                Price = property.Price,
                RoomsCount = property.RoomsCount,
                BathroomsCount = property.BathroomsCount,
                HeadImage = property.HeadImage,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Purpose = property.Purpose.ToString(),
                Type = property.Type.ToString(),
                Status = property.Status.ToString(),
                HasParking = property.HasParking,
                HasWifi = property.HasWifi,
                HasElevator = property.HasElevator,
                AgentId = property.AgentId,
                VideoUrl = property.VideoUrl,
            };


        }

        public async Task<IReadOnlyList<PropertyResponse>> GetAgentPropertiesAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new AgentPropertiesSpecification(id);

            var props = await _unitOfWork.Repository<Property>().GetAllWithSpecAsync(spec);

            if (props == null || !props.Any())
                throw new Exception("No Properties founded for this Agent");

            return props.Select(property => new PropertyResponse
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Location = property.Location,
                Size = property.Size,
                Price = property.Price,
                RoomsCount = property.RoomsCount,
                BathroomsCount = property.BathroomsCount,
                HeadImage = property.HeadImage,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Purpose = property.Purpose.ToString(),
                Type = property.Type.ToString(),
                Status = property.Status.ToString(),
                HasParking = property.HasParking,
                HasWifi = property.HasWifi,
                HasElevator = property.HasElevator,
                AgentId = property.AgentId,
                AgentEmail = property.Agent.Email,
                AgentImage = property.Agent.ImageUrl,
                AgentName = property.Agent.Name,
                AgentPhone = property.Agent.Phone,
                VideoUrl = property.VideoUrl,

            }).ToList().AsReadOnly();
        }


        public async Task<IReadOnlyList<AllPropertiesResponse>> GetPropertiesAsync(CancellationToken cancellationToken)
        {

            var properties = await _unitOfWork.Repository<Property>().GetAllAsync(cancellationToken);

            if (properties == null || !properties.Any())
            {
                throw new Exception("No properties found");
            }

            return properties
                .OrderByDescending(p => p.Id)
                .Select(p => new AllPropertiesResponse
                {
                    HeadImage = p.HeadImage,
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price

                }).ToList().AsReadOnly();


        }

        public async Task<PropertyResponse> GetPropertyByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }

            var spec = new PropertyWithAgentSpecification(id);

            var property = await _unitOfWork.Repository<Property>().GetByIdWithSpecAsync(spec);

            if (property == null)
            {
                throw new Exception("Prop not founded!");
            }

            return new PropertyResponse
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Location = property.Location,
                Size = property.Size,
                Price = property.Price,
                RoomsCount = property.RoomsCount,
                BathroomsCount = property.BathroomsCount,
                HeadImage = property.HeadImage,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Purpose = property.Purpose.ToString(),
                Type = property.Type.ToString(),
                Status = property.Status.ToString(),
                HasParking = property.HasParking,
                HasWifi = property.HasWifi,
                HasElevator = property.HasElevator,
                AgentId = property.AgentId,
                AgentEmail = property.Agent.Email,
                AgentImage = property.Agent.ImageUrl,
                AgentName = property.Agent.Name,
                AgentPhone = property.Agent.Phone,
                VideoUrl = property.VideoUrl,
            };

        }

        public async Task<bool> MakePropertySoldAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new Exception($"invalid id {nameof(id)}");

            var prop = await _unitOfWork.Repository<Property>().GetByIdAsync(id);

            if (prop == null)
                throw new Exception("Property not founded!");

            prop.Status = PropertyStatus.Sold;

            _unitOfWork.Repository<Property>().Update(prop);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("An Error while change property Status.");

            return true;
        }

        public async Task<PropertyResponse> UpdatePropertyAsync(int id, AddPropertyRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The body cannot be empty");
            }

            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }

            var property = await _unitOfWork.Repository<Property>().GetByIdAsync(id, cancellationToken);

            if (property == null)
            {
                throw new Exception("Property not found");
            }

            if (!Enum.TryParse<Purpose>(request.Purpose, true, out var purpose))
            {
                throw new ArgumentException($"Invalid Purpose value {request.Purpose}");
            }

            if (!Enum.TryParse<PropertyType>(request.Type, true, out var type))
            {
                throw new ArgumentException($"Invalid PropertyType value");
            }

            if (!Enum.TryParse<PropertyStatus>(request.Status, true, out var status))
            {
                throw new ArgumentException("Invalid PropertyStatus value");
            }

            //Delete old images if new images are uploaded

            if (request.HeadImage != null)
            {
                DeleteFileIfExists(property.HeadImage, "properties");
            }

            if (request.Image1 != null)
            {
                DeleteFileIfExists(property.Image1, "properties");
            }

            if (request.Image2 != null)
            {
                DeleteFileIfExists(property.Image2, "properties");
            }

            if (request.Image3 != null)
            {
                DeleteFileIfExists(property.Image3, "properties");
            }

            //Delete old video if new video is uploaded

            if (request.Video != null && !string.IsNullOrEmpty(property.VideoUrl))
            {
                DeleteFileIfExists(property.VideoUrl, "properties");
            }

            property.Name = request.Name;
            property.Description = request.Description;
            property.Location = request.Location;
            property.Size = request.Size;
            property.Price = request.Price;
            property.RoomsCount = request.RoomsCount;
            property.BathroomsCount = request.BathroomsCount;
            property.AgentId = request.AgentId;
            property.HasParking = request.HasParking;
            property.HasWifi = request.HasWifi;
            property.HasElevator = request.HasElevator;
            property.Purpose = purpose;
            property.Type = type;
            property.Status = status;
            property.IsFurnished = request.IsFurnished;
            property.FloorsCount = request.FloorsCount;

            if (request.HeadImage != null)
            {
                property.HeadImage = await _fileUploadService.UploadFileAsync(request.HeadImage, "properties");
            }

            if (request.Image1 != null)
            {
                property.Image1 = await _fileUploadService.UploadFileAsync(request.Image1, "properties");
            }

            if (request.Image2 != null)
            {
                property.Image2 = await _fileUploadService.UploadFileAsync(request.Image2, "properties");
            }

            if (request.Image3 != null)
            {
                property.Image3 = await _fileUploadService.UploadFileAsync(request.Image3, "properties");
            }

            if (request.Video != null)
            {
                property.VideoUrl = await _fileUploadService.UploadFileAsync(request.Video, "properties");
            }

            _unitOfWork.Repository<Property>().Update(property);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to update property");
            }

            return new PropertyResponse
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Location = property.Location,
                Size = property.Size,
                Price = property.Price,
                RoomsCount = property.RoomsCount,
                BathroomsCount = property.BathroomsCount,
                HeadImage = property.HeadImage,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Purpose = property.Purpose.ToString(),
                Type = property.Type.ToString(),
                Status = property.Status.ToString(),
                HasParking = property.HasParking,
                HasWifi = property.HasWifi,
                HasElevator = property.HasElevator,
                AgentId = property.AgentId,
                VideoUrl = property.VideoUrl,
            };
        }

        private void DeleteFileIfExists(string fileName, string folderName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folderName, fileName);
                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }
    }
}