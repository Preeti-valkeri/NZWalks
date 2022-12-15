
using NZWalks.API.Models.Domain;


namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalk();
        Task<Walk> GetWalks(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
    }
}
