using MOGARealState.Core.Entities;
using MOGARealState.Core.Enums;
using MOGARealState.Core.Repositories;
using MOGARealState.Core.Specifications.UserSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> DeleteFavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default)
        {
            var existingFavorite = await _unitOfWork.Repository<FavoriteUserProperties>()
                 .FirstOrDefaultAsync(fp => fp.AppUserId == userId && fp.PropertyId == properId);


            if (existingFavorite == null)
            {
                throw new Exception("Favorite Property not found.");
            }

            _unitOfWork.Repository<FavoriteUserProperties>().Delete(existingFavorite);
            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("An error occurred while removing the Property from favorites.");
            }

            return true;
        }

        public async Task<bool> FavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default)
        {
            var existingFavorite = await _unitOfWork.Repository<FavoriteUserProperties>()
                .FirstOrDefaultAsync(fp => fp.AppUserId == userId && fp.PropertyId == properId);

            if (existingFavorite != null)
            {
                throw new Exception("This Property is already in your favorites.");
            }

            var favoriteProperty = new FavoriteUserProperties
            {
                AppUserId = userId,
                PropertyId = properId
            };

            _unitOfWork.Repository<FavoriteUserProperties>().Add(favoriteProperty);
            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("An error occurred while adding the Property to favorites.");
            }

            return true;
        }

        public async Task<IReadOnlyList<AllPropertiesResponse>> GetFavoritePropertiesAsync(string userId, CancellationToken cancellationToken = default)
        {
            var spec = new AllFavoritePropertiesByUserSpecification(userId);

            var favProps = await _unitOfWork
                .Repository<FavoriteUserProperties>().GetAllWithSpecAsync(spec, cancellationToken);

            if (favProps is null || !favProps.Any())
                throw new Exception("No Favorite Properties Founded");

            return favProps.Select(f => new AllPropertiesResponse
            {
                Id = f.PropertyId,
                HeadImage = f.Property.HeadImage,
                Name = f.Property.Name,
                Price = f.Property.Price

            }).ToList().AsReadOnly();
        }

        public async Task<UserDataResponse> GetUserDataAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<AppUser>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var userDataResponse = new UserDataResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Phone = user.PhoneNumber
            };

            return userDataResponse;
        }

        public async Task<bool> IsFavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default)
        {
            var favoriteProperty = await _unitOfWork.Repository<FavoriteUserProperties>()
            .FirstOrDefaultAsync(fp => fp.AppUserId == userId && fp.PropertyId == properId);

            if (favoriteProperty == null)
                return false;

            return true;
        }

        public async Task<bool> OrderPropertyAsync(string userId, int properId, CancellationToken cancellationToken = default)
        {
            var existingOrder = await _unitOfWork.Repository<UserOrders>()
               .FirstOrDefaultAsync(fp => fp.AppUserId == userId && fp.PropertyId == properId);

            if (existingOrder != null)
            {
                throw new Exception("This Property is already in your Orders.");
            }

            var userOrder = new UserOrders
            {
                AppUserId = userId,
                PropertyId = properId,
                Status = OrderStatus.Pending,
            };

            _unitOfWork.Repository<UserOrders>().Add(userOrder);
            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("An error occurred while adding the Property to Orders.");
            }

            return true;
        }
    }
}
