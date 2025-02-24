using MOGARealState.Core.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Services
{
    public interface IUserService
    {
        Task<bool> FavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default);
        Task<bool> IsFavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default);
        Task<bool> DeleteFavoritePropertyAsync(string userId, int properId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<AllPropertiesResponse>> GetFavoritePropertiesAsync(string userId, CancellationToken cancellationToken = default);

        Task<bool> OrderPropertyAsync(string userId, int properId, CancellationToken cancellationToken = default);

    }
}
