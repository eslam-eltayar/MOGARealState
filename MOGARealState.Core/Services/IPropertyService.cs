using MOGARealState.Core.DTOs.Requests;


namespace MOGARealState.Core.Services
{
    public interface IPropertyService
    {
        Task<PropertyResponse> AddPropertyAsync(AddPropertyRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<AllPropertiesResponse>> GetPropertiesAsync(CancellationToken cancellationToken = default);
        Task<PropertyResponse> GetPropertyByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PropertyResponse> UpdatePropertyAsync(int id, AddPropertyRequest request, CancellationToken cancellationToken = default);

        Task<bool> MakePropertySoldAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PropertyResponse>> GetAgentPropertiesAsync(int id, CancellationToken cancellationToken = default);
    }
}